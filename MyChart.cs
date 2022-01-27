using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Piratecat.StockChart;

namespace Pancake_Pridction_KNN
{
    public partial class MyChart : Chart
    {
        public List<string> indicatorNames = new List<string>() {
                "BOLL",
                "SLOWKD",
                "MACD",
                "KDJ",
                "ASI",
                "ADTM",
                "ATR",
                "BBI",
                "BIAS",
                "CCI",
                "CHAIKIN",
                "DDI",
                "DMA",
                "DMI",
                "DPO",
                "EMA",
                "LWR",
                "MASS",
                "MTM",
                "NVI",
                "OBV",
                "OSC",
                "PBX",
                "PSY",
                "PVI",
                "ROC",
                "RSI",
                "SAR",
                "SD",
                "MA",
                "SMA",
                "TRIX",
                "VR",
                "WR",
                "WVAD"};
        public static int COLUMN_OPEN = 0;
        public static int COLUMN_HIGH = 1;
        public static int COLUMN_LOW = 2;
        public static int COLUMN_CLOSE = 3;
        public static int COLUMN_VOLUME = 4;
        public static int COLUMN_PRICEUP = 5200001;

        public static int COLUMN_BUY_STRONGER = 800001;
        public static int COLUMN_SMALL_BUY = 800002;
        public static int COLUMN_MIDDLE_BUY = 800003;
        public static int COLUMN_BIG_BUY = 800004;
        public static int COLUMN_EXBIG_BUY = 800005;


        public static int COLUMN_SELL_STRONGER = 800011;
        public static int COLUMN_SMALL_SELL = 800012;
        public static int COLUMN_MIDDLE_SELL = 800013;
        public static int COLUMN_BIG_SELL = 800014;
        public static int COLUMN_EXBIG_SELL = 800015;

        public static int COLUMN_BUY_MARK = 10000;
        public static int COLUMN_BUY_MARK_STYLE = 10001;
        public static int COLUMN_BUY_MARK_COLOR = 10002;
        public static int COLUMN_BUY_MARK_ADDTION = 10003;
        public static int COLUMN_SELL_MARK = 10011;
        public static int COLUMN_SELL_MARK_STYLE = 10012;
        public static int COLUMN_SELL_MARK_COLOR = 10013;
        public static int COLUMN_SELL_MARK_ADDTION = 10014;

        static int KlineStyleField = 1001;
        static int KlineColorField = 1002;
        static int CJLStyleField = 1003;
        static int CJLColorField = 1004;


        private List<BaseIndicator> indicators = new List<BaseIndicator>();
        CandleShape kLineCandle = null;

        public ChartDiv MainDiv;

        private ChartDiv indBase_Div;



        public BaseIndicator baseIndi;

        public IndicatorBuyStronger indBuyStronger;
        public IndicatorSellStronger indSellStronger;
        public IndicatorMomentumIndex indMTM;
        public IndicatorBollingerBands indicatorMainDiv_Boll;



        private ChartDiv volumeDiv;


