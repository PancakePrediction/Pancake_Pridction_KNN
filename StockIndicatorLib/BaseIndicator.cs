
using System;
using System.Collections.Generic;

namespace StockIndicatorLib
{

    public class Chart
    {
        protected CTableEx dataSource = new CTableEx();
        public CTableEx DataSource
        {
            get
            {
                return this.dataSource;
            }
            set
            {
                this.dataSource = value;
            }
        }
    }
    public class BaseShape
    {

    }

    public abstract class BaseIndicator
    {
        private Chart _chart;
        private string _NameText = "";
        private string _DescText = "";

        private List<IndicatorPeriod> _PeriodList = new List<IndicatorPeriod>();
        private Dictionary<string, int> dictionary_0 = new Dictionary<string, int>();
        protected string[] fields;
        protected CTableEx dataSource = null;
        protected List<BaseKLine> kLines = null;
        protected BaseIndicator(List<BaseKLine> kLines,CTableEx dataSource)
        {
            this.kLines = kLines;
            this.dataSource = dataSource;
        }


        protected double GetDiffPercent(ref double lastValue, double value, double percent = 100)
        {
            if (double.IsNaN(lastValue)) lastValue = value;
            var B = value;
            var A = lastValue;
            double percentAgvValue = ((B - A) / A) * percent;
            if (A < 0 && B < 0) percentAgvValue = percentAgvValue * -1;
            else
            if (A < 0 && B > 0) percentAgvValue = percentAgvValue * -1;
            lastValue = value;
            return percentAgvValue;
        }


        public virtual double CalculateAvedev(int target, int cycle, double curValue, double maValue, int r)
        {
            if (!dataSource.ContainsColumn(target))
            {
                return 0.0;
            }
            int num2 = r - (cycle - 1);
            if (num2 < 0)
            {
                num2 = 0;
            }
            double num3 = Math.Abs((double)(curValue - maValue));
            for (int i = r - 1; i >= num2; i--)
            {
                double num5 = dataSource.Get2(i, target);
                num3 += Math.Abs((double)(num5 - maValue));
            }
            int num6 = cycle;
            if (r <= (cycle - 1))
            {
                num6 = r + 1;
            }
            return (num3 / ((double)num6));
        }

        public virtual double CalculateExponentialMovingAvg(int field, int target, int n, double value, int index)
        {
            if (!(dataSource.ContainsColumn(field) && dataSource.ContainsColumn(target)))
            {
                return 0.0;
            }
            double d = 0.0;
            if (index > 0)
            {
                d = dataSource.Get2(index - 1, field);
                if (double.IsNaN(d))
                {
                    d = 0.0;
                }
            }
            else
            {
                d = value;
            }
            return (((value * 2.0) + (d * (n - 1))) / ((double)(n + 1)));
        }

        public virtual double CalculateRawStochasticValue(int r, int kPeriods, int close, int high, int low)
        {
            int num = r - (kPeriods - 1);
            if (num < 0)
            {
                num = 0;
            }
            double num2 = dataSource.Get2(r, close);
            List<double> valueList = new List<double>();
            List<double> list2 = new List<double>();
            for (int i = r; i >= num; i--)
            {
                double item = dataSource.Get2(i, high);
                double num5 = dataSource.Get2(i, low);
                valueList.Add(item);
                list2.Add(num5);
            }
            double highValue = LbCommon.GetHighValue(valueList);
            double lowValue = LbCommon.GetLowValue(list2);
            if (!(highValue == lowValue))
            {
                return (((num2 - lowValue) / (highValue - lowValue)) * 100.0);
            }
            return 100.0;
        }

        public virtual double CalculateStandardDeviation(int target, int cycle, double standardDeviation, double targetValue, double agvValue, int r)
        {
            if (!dataSource.ContainsColumn(target))
            {
                return 0.0;
            }
            double num = (targetValue - agvValue) * (targetValue - agvValue);
            int num2 = r - (cycle - 1);
            if (num2 < 0)
            {
                num2 = 0;
            }
            for (int i = r - 1; i >= num2; i--)
            {
                double num4 = dataSource.Get2(i, target);
                num += (num4 - agvValue) * (num4 - agvValue);
            }
            return (standardDeviation * Math.Sqrt(num / ((double)(cycle - 1))));
        }

