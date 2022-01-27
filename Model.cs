using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pancake_Pridction_KNN
{
    public enum BetSide
    {
        @null, BULL, BEAR
    }
    public class RoundLog
    {
        public decimal gasUsed { get; set; }

        public int roundID { get; set; }
        public double ClosePrice { get; set; }
        public double TotalAmount { get; set; }
        public double BullAmount { get; set; }
        public double BearAmount { get; set; }
        public BetSide BetSide { get; set; }
        public BetSide Result { get; set; }
        public double RewardAmount { get; set; }
        public bool Claimed { get; set; }
        public bool Winner { get; set; }
    }

    public class TestResultInfo
    {
        public double pnlRatio;
        public double pnlRatioAnnual;
        public double winRatio;
        public decimal bollPeriod;
        public decimal bollWidth;
        public string stockCode;
    }


    //    [
    //  [
    //    1499040000000,      // Open time
    //    "0.01634790",       // Open
    //    "0.80000000",       // High
    //    "0.01575800",       // Low
    //    "0.01577100",       // Close
    //    "148976.11427815",  // Volume
    //    1499644799999,      // Close time
    //    "2434.19055334",    // Quote asset volume
    //    308,                // Number of trades
    //    "1756.87402397",    // Taker buy base asset volume
    //    "28.46694368",      // Taker buy quote asset volume
    //    "17928899.62484339" // Ignore
    //  ]
    //]


    public class BinanceKLine
    {
        [JsonProperty(Order = 0)]
        public long openTime { get; set; }
        [JsonProperty(Order = 1)]
        public string open { get; set; }
        [JsonProperty(Order = 2)]
        public string high { get; set; }
        [JsonProperty(Order = 3)]
        public string low { get; set; }
        [JsonProperty(Order = 4)]
        public string close { get; set; }
        [JsonProperty(Order = 5)]
        public string volume { get; set; }
        [JsonProperty(Order = 6)]
        public long closeTime { get; set; }
        [JsonProperty(Order = 7)]
        public string QuoteAssetvolume { get; set; }
        [JsonProperty(Order = 8)]
        public long NumberIfTrades { get; set; }
        [JsonProperty(Order = 9)]
        public string NumberIfTrades1 { get; set; }
        [JsonProperty(Order = 10)]
        public string NumberIfTrades2 { get; set; }
        [JsonProperty(Order = 11)]
        public string NumberIfTrades3 { get; set; }
    }

    public class Bar_my
    {
        public string symbol;
        public DateTime time;
        public double open;
        public double close;
        public double high;
        public double low;
        public double volume;
        public double amount;
        public double preClose;
        public long position;
        public string frequency;

    }
}