        int Digit = 3;
        public MyChart(Form parentForm, string xFormat = null, int boll_days = 20, int boll_width = 2, int VolDivHeight = 15) : base(parentForm)
        {
            if (xFormat == null) xFormat = "MM-dd HH:mm:ss";
            InitializeComponent();
            AutoCapureCandle = true;
            AllowDrag = true;
            DataSource.SetColsCapacity(40);
            IsFengShiLine = false;
            AutoFillXScale = true;
            ScrollAddSpeed = true;
            XFieldText = "Date";
            CanMoveShape = false;
            LeftYScaleWidth = 80;
            RightYScaleWidth = 80;
            XScalePixel = 11;
            MainDiv = AddChartDiv(60);
            MainDiv.Title = "Day";
            MainDiv.XScale.Visible = true;
            MainDiv.PaddingBottom = 10;
            MainDiv.PaddingTop = 10;
            //mainDiv.LeftYScale.System = VScaleSystem.Logarithmic;
            MainDiv.RightYScale.ScaleType = YScaleType.Percent;
            MainDiv.LeftYScale.Digit = Digit;
            MainDiv.RightYScale.Digit = Digit;
            if (xFormat != null)
                MainDiv.XScale.Format = xFormat;

            kLineCandle = AddCandle("KLine", COLUMN_OPEN, COLUMN_HIGH, COLUMN_LOW, COLUMN_CLOSE, COLUMN_PRICEUP, MainDiv);
            kLineCandle.UpColor = Color.FromArgb(80, 255, 255);
            kLineCandle.DownColor = Color.FromArgb(255, 80, 80);
            kLineCandle.CandleStyle = CandleStyle.CloseLine;
            kLineCandle.StyleField = KlineStyleField;
            kLineCandle.ColorField = KlineColorField;
            kLineCandle.Digit = Digit;

            DataSource.AddColumn(kLineCandle.StyleField);
            DataSource.AddColumn(kLineCandle.ColorField);


            indicatorMainDiv_Boll = (IndicatorBollingerBands)AddIndicator("BOLL");
            indicatorMainDiv_Boll.SetParam(COLUMN_CLOSE, boll_days, boll_width, MainDiv);
            indicators.Add(indicatorMainDiv_Boll);


            volumeDiv = AddChartDiv(VolDivHeight);
            volumeDiv.XScale.Visible = false;
            volumeDiv.LeftYScale.Magnitude = 10000;
            volumeDiv.RightYScale.Magnitude = 10000;
            volumeDiv.LeftYScale.Digit = 0;
            volumeDiv.RightYScale.Digit = 0;

            BarShape barShape = AddBar("Volume", COLUMN_VOLUME, volumeDiv);
            barShape.Title = "Volume";
            barShape.BarStyle = BarStyle.Bar;
            barShape.Digit = 0;
            barShape.StyleField = CJLStyleField;
            barShape.ColorField = CJLColorField;
            DataSource.AddColumn(barShape.StyleField);
            DataSource.AddColumn(barShape.ColorField);
            SetBar("Volume", System.Drawing.Color.FromArgb(255, 255, 80), System.Drawing.Color.FromArgb(125, 206, 235));



            indBase_Div = AddChartDiv(20);
            indBase_Div.Digit = 3;
            indBase_Div.XScale.Format = xFormat;
            indBase_Div.IsMain = false;
            indBase_Div.GridInterval = 1;
            indBase_Div.XScale.Visible = false;
            indBase_Div.Title = "Indicator";
            //baseIndi = SetIndicator("MACD", indBase_Div);

            baseIndi = SetIndicator("SLOWKD", indBase_Div);

        }

        public void ChangeIndicator(string IndicatorName)
        {
            DeleteIndicator(baseIndi);
            indicators.Remove(baseIndi);
            baseIndi = SetIndicator(IndicatorName, indBase_Div);
        }

        public MyChart(IContainer container) : base(null)
        {
            container.Add(this);

            InitializeComponent();
        }

        public static string addtion = "myaddtion";
        public void SetTradeShape(int col, bool buy = true, string addtion = "", bool refresh = false)
        {
            if (col == -1)
                col = dataSource.RowsCount - 1;
            if (buy)
            {
                DataSource.SetByColName(col, COLUMN_BUY_MARK, DataSource.Get2(col, COLUMN_CLOSE));
                DataSource.SetByColName(col, COLUMN_BUY_MARK_STYLE, (int)LineStyle.BuyMark);
                DataSource.SetByColName(col, COLUMN_BUY_MARK_COLOR, Color.FromArgb(255, 80, 80).ToArgb());

                var ptr = Marshal.StringToHGlobalAnsi(addtion);
                DataSource.SetByColName(col, COLUMN_BUY_MARK_ADDTION, (long)ptr);
            }
            else
            {
                DataSource.SetByColName(col, COLUMN_SELL_MARK, DataSource.Get2(col, COLUMN_OPEN));
                DataSource.SetByColName(col, COLUMN_SELL_MARK_STYLE, (int)LineStyle.SellMark);
                DataSource.SetByColName(col, COLUMN_SELL_MARK_COLOR, Color.FromArgb(80, 255, 80).ToArgb());
                var ptr = Marshal.StringToHGlobalAnsi(addtion);
                DataSource.SetByColName(col, COLUMN_SELL_MARK_ADDTION, (long)ptr);
            }
            if (refresh)
                RefreshGraph();

        }