        public double CalculateTrueRange(int r, double curHigh, double curLow, int high, int low)
        {
            double num = 0.0;
            double num2 = 0.0;
            if (r > 0)
            {
                num = dataSource.Get2(r - 1, high);
                num2 = dataSource.Get2(r - 1, low);
            }
            return ((Math.Abs((double)(curHigh - num)) >= Math.Abs((double)(curLow - num2))) ? Math.Abs((double)(curHigh - num)) : Math.Abs((double)(curLow - num2)));
        }

        public virtual double CalcutaSumilationValue(int index, int field, int n, int target, double close)
        {
            double num = 0.0;
            int num2 = index - (n - 1);
            if (num2 < 0)
            {
                num2 = 0;
            }
            if (index <= (n - 1))
            {
                for (int i = index - 1; i >= num2; i--)
                {
                    num += dataSource.Get2(i, target);
                }
            }
            else if (index > (n - 1))
            {
                num = dataSource.Get2(index - 1, field) - dataSource.Get2(index - n, target);
            }
            return (num + close);
        }

        public double CalcuteMovingAvg(int index, int n, int field, int target, double close)
        {
            if (!(dataSource.ContainsColumn(field) && dataSource.ContainsColumn(target)))
            {
                return 0.0;
            }
            double num = 0.0;
            int num2 = index - (n - 1);
            if (num2 < 0)
            {
                num2 = 0;
            }
            if (index <= (n - 1))
            {
                for (int i = index - 1; i >= num2; i--)
                {
                    num += dataSource.Get2(i, target);
                }
                n = index + 1;
            }
            else if (index > (n - 1))
            {
                num = dataSource.Get2(index - 1, field) * n;
                num -= dataSource.Get2(index - n, target);
            }
            num += close;
            return (num / ((double)n));
        }

        public virtual double CalcuteSimpleMovingAvg(int index, int n, int weight, int field, int target, double value)
        {
            if (!(dataSource.ContainsColumn(field) && dataSource.ContainsColumn(target)))
            {
                return 0.0;
            }
            if (index <= (n - 1))
            {
                n = index + 1;
            }
            double num = 0.0;
            if (index > 0)
            {
                num = dataSource.Get2(index - 1, field);
            }
            return (((value * weight) + ((n - weight) * num)) / ((double)n));
        }

        //public abstract bool Contains(object obj);
        //public abstract List<BaseShape> GetShapeList();
        public virtual void OnCalculate(int r)
        {
        }

        public virtual double PeakValue(int r, int field, int cycle, int forward)
        {
            // This item is obfuscated and can not be translated.
            ;
            if (!dataSource.ContainsColumn(field))
            {
                return 0.0;
            }
            List<double> valueList = new List<double>();
            for (int i = r - forward; i < 0; i--)
            {
                Label_0032:
                if (0 == 0)
                {
                    return LbCommon.GetHighValue(valueList);
                }
                double item = dataSource.Get2(i, field);
                valueList.Add(item);
            }
            //goto Label_0032;
            return 0;
        }

        public abstract void Remove();
        public virtual double ValleyValue(int r, int field, int cycle, int forward)
        {
            // This item is obfuscated and can not be translated.
            ;
            if (!dataSource.ContainsColumn(field))
            {
                return 0.0;
            }
            List<double> valueList = new List<double>();
            for (int i = r - forward; i < 0; i--)
            {
                Label_0032:
                if (0 == 0)
                {
                    return LbCommon.GetLowValue(valueList);
                }
                double item = dataSource.Get2(i, field);
                valueList.Add(item);
            }
            //goto Label_0032;
            return 0;
        }

        public void SetDataSource(CTableEx ds)
        {
            this.dataSource = ds;
        }

        public Chart Chart
        {
            get
            {
                return this._chart;
            }
            set
            {
                this._chart = value;
            }
        }


        public string IndDesc
        {
            get
            {
                return this._DescText;
            }
            set
            {
                this._DescText = value;
            }
        }

        public string IndName
        {
            get
            {
                return this._NameText;
            }
            set
            {
                this._NameText = value;
            }
        }

        public List<IndicatorPeriod> IndPeriod
        {
            get
            {
                return this._PeriodList;
            }
            set
            {
                this._PeriodList = value;
            }
        }

        public Dictionary<string, int> ColumnIndex
        {
            get
            {
                return this.dictionary_0;
            }
            set
            {
                this.dictionary_0 = value;
            }
        }
    }
}


