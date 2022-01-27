namespace StockIndicatorLib
{
    public class CTableEx
    {
        private int capacity = 4;
        private int step = 4;
        private CList<int[]> columnList = new CList<int[]>();
        private CList<double> rowPKList = new CList<double>();
        private static int currIndex = 0x2710;
        private static int currDisableIndex = 530000;
        private bool disposed;
        private CList<CRow> rowDataList = new CList<CRow>();
        public static int NULLFIELD = -1;

        private CRow setRowPK(double rowPK)
        {
            CRow row;

            this.rowPKList.add(rowPK);
            row = new CRow(this.capacity, this.step);
            this.rowDataList.add(row);
            return row;

            if ((this.rowPKList.Count == 0) || (rowPK > this.rowPKList.get(this.rowPKList.Count - 1)))
            {
                this.rowPKList.add(rowPK);
                row = new CRow(this.capacity, this.step);
                this.rowDataList.add(row);
                return row;
            }
            int index = 0;
            int num2 = this.rowPKList.Count - 1;
            for (int i = num2 - 0; i > 1; i = num2 - index)
            {
                int num4 = index + (i / 2);
                double num5 = this.rowPKList.get(num4);
                if (num5 > rowPK)
                {
                    num2 = num4;
                }
                else if (num5 < rowPK)
                {
                    index = num4;
                }
            }
            if (rowPK < this.rowPKList.get(index))
            {
                this.rowPKList.insert(index, rowPK);
                row = new CRow(this.capacity, this.step);
                this.rowDataList.insert(index, row);
                return row;
            }
            if (rowPK > this.rowPKList.get(num2))
            {
                this.rowPKList.insert(num2 + 1, rowPK);
                row = new CRow(this.capacity, this.step);
                this.rowDataList.insert(num2 + 1, row);
                return row;
            }
            this.rowPKList.insert(index + 1, rowPK);
            row = new CRow(this.capacity, this.step);
            this.rowDataList.insert(index + 1, row);
            return row;
        }

        private void FillColData()
        {
            int columns = this.columnList.Count;
            for (int i = 0; i < this.rowDataList.Count; i++)
            {
                this.rowDataList.get(i).FillEmpty(columns);
            }
        }

        public int AddColumn(int colName)
        {
            var rowIndex = this.columnList.Count;
            int[] numArray = new int[] { colName, rowIndex };
            this.columnList.add(numArray);
            return rowIndex;



            if (this.columnList.Count == 0)
            {
                this.columnList.add(numArray);
            }
            else
            {
                int index = 0;
                int num2 = this.columnList.Count - 1;
                for (int i = num2 - 0; i > 1; i = num2 - index)
                {
                    int num4 = index + (i / 2);
                    int num5 = this.columnList.get(num4)[0];
                    if (num5 > colName)
                    {
                        num2 = num4;
                    }
                    else if (num5 < colName)
                    {
                        index = num4;
                    }
                }
                if (colName < this.columnList.get(index)[0])
                {
                    this.columnList.insert(index, numArray);
                    this.FillColData();
                }
                else if (colName > this.columnList.get(num2)[0])
                {
                    this.columnList.insert(num2 + 1, numArray);
                    this.FillColData();
                }
                else
                {
                    this.columnList.insert(index + 1, numArray);
                    this.FillColData();
                }
            }
        }

        public void AddRow(double pk, double[] ary, int size)
        {
            this.rowPKList.add(pk);
            CRow row = new CRow(ary, size);
            this.rowDataList.add(row);
        }

        public void Clear()
        {
            if (this.rowPKList != null)
            {
                this.rowPKList.clear();
            }
            if (this.rowDataList != null)
            {
                for (int i = 0; i < this.rowDataList.Count; i++)
                {
                    CRow row = this.rowDataList.get(i);
                    if (row != null)
                    {
                        row.Dispose();
                    }
                }
                this.rowDataList.clear();
            }
        }

        public bool ContainsColumn(int colName)
        {
            return (this.GetColumnIndex(colName) != -1);
        }

        public void Dispose()
        {
            if (!this.disposed)
            {
                this.Clear();
                if (this.columnList != null)
                {
                    this.columnList.Dispose();
                    this.columnList = null;
                }
                if (this.rowPKList != null)
                {
                    this.rowPKList.Dispose();
                    this.rowPKList = null;
                }
                if (this.rowDataList != null)
                {
                    this.rowDataList.Dispose();
                    this.rowDataList = null;
                }
                this.disposed = true;
            }
        }

        ~CTableEx()
        {
            this.Dispose();
        }

        public double Get(double pk, int colName)
        {
            int rowIndex = this.GetRowIndex(pk);
            int columnIndex = this.GetColumnIndex(colName);
            return this.rowDataList.m_ary[rowIndex].m_values.m_ary[columnIndex];
        }

        public double Get2(int rowIndex, int colName)
        {
            int columnIndex = this.GetColumnIndex(colName);
            var arr = this.rowDataList.m_ary[rowIndex];
            if (arr == null) return double.NaN;
            return arr.m_values.m_ary[columnIndex];
        }

        public double Get3(int rowIndex, int colIndex)
        {
            if (rowIndex < 0 || colIndex < 0) return double.NaN;
            return this.rowDataList.m_ary[rowIndex].m_values.m_ary[colIndex];
        }

        public int GetColumnIndex(int colName)
        {
            if ((colName != NULLFIELD) && (this.columnList != null))
            {
                for (int i = 0; i < this.columnList.Count; i++)
                {
                    if (colName == this.columnList.m_ary[i][0])
                        return i;
                }
                return -1;
                int num = 0;
                int num2 = this.columnList.Count - 1;
                while (num <= num2)
                {
                    int index = (num + num2) / 2;
                    int[] numArray = this.columnList.get(index);
                    if (colName == numArray[0])
                    {
                        return index;
                    }
                    if (colName > numArray[0])
                    {
                        num = index + 1;
                    }
                    else if (colName < numArray[0])
                    {
                        num2 = index - 1;
                    }
                }
            }
            return -1;
        }

        public int[] GetColumns()
        {
            int num = this.columnList.Count;
            int[] numArray = new int[num];
            for (int i = 0; i < num; i++)
            {
                numArray[i] = this.columnList.get(i)[0];
            }
            return numArray;
        }

        public int GetRowIndex(double key)
        {
            if (double.IsNaN(key) || this.rowPKList.m_ary == null) return -1;
            for (int i = 0; i < this.rowPKList.m_ary.Length; i++)
            {
                if (this.rowPKList.m_ary[i] == key) return i;

            }
            return -1;


            if (this.rowPKList != null)
            {
                int num = 0;
                int num2 = this.rowPKList.Count - 1;
                while (num <= num2)
                {
                    int index = (num + num2) / 2;
                    double num4 = this.rowPKList.get(index);
                    if (key == num4)
                    {
                        return index;
                    }
                    if (key > num4)
                    {
                        num = index + 1;
                    }
                    else if (key < num4)
                    {
                        num2 = index - 1;
                    }
                }
            }
            return -1;
        }

        public double GetXValue(int index)
        {
            try
            {
                return this.rowPKList.get(index);
            }
            catch
            {
                return double.NaN;
            }
        }

        public void Remove(double pk)
        {
            int rowIndex = this.GetRowIndex(pk);
            this.rowPKList.remove_at(rowIndex);
            this.rowDataList.get(rowIndex).Clear();
            this.rowDataList.remove_at(rowIndex);
        }

        public void RemoveAt(int rowIndex)
        {
            this.rowPKList.remove_at(rowIndex);
            this.rowDataList.get(rowIndex).Clear();
            this.rowDataList.remove_at(rowIndex);
        }

        public void RemoveColumn(int colName)
        {
            int columnIndex = this.GetColumnIndex(colName);
            if (columnIndex != -1)
            {
                int num4;
                int num2 = this.columnList.Count;
                int num3 = -1;
                for (num4 = 0; num4 < num2; num4++)
                {
                    int[] numArray = this.columnList.get(num4);
                    int num5 = numArray[0];
                    int num6 = numArray[1];
                    if (numArray[0] == colName)
                    {
                        num3 = num4;
                    }
                    else if (num6 > columnIndex)
                    {
                        this.columnList.set(num4, new int[] { num5, num6 - 1 });
                    }
                }
                this.columnList.remove_at(num3);
                for (num4 = 0; num4 < this.rowDataList.Count; num4++)
                {
                    this.rowDataList.get(num4).Remove(columnIndex);
                    this.rowDataList.get(num4).FillEmpty(this.columnList.Count);
                }
            }
        }

        public void Set(double pk, int colName, double value)
        {
            CRow row = null;
            int rowIndex = this.GetRowIndex(pk);
            if (rowIndex == -1)
            {
                row = this.setRowPK(pk);
                row.FillEmpty(this.columnList.Count);
            }
            else
            {
                row = this.rowDataList.get(rowIndex);
            }
            int columnIndex = this.GetColumnIndex(colName);
            row.Set(columnIndex, value);
        }

        public void SetByColName(int rowIndex, int colName, double value)
        {
            int columnIndex = this.GetColumnIndex(colName);
            var ff = this.rowDataList.get(rowIndex);
            if (ff.m_values.Count <= columnIndex+1)
                ff.FillEmpty(columnIndex+1);

            ff.Set(columnIndex, value);
        }

        public void SetByIndex(int rowIndex, int colIndex, double value)
        {
            this.rowDataList.get(rowIndex).Set(colIndex, value);
        }

        public void SetColsCapacity(int capacity)
        {
            this.capacity = capacity;
        }

        public void SetColsGrowStep(int step)
        {
            this.step = step;
        }

        public void SetRowsCapacity(int capacity)
        {
            this.rowPKList.set_capacity(capacity);
            this.rowDataList.set_capacity(capacity);
        }

        public void SetRowsGrowStep(int step)
        {
            this.rowPKList.set_step(step);
            this.rowDataList.set_step(step);
        }

        public static int AutoField
        {
            get
            {
                return currIndex++;
            }
        }
        public static int AutoDisableField
        {
            get
            {
                return currDisableIndex++;
            }
        }

        public int ColumnsCount
        {
            get
            {
                return this.columnList.Count;
            }
        }

        public bool IsDisposed
        {
            get
            {
                return this.disposed;
            }
        }

        public int RowsCount
        {
            get
            {
                if (this.rowPKList.Count != 0)
                {
                    return this.rowPKList.Count;
                }
                return 0;
            }
        }
    }
}