        public void AddKDJIndicator(CTableEx ds, ChartPeriodType periodType)
        {
            IndicatorMyKDJ ind = null;
            ChartDiv div;

            switch (periodType)
            {
                case ChartPeriodType.FS:
                    div = AddChartDiv(16);
                    div.IsMain = true;
                    div.XScale.Visible = false;
                    div.Title = "KDJ realtime";
                    ind = (IndicatorMyKDJ)SetIndicator("KDJFS", div, ds);
                    break;
                case ChartPeriodType.M1:
                    div = AddChartDiv(16);
                    div.IsMain = true;
                    div.XScale.Visible = false;
                    div.Title = "KDJ M1";
                    ind = (IndicatorMyKDJ)SetIndicator("KDJM1", div, ds);
                    break;
                case ChartPeriodType.M5:
                    div = AddChartDiv(16);
                    div.IsMain = true;
                    div.XScale.Visible = false;
                    div.Title = "KDJ M5";
                    ind = (IndicatorMyKDJ)SetIndicator("KDJM5", div, ds);
                    break;
                case ChartPeriodType.M15:
                    div = AddChartDiv(16);
                    div.IsMain = true;
                    div.XScale.Visible = false;
                    div.Title = "KDJ M15";
                    ind = (IndicatorMyKDJ)SetIndicator("KDJM15", div, ds);
                    break;
                case ChartPeriodType.M30:
                    div = AddChartDiv(16);
                    div.IsMain = true;
                    div.XScale.Visible = false;
                    div.Title = "KDJ M30";
                    ind = (IndicatorMyKDJ)SetIndicator("KDJM30", div, ds);
                    break;
                case ChartPeriodType.M60:
                    div = AddChartDiv(16);
                    div.IsMain = true;
                    div.XScale.Visible = false;
                    div.Title = "KDJ M60";
                    ind = (IndicatorMyKDJ)SetIndicator("KDJM60", div, ds);
                    break;
                case ChartPeriodType.D1:
                    div = AddChartDiv(16);
                    div.IsMain = true;
                    div.XScale.Visible = false;
                    div.Title = "KDJ day";
                    ind = (IndicatorMyKDJ)SetIndicator("KDJD1", div, ds);
                    break;
                default:
                    throw new Exception("unknow");
            }
            div.Digit = 1;
            div.XScale.Format = "MM-dd HH:mm:ss";
        }




        public void UpdateKline(List<Bar_my> list, bool clear, bool update = true)
        {
            CTableEx ds = this.dataSource;
            if (clear)
            {
                //indicators.Clear();
                ds.Clear();
                if (list == null) return;
            }
            BarShape barVolume = GetShape("Volume") as BarShape;
            CandleShape candleShape = GetShape("KLine") as CandleShape;
            var downColor = System.Drawing.Color.FromArgb(80, 255, 255).ToArgb();
            var upColor = System.Drawing.Color.FromArgb(255, 80, 80).ToArgb();
            int startIndex = ds.RowsCount;
            if (startIndex < 0)
            {
                startIndex = 0;
            }
            Bar_my beforeBar = list[0];
            for (int i = 0; i < list.Count; i++)
            {
                Bar_my data = list[i];
                if (!double.IsNaN(data.close))
                {
                    var timeLine = data.time.ToKlineTimestamp();
                    ds.Set(timeLine, COLUMN_VOLUME, data.volume / 1000);
                    int index = ds.GetRowIndex(timeLine);

                    ds.SetByColName(index, COLUMN_OPEN, data.open);
                    ds.SetByColName(index, COLUMN_HIGH, data.high);
                    ds.SetByColName(index, COLUMN_LOW, data.low);
                    ds.SetByColName(index, COLUMN_CLOSE, data.close);
                    var upratio = Math.Round(((data.close - beforeBar.close) / beforeBar.close) * 100, 2);
                    ds.SetByColName(index, COLUMN_PRICEUP, upratio);
                    if (data.open > data.close)
                    {

                        ds.SetByColName(index, barVolume.StyleField, 0);
                        ds.SetByColName(index, barVolume.ColorField, downColor);

                        ds.SetByColName(index, candleShape.StyleField, 0);
                    }
                    else
                    {

                        ds.SetByColName(index, barVolume.StyleField, 1);
                        ds.SetByColName(index, barVolume.ColorField, upColor);

                        ds.SetByColName(index, candleShape.StyleField, 1);
                    }
                }
                beforeBar = data;
            }

            int indicatorsSize = indicators.Count;
            for (int i = 0; i < indicatorsSize; i++)
            {
                indicators[i].OnCalculate(startIndex);
            }
            if (update)
            {
                RefreshGraph();
                System.Windows.Forms.Application.DoEvents();
            }
        }


