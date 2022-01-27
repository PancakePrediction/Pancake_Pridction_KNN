using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace StockIndicatorLib
{

    public class SlowKDJ_Result : BaseIndicatorResult
    {
        public SlowKDJ_Result(BaseIndicator indicator) : base(indicator)
        {

        }
        public double K;
        public double D;
        public double KDRatio;
    }

    public class Indicator_SlowKD : BaseIndicator
    {
        private int _rsvField = CTableEx.AutoField;
        private int _fastField = CTableEx.AutoField;
        private int _K_Field = CTableEx.AutoField;
        private int _D_Field = CTableEx.AutoField;

        public Indicator_SlowKD(List<BaseKLine> kLines, CTableEx dataSource) : base(kLines, dataSource)
        {
            base.IndName = "SLOWKD";
            base.IndDesc = "";
            base.IndPeriod.Add(new IndicatorPeriod(0, "N", 9.0));
            base.IndPeriod.Add(new IndicatorPeriod(1, "M1", 3.0));
            base.IndPeriod.Add(new IndicatorPeriod(2, "M2", 3.0));
            base.IndPeriod.Add(new IndicatorPeriod(3, "M3", 3.0));


            dataSource.AddColumn(_K_Field);
            dataSource.AddColumn(_D_Field);
            dataSource.AddColumn(_rsvField);
            dataSource.AddColumn(_fastField);

        }


        public override void OnCalculate(int r)
        {
            var ds = base.dataSource;
            int rowsCount = ds.RowsCount;
            for (int i = r; i < rowsCount; i++)
            {
                double rsv = this.CalculateRawStochasticValue(i, (int)base.IndPeriod[0].ParamValue,
                    base.ColumnIndex["Close"],
                    base.ColumnIndex["High"],
                    base.ColumnIndex["Low"]);
                double fastK = this.CalcuteSimpleMovingAvg(i, (int)base.IndPeriod[1].ParamValue, 1, this._fastField, this._rsvField, rsv);
                double K = this.CalcuteSimpleMovingAvg(i, (int)base.IndPeriod[2].ParamValue, 1, this._K_Field, this._fastField, fastK);
                double D = this.CalcuteSimpleMovingAvg(i, (int)base.IndPeriod[3].ParamValue, 1, this._D_Field, this._K_Field, K);
                ds.SetByColName(i, this._rsvField, rsv);
                ds.SetByColName(i, this._fastField, fastK);
                ds.SetByColName(i, this._K_Field, K);
                ds.SetByColName(i, this._D_Field, D);
                kLines[i].indicatorList.Add(new SlowKDJ_Result(this)
                {
                    K = Math.Round(K, 2),
                    D = Math.Round(D, 2),
                    KDRatio = Math.Round(K / D, 2)
                });
            }
        }

        public override void Remove()
        {
            dataSource.RemoveColumn(this._rsvField);
            dataSource.RemoveColumn(this._fastField);
        }

        public void SetParam(int colClose, int colHigh, int colLow, double n, double m1, double m2, double m3)
        {
            base.IndPeriod[0].ParamValue = n;
            base.IndPeriod[1].ParamValue = m1;
            base.IndPeriod[2].ParamValue = m2;
            base.IndPeriod[3].ParamValue = m3;
            base.ColumnIndex["Close"] = colClose;
            base.ColumnIndex["High"] = colHigh;
            base.ColumnIndex["Low"] = colLow;
        }
    }


}
