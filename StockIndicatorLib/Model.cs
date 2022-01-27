using System;
using System.Collections.Generic;
using System.Text;

namespace StockIndicatorLib
{
    public class BaseKLine
    {
        public DateTime time { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double close { get; set; }
        public double volume { get; set; }
        public double change { get; set; }
        public string interval { get; set; }
        public bool Full { get; set; }

        public List<BaseIndicatorResult> indicatorList = new List<BaseIndicatorResult>();
    }

    public class Trade
    {
        public DateTime time { get; set; }
        public double price { get; set; }
        public double vol { get; set; }
        public bool buying { get; set; }
    }
}