        public void AppendKline(Bar_my data, bool update = true, bool calcIndi = true)
        {
            CTableEx ds = this.dataSource;

            BarShape barVolume = GetShape("Volume") as BarShape;
            CandleShape candleShape = GetShape("KLine") as CandleShape;
            var downColor = System.Drawing.Color.FromArgb(80, 255, 255).ToArgb();
            var upColor = System.Drawing.Color.FromArgb(255, 80, 80).ToArgb();

            int indexBefore = ds.RowsCount - 1;
            if (indexBefore < 0) indexBefore = 0;
            int startIndex = ds.RowsCount;
            if (startIndex < 0) startIndex = 0;


            double klineOpen = 0;
            if (ds.RowsCount > 0)
                klineOpen = ds.Get2(indexBefore, COLUMN_OPEN);

            int Currindex = ds.RowsCount - 1;

            if (!double.IsNaN(data.close))
            {
                var timeLine = data.time.ToKlineTimestamp();
                ds.Set(timeLine, COLUMN_VOLUME, data.volume / 1000);
                Currindex = ds.GetRowIndex(timeLine);

                ds.SetByColName(Currindex, COLUMN_OPEN, data.open);
                ds.SetByColName(Currindex, COLUMN_HIGH, data.high);
                ds.SetByColName(Currindex, COLUMN_LOW, data.low);
                ds.SetByColName(Currindex, COLUMN_CLOSE, data.close);
                var upratio = Math.Round(((data.close - klineOpen) / klineOpen) * 100, 2);
                ds.SetByColName(Currindex, COLUMN_PRICEUP, upratio);

                if (data.open > data.close)
                {

                    ds.SetByColName(Currindex, barVolume.StyleField, 0);
                    ds.SetByColName(Currindex, barVolume.ColorField, downColor);

                    ds.SetByColName(Currindex, candleShape.StyleField, 0);
                }
                else
                {

                    ds.SetByColName(Currindex, barVolume.StyleField, 1);
                    ds.SetByColName(Currindex, barVolume.ColorField, upColor);

                    ds.SetByColName(Currindex, candleShape.StyleField, 1);
                }
            }

            if (calcIndi)
            {
                int indicatorsSize = indicators.Count;
                for (int i = 0; i < indicatorsSize; i++)
                {
                    indicators[i].OnCalculate(Currindex);
                }
            }

            if (update)
            {
                //FirstVisibleIndex = 0;
                LastVisibleIndex = ds.RowsCount;
                RefreshGraph();
                //System.Windows.Forms.Application.DoEvents();
            }
        }



