using System;

namespace StockIndicatorLib
{
    public class CRow : IDisposable
    {
        public CList<double> m_values;

        public CRow()
        {
            this.m_values = new CList<double>();
        }

        public CRow(int capacity, int step)
        {
            this.m_values = new CList<double>();
            this.m_values.set_capacity(capacity);
            this.m_values.set_step(capacity);
        }

        public CRow(double[] ary, int size)
        {
            this.m_values = new CList<double>();
            this.m_values.addranges(ary, size);
        }

        public void Clear()
        {
            if (this.m_values != null)
            {
                this.m_values.clear();
            }
        }

        public void Dispose()
        {
            if (this.m_values != null)
            {
                this.m_values.Dispose();
                this.m_values = null;
            }
        }

        public void FillEmpty(int columns)
        {
            int num = this.m_values.Count;
            if (num >= 0)
            {
                for (int i = num; i < columns; i++)
                {
                    this.m_values.insert(i, double.NaN);
                }
            }
        }

        ~CRow()
        {
            this.Dispose();
        }

        public double Get(int index)
        {
            return this.m_values.get(index);
        }

        public void Remove(int index)
        {
            this.m_values.remove_at(index);
        }

        public void Set(int index, double value)
        {
            this.m_values.set(index, value);
        }
    }
}


