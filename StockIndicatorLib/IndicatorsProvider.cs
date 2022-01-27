using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace StockIndicatorLib
{
    public class IndicatorsProvider
    {
        public static int COLUMN_OPEN = 0;
        public static int COLUMN_HIGH = 1;
        public static int COLUMN_LOW = 2;
        public static int COLUMN_CLOSE = 3;
        public static int COLUMN_VOLUME = 4;
        public static int COLUMN_PRICEUP = 5;

        public readonly List<BaseKLine> kLines;
        private CTableEx dataSource = new CTableEx();
        public IndicatorsProvider(List<BaseKLine> kLines)
        {
            this.kLines = kLines;
            dataSource.AddColumn(COLUMN_OPEN);
            dataSource.AddColumn(COLUMN_HIGH);
            dataSource.AddColumn(COLUMN_LOW);
            dataSource.AddColumn(COLUMN_CLOSE);
            dataSource.AddColumn(COLUMN_VOLUME);
            dataSource.AddColumn(COLUMN_PRICEUP);
            ConvertKlineToDataSource(kLines, true);
        }


        public void ConvertKlineToDataSource(List<BaseKLine> list, bool clear)
        {
            CTableEx ds = this.dataSource;
            if (clear)
            {
                //indicators.Clear();
                ds.Clear();
                if (list == null) return;
            }

            int startIndex = ds.RowsCount;
            if (startIndex < 0)
            {
                startIndex = 0;
            }
            var beforeBar = list[0];
            for (int i = 0; i < list.Count; i++)
            {
                var data = list[i];
                if (!double.IsNaN(data.close))
                {
                    var timeLine = data.time.ToKlineTimestamp();
                    ds.Set(timeLine, COLUMN_VOLUME, data.volume / 10000);
                    int index = ds.GetRowIndex(timeLine);

                    ds.SetByColName(index, COLUMN_OPEN, data.open);
                    ds.SetByColName(index, COLUMN_HIGH, data.high);
                    ds.SetByColName(index, COLUMN_LOW, data.low);
                    ds.SetByColName(index, COLUMN_CLOSE, data.close);
                    var upratio = Math.Round(((data.close - beforeBar.close) / beforeBar.close) * 100, 2);
                    ds.SetByColName(index, COLUMN_PRICEUP, upratio);
                }
                beforeBar = data;
            }
        }

        public void AddIndicators(Type indicatorType)
        {
            var sldk = new Indicator_SlowKD(this.kLines, this.dataSource);
            sldk.SetParam(COLUMN_CLOSE, COLUMN_HIGH, COLUMN_LOW, 3, 3, 3, 3);
            sldk.OnCalculate(0);

            if (false)
            {
                foreach (var item in kLines)
                {
                    var indi = item.indicatorList.Find(f => f.indicator.GetType() == typeof(Indicator_SlowKD)) as SlowKDJ_Result;
                    Debug.WriteLine($"time:{item.time}  K: {indi.K}  D: {indi.D}");
                }
            }
        }


        public void CalcIndicatorsWhenDataUpdated()
        {
            var indcators = kLines.First().indicatorList.GroupBy(g => g.indicator).Select(s => s.Key).ToList();
            foreach (var ind in indcators)
            {
                var type = ind.GetType();
                if (type == typeof(Indicator_SlowKD))
                {
                    var indicator = ind as Indicator_SlowKD;
                    indicator.OnCalculate(kLines.Count - 3);
                }
            }
        }


    }

    public static class Ext
    {

        public static double ToKlineTimestamp(this DateTime dt)
        {
            return (dt - new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