        public BaseIndicator SetIndicator(String text, ChartDiv div, CTableEx ds = null)
        {
            DeleteIndicator(text);
            BaseIndicator indicator = AddIndicator(text, ds);
            switch (text)
            {
                case "BuyStronger":
                    (indicator as IndicatorSellStronger).SetParam(COLUMN_SELL_STRONGER, COLUMN_SMALL_SELL, COLUMN_MIDDLE_SELL, COLUMN_BIG_SELL, COLUMN_EXBIG_SELL, div);
                    break;
                case "SellStronger":
                    (indicator as IndicatorBuyStronger).SetParam(COLUMN_BUY_STRONGER, COLUMN_SMALL_BUY, COLUMN_MIDDLE_BUY, COLUMN_BIG_BUY, COLUMN_EXBIG_BUY, div);
                    break;
                case "BOLL":
                    (indicator as IndicatorBollingerBands).SetParam(COLUMN_CLOSE, 20, 2, div);
                    break;
                case "VolKDJ":
                    (indicator as Indicators_VKDJ).SetParam(COLUMN_CLOSE, COLUMN_HIGH, COLUMN_LOW, COLUMN_VOLUME, 9, 3, 3, div);
                    break;
                case "KDJ":
                    (indicator as IndicatorStochasticOscillator).SetParam(COLUMN_CLOSE, COLUMN_HIGH, COLUMN_LOW, 9, 3, 3, div);
                    break;
                case "KDJFS":
                    (indicator as IndicatorMyKDJ).SetParam(COLUMN_CLOSE, COLUMN_HIGH, COLUMN_LOW, 9, 3, 3, div, ChartPeriodType.FS);
                    break;
                case "KDJM1":
                    (indicator as IndicatorMyKDJ).SetParam(COLUMN_CLOSE, COLUMN_HIGH, COLUMN_LOW, 9, 3, 3, div, ChartPeriodType.M1);
                    break;
                case "KDJM5":
                    (indicator as IndicatorMyKDJ).SetParam(COLUMN_CLOSE, COLUMN_HIGH, COLUMN_LOW, 9, 3, 3, div, ChartPeriodType.M5);
                    break;
                case "KDJM15":
                    (indicator as IndicatorMyKDJ).SetParam(COLUMN_CLOSE, COLUMN_HIGH, COLUMN_LOW, 9, 3, 3, div, ChartPeriodType.M15);
                    break;
                case "KDJM30":
                    (indicator as IndicatorMyKDJ).SetParam(COLUMN_CLOSE, COLUMN_HIGH, COLUMN_LOW, 9, 3, 3, div, ChartPeriodType.M30);
                    break;
                case "KDJM60":
                    (indicator as IndicatorMyKDJ).SetParam(COLUMN_CLOSE, COLUMN_HIGH, COLUMN_LOW, 9, 3, 3, div, ChartPeriodType.M60);
                    break;
                case "KDJD1":
                    (indicator as IndicatorMyKDJ).SetParam(COLUMN_CLOSE, COLUMN_HIGH, COLUMN_LOW, 9, 3, 3, div, ChartPeriodType.D1);
                    break;
                case "ASI":
                    (indicator as IndicatorAccumulationSwingIndex).SetParam(COLUMN_OPEN, COLUMN_CLOSE, COLUMN_HIGH, COLUMN_LOW, 6, div);
                    break;
                case "ADTM":
                    (indicator as IndicatorADTM).SetParam(COLUMN_OPEN, COLUMN_HIGH, COLUMN_LOW, 23, 8, div);
                    break;
                case "ATR":
                    (indicator as IndicatorAverageTrueRange).SetParam(COLUMN_HIGH, COLUMN_LOW, 14, div);
                    break;
                case "BBI":
                    (indicator as IndicatorBullandBearIndex).SetParam(COLUMN_CLOSE, 3, 6, 12, 24, div);
                    break;
                case "BIAS":
                    (indicator as IndicatorBIAS).SetParam(COLUMN_CLOSE, 6, div);
                    break;
                case "CCI":
                    (indicator as IndicatorCommodityChannelIndex).SetParam(COLUMN_CLOSE, COLUMN_HIGH, COLUMN_LOW, 14, div);
                    break;
                case "CHAIKIN":
                    (indicator as IndicatorChaikinOscillator).SetParam(COLUMN_CLOSE, COLUMN_HIGH, COLUMN_LOW, COLUMN_VOLUME, 10, 20, 6, div);
                    break;
                case "DDI":
                    (indicator as IndicatorDirectionDeviationIndex).SetParam(COLUMN_HIGH, COLUMN_LOW, 13, 30, 10, 5, div);
                    break;
                case "DMA":
                    (indicator as IndicatorDifferentOfMovingAverage).SetParam(COLUMN_CLOSE, 10, 50, 10, div);
                    break;
                case "DMI":
                    (indicator as IndicatorDirectionalMovementIndex).SetParam(COLUMN_CLOSE, COLUMN_HIGH, COLUMN_LOW, 14, 6, div);
                    break;
                case "DPO":
                    (indicator as IndicatorDetrendedPriceOscillator).SetParam(COLUMN_CLOSE, 20, 11, 6, div);
                    break;
                case "EMA":
                    (indicator as IndicatorExponentialMovingAverage).SetParam(COLUMN_CLOSE, 5, div);
                    break;
                case "LWR":
                    (indicator as IndicatorLWR).SetParam(COLUMN_CLOSE, COLUMN_HIGH, COLUMN_LOW, 9, 3, 3, div);
                    break;
                case "MACD":
                    (indicator as IndicatorMACD).SetParam(COLUMN_CLOSE, 12, 26, 9, div);
                    break;
                case "MASS":
                    (indicator as IndicatorMassIndex).SetParam(COLUMN_HIGH, COLUMN_LOW, 25, 9, div);
                    break;
                case "MTM":
                    (indicator as IndicatorMomentumIndex).SetParam(COLUMN_CLOSE, 12, 6, div);
                    break;
                case "NVI":
                    (indicator as IndicatorNegativeVolumeIndex).SetParam(COLUMN_VOLUME, COLUMN_CLOSE, 72, div);
                    break;
                case "OBV":
                    (indicator as IndicatorOnBalanceVolume).SetParam(COLUMN_CLOSE, COLUMN_VOLUME, div);
                    break;
                case "OSC":
                    (indicator as IndicatorOscillator).SetParam(COLUMN_CLOSE, 10, 6, div);
                    break;
                case "PBX":
                    (indicator as IndicatorPBX).SetParam(COLUMN_CLOSE, 4, div);
                    break;
                case "PSY":
                    (indicator as IndicatorPsychologicalLine).SetParam(COLUMN_CLOSE, 12, div);
                    break;
                case "PVI":
                    (indicator as IndicatorPositiveVolumeIndex).SetParam(COLUMN_VOLUME, COLUMN_CLOSE, 72, div);
                    break;
                case "ROC":
                    (indicator as IndicatorRateOfChange).SetParam(COLUMN_CLOSE, 12, 6, div);
                    break;
                case "RSI":
                    (indicator as IndicatorRelativeStrengthIndex).SetParam(COLUMN_CLOSE, 6, div);
                    break;
                case "SAR":
                    (indicator as IndicatorStopAndReveres).SetParam(COLUMN_HIGH, COLUMN_LOW, 4, 2, 20, div);
                    break;
                case "SD":
                    (indicator as IndicatorStandardDeviation).SetParam(COLUMN_CLOSE, 14, 2, div);
                    break;
                case "SLOWKD":
                    (indicator as IndicatorSlowStochasticOscillator).SetParam(COLUMN_CLOSE, COLUMN_HIGH, COLUMN_LOW, 3, 3, 3, 3, div);
                    break;
                case "MA":
                    (indicator as IndicatorMovingAverage).SetParam(COLUMN_CLOSE, div);
                    break;
                case "SMA":
                    (indicator as IndicatorSimpleMovingAverage).SetParam(COLUMN_CLOSE, 5, 1, div);
                    break;
                case "TRIX":
                    (indicator as IndicatorTripleExponentiallySmoothedMovingAverage).SetParam(COLUMN_CLOSE, 12, 12, div);
                    break;
                case "VR":
                    (indicator as IndicatorVolumeRatio).SetParam(COLUMN_CLOSE, COLUMN_VOLUME, 26, 6, div);
                    break;
                case "WR":
                    (indicator as IndicatorWilliamsAndRate).SetParam(COLUMN_CLOSE, COLUMN_HIGH, COLUMN_LOW, 10, div);
                    break;
                case "WVAD":
                    (indicator as IndicatorWVAD).SetParam(COLUMN_VOLUME, COLUMN_CLOSE, COLUMN_HIGH, COLUMN_LOW, COLUMN_OPEN, 24, 6, div);
                    break;
            }
            indicators.Add(indicator);
            try
            {
                indicator.OnCalculate(0);

            }
            catch (Exception)
            {
                indicators.Remove(indicator);
            }
            RefreshGraph();

            return indicator;
        }

        public class KLineInfo
        {
            public DateTime time;
            public double Change;
            public double UpShadowLine;
            public double DownShalowLine;
            public double BoxLine;
            public double ClosePrice;
            public double BollMiddle;
            public double OnBollMiddle;
            public double BollWidth;
            public double BollChange;
            public double Change10m;
            public double Change15m;
            public double Change20m;
            public double Change25m;
        }



        public KLineInfo GetKLineInfo(int countDownIndex = 1)
        {
            int index = dataSource.RowsCount - countDownIndex;
            var time = dataSource.GetRowPk(index);
            var close = dataSource.Get2(index, COLUMN_CLOSE);
            var high = dataSource.Get2(index, COLUMN_HIGH);
            var open = dataSource.Get2(index, COLUMN_OPEN);
            var low = dataSource.Get2(index, COLUMN_LOW);
            var bollResult1 = indicatorMainDiv_Boll.GetResult(countDownIndex);
            var bollResult2 = indicatorMainDiv_Boll.GetResult(countDownIndex + 1);
            var bollMiddle = bollResult1.Middle;
            var bollMiddleChange = bollResult2.Middle.GrowUp(bollResult1.Middle);
            var total = high - low;
            var info = new KLineInfo();
            double onBollSide;

            if (close < bollMiddle)
            {
                var height = bollMiddle - bollResult1.Lower;
                var over = bollMiddle - close;
                onBollSide = -(1 - (height - over) / height) * 100;
            }
            else
            {
                var height = bollResult1.Upper - bollMiddle;
                var over = close - bollMiddle;
                onBollSide = (1 - (height - over) / height) * 100;
            }

            var change10m = dataSource.Get2(index - 1, COLUMN_CLOSE).GrowUp(close);
            var change15m = dataSource.Get2(index - 2, COLUMN_CLOSE).GrowUp(close);
            var change20m = dataSource.Get2(index - 3, COLUMN_CLOSE).GrowUp(close);
            var change25m = dataSource.Get2(index - 4, COLUMN_CLOSE).GrowUp(close);

            info.Change10m = change10m;
            info.Change15m = change15m;
            info.Change20m = change20m;
            info.Change25m = change25m;
            info.BollChange = bollMiddleChange;
            info.BollWidth = bollResult1.Upper - bollResult1.Lower;
            info.OnBollMiddle = onBollSide;
            info.time = new DateTime().FromKlineTimestamp((long)time);
            info.Change = ((close - open) / open) * 100;
            info.ClosePrice = close;
            info.BollMiddle = bollMiddle;
            if (info.Change < 0)
            {
                var upline = high - open;
                var downline = close - low;
                info.UpShadowLine = (1 - (total - upline) / total) * 100;
                info.BoxLine = (1 - (total - Math.Abs(close - open)) / total) * 100;
                info.DownShalowLine = ((1 - (total - downline) / total) * 100);
            }
            else
            {
                var upline = high - close;
                var downline = open - low;
                info.UpShadowLine = (1 - (total - upline) / total) * 100;
                info.BoxLine = (1 - (total - Math.Abs(close - open)) / total) * 100;
                info.DownShalowLine = ((1 - (total - downline) / total) * 100);
            }

            return info;
        }

    }

    public static class ExtMyChart
    {
        public static double ToKlineTimestamp(this DateTime dt)
        {
            return (dt - new DateTime(1970, 1, 1)).TotalSeconds;
        }
        public static DateTime FromKlineTimestamp(this DateTime dt, long seconds)
        {
            return new DateTime(1970, 1, 1).AddSeconds(seconds);
        }

    }



}
