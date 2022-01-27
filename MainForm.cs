using ML_Accord_KNN;
using Newtonsoft.Json.Linq;
using Pancake_Pridction_KNN.Contract;
using Piratecat.StockChart;
using SharedModels;
using StockIndicatorLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;//https://github.com/PingmanTools/websocket-sharp/

namespace Pancake_Pridction_KNN
{
    public partial class MainForm : Form
    {
        public MyChart chartBNB_M5;
        public MyChart chartBNB_M5True;
        public static MainForm self;
        Common.Logging.ILog _log;

        List<BaseKLine> klineList_5m = null;
        List<BaseKLine> klineList_5mTrue = new List<BaseKLine>();
        IndicatorsProvider inds = null;
        GameController game=null;
        const int KlineCountdownTimeInSecounds = 40;
        double LastClosePrice = 0;
        int CurrentRoundID = 0;
        public MainForm()
        {
            _log = Program.logger;
            self = this;
            Config.ReadConfig();
            if (!string.IsNullOrEmpty(Config.cfg.DbServerName))
            {
                SqlLib.SQL_Info.DbServerName = Config.cfg.DbServerName;
                SqlLib.SQL_Info.dbName = Config.cfg.dbName;
                SqlLib.SQL_Info.dbUser = Config.cfg.dbUser;
                SqlLib.SQL_Info.dbPassword = Config.cfg.dbPassword;
            }
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            chartBNB_M5 = new MyChart(this,"HH:mm/MM-dd", 5, 2);
            chartBNB_M5True = new MyChart(this,"HH:mm/MM-dd", 5, 2);
            InitializeComponent();

            chartBNB_M5.Dock = DockStyle.Fill;
            chartBNB_M5.ContextMenuStrip = contextMenuStrip1;
            chartBNB_M5.AutoFillXScale = false;
            BNB_Panel.Controls.Add(chartBNB_M5);


            chartBNB_M5True.Dock = DockStyle.Fill;
            chartBNB_M5True.ContextMenuStrip = contextMenuStrip1;
            chartBNB_M5True.AutoFillXScale = false;
            panel_min5True.Controls.Add(chartBNB_M5True);

            foreach (var item in chartBNB_M5.indicatorNames)
            {
                var tsItem = new ToolStripMenuItem();
                tsItem.Text = item;
                tsItem.Click += new EventHandler((s, e) =>
                {
                    ChangeIndicatorToolStripMenuItem_Click(s, e);
                });
                this.IndicatorsToolStripMenuItem.DropDownItems.Add(tsItem);
            }

            for (int i = 0; i < 10; i++)
                pankouList.Add(new PankouInfo());
        }


        private async void MainForm_Load(object sender, EventArgs e)
        {
            //var ws = new WebSocket("wss://bsc.getblock.io/mainnet/?api_key=f12644f7-c8e9-443e-a303-f9949c8fda88");
            //ws.Connect();

            bet_Amount_BNB_tbox.Text = Config.cfg.betAmountInBNB.ToString("f4");
            IntervalForOrders_checkbox_CheckedChanged(null, null);

            game = new GameController();
            await game.StartPrediction();

            Log.ShowInUI("GameController Started.");
            game.OnEvent += (ss, ee) =>
             {
                 switch (ee.Type)
                 {
                     case GameController.ConEventType.NewRoundStarted:
                         {
                             CurrentRoundID = ee.RoundID;
                             _log.Info($"New round ID:  {CurrentRoundID}");
                             m5KlineStartTime = DateTime.Now.AddSeconds(300 - KlineCountdownTimeInSecounds);
                             IsThisRoundBetted = false;
                             IsThisRoundAutoBetted = false;
                             CheckingToBet = false;
                         }
                         break;
                     case GameController.ConEventType.EndRound:
                         {
                             LastClosePrice = ee.price;
                             var autobetLog = AutoBetLogList.Find(f => f.RoundID == ee.RoundID - 2);
                             if (autobetLog != null)
                                 autobetLog.Win = ee.Win;

                             if (label_lockPrice.InvokeRequired)
                             {
                                 label_lockPrice.Invoke(new Action(() =>
                                 {
                                     label_lockPrice.Text = $"{LastClosePrice:f2}";
                                 }));
                             }
                             else
                                 label_lockPrice.Text = $"{LastClosePrice:f2}";

                             if (Prediction_Table.Objects != null)
                             {
                                 var pres = Prediction_Table.Objects.Cast<PredictionItem>().ToList();
                                 var find = pres.Find(f => f.RoundID == ee.RoundID - 2);
                                 if (find != null)
                                 {
                                     find.Result = ee.RoundResult;
                                     Prediction_Table.UpdateObject(find);
                                 }
                             }
                         }
                         break;
                     case GameController.ConEventType.StopAutoBet:
                         {

                         }
                         break;
                     default:
                         break;
                 }
             };

            StartBalanceBNB = await game.GetBalance_BNB();

            //Fix the countdown time.
            var roundsOutputDTO = await game.GetRoundInfo(System.Numerics.BigInteger.Zero);
            game.CurrentRoundStartTime = new DateTime().FromPancakeTimeStamp((long)roundsOutputDTO.StartTimestamp);


            m5KlineStartTime = game.CurrentRoundStartTime.AddSeconds(300 - KlineCountdownTimeInSecounds);



            #region Get Kline from binance api
            klineList_5m = GetBNB_Kline_Indicator(500, "5m");
            inds = new IndicatorsProvider(klineList_5m);
            inds.AddIndicators(null);

            var barList = klineList_5m.ToBarList();
            chartBNB_M5.UpdateKline(barList, true, true);
            chartBNB_M5True.UpdateKline(barList, true, true);
            #endregion


            //subscrib trades from binance server
            GoBNB_WebsocketSubscrib(Config.cfg.Proxy);

            timerForTickDown.Enabled = true;

            return;
        }

        [DllImport("user32.dll")]
        public static extern bool FlashWindow(
            IntPtr hWnd,     // handle to window
            bool bInvert   // flash status
             );

        DateTime targetTime = DateTime.Now.AddDays(1);

        DateTime oldRoundStartTime = DateTime.Now.AddDays(1);

        bool IsThisRoundBetted = false;
        bool IsThisRoundAutoBetted = false;

        long LastTradesTime = 0;
        double remainSeconds = 0;

        PancakeKNN_V3 pancakeKNNV3 = null;
        public class PredictionItem
        {
            public int RoundID;
            public BetSide Prediction;
            public BetSide Result;
            public string Time;
        }

        int predictionRoundID = 0;
        private void timerForTickDown_Tick(object sender, EventArgs e)
        {
            label_TimeTickDown.ForeColor = Color.DeepSkyBlue;
            label_TimeTickDown.Refresh();

            var tmpTime = game.CurrentRoundStartTime.AddMinutes(5).AddSeconds(10);
            if ((tmpTime - targetTime).TotalSeconds < 0)
            {
                targetTime = tmpTime;
            }

            targetTime = targetTime.AddSeconds(-1);

            var ts = tmpTime - DateTime.Now;

            label_TimeTickDown.Text = $"{ts.Minutes:D2}:{ts.Seconds:D2}";
            remainSeconds = ts.TotalSeconds;
            if (ts.TotalSeconds < 60)
            {
                Thread.Sleep(300);
                label_TimeTickDown.ForeColor = Color.Red;
                if (this.TopLevel == false)
                {
                    FlashWindow(this.Handle, true);
                }
            }

            var chainlinkPriceOffset = ShowPriceOffset();


            label_Balance.Text = $"Balance: {GameController.balanceBNB:f2}";

            var KNN_AutobetEnabled = EnableAutobet_chkbox.Checked;
            if (IsThisRoundAutoBetted == false && !string.IsNullOrEmpty(Config.cfg.DbServerName))
            {
                var DataFeedWorking = LastTradesTime != LatestTradesTime;


                if (ts.TotalSeconds >= 18 && ts.TotalSeconds <= 20 && DataFeedWorking)
                {
                    if (Math.Abs(chainlinkPriceOffset) >= 0.9)
                    {
                        IsThisRoundAutoBetted = true;
                        var side = chainlinkPriceOffset > 0 ? BetSide.BULL : BetSide.BEAR;
                        var betResult = game.ManualBet(side, 2, QuickBet: true).Result;
                        Log.ShowInUI($"money coming, chainlinkPriceOffset:{chainlinkPriceOffset:f2}  Bet {side}");
                    }
                }

                if (ts.TotalSeconds >= 18 && ts.TotalSeconds <= 23)
                {
                    if (DataFeedWorking && CheckingToBet == false)
                    {
                        CheckingToBet = true;
                        LastTradesTime = LatestTradesTime;
                        var slowKDJ = chartBNB_M5.baseIndi as Piratecat.StockChart.IndicatorSlowStochasticOscillator;
                        if (slowKDJ == null) return;

                        var klineInfo = chartBNB_M5.GetKLineInfo(2);
                        var klineInfo3 = chartBNB_M5.GetKLineInfo(3);
                        var kdInfo2 = slowKDJ.GetKDRatio(2);
                        var kdInfo3 = slowKDJ.GetKDRatio(3);
                        var kdInfo4 = slowKDJ.GetKDRatio(4);
                        var KDRatioChange1 = kdInfo2.KDRatio.Percent(kdInfo3.KDRatio);
                        var KDRatioChange2 = kdInfo3.KDRatio.Percent(kdInfo4.KDRatio);
                        var KDRatioChangesChange = KDRatioChange2.Percent(KDRatioChange1);

                        var ts1 = tmpTime - GameController.chainlinkPriceTime;
                        Log.ShowInUI($"RoundID:#{CurrentRoundID}#  last 10s change{kline_10s.change:f3}   chainlinkOffset：{chainlinkPriceOffset:f2} ");
                        IsThisRoundAutoBetted = true;


                        RoundData rd = new RoundData()
                        {
                            RoundID = CurrentRoundID,
                            Last10sChange = Math.Round(kline_10s.change, 3),
                            LinkPriceSecounds = (int)ts1.TotalSeconds,
                            Offset = Math.Round(chainlinkPriceOffset, 2),
                            KDChange = Math.Round(KDRatioChange1, 2),
                            Trend = Math.Round(KDRatioChangesChange, 2),
                            KDRatio2 = Math.Round(kdInfo2.KDRatio, 3),
                            KDRatio3 = Math.Round(kdInfo3.KDRatio, 3),
                            Change = Math.Round(klineInfo.Change, 2),
                            ChangeBefore = Math.Round(klineInfo3.Change, 2),
                            DownShadowLine = Math.Round(klineInfo.DownShalowLine, 2),
                            UpShadowLine = Math.Round(klineInfo.UpShadowLine, 2),
                            OnBollMiddle = Math.Round(klineInfo.OnBollMiddle, 2),
                            K2 = Math.Round(kdInfo2.K, 2),
                            D2 = Math.Round(kdInfo2.D, 2),
                            K3 = Math.Round(kdInfo3.K, 2),
                            D3 = Math.Round(kdInfo3.D, 2),
                            BollWidth = Math.Round(klineInfo.BollWidth, 2),
                            BollChange = Math.Round(klineInfo.BollChange, 2),
                            Change10m = Math.Round(klineInfo.Change10m, 2),
                            Change15m = Math.Round(klineInfo.Change15m, 2),
                            Change20m = Math.Round(klineInfo.Change20m, 2),
                            Change25m = Math.Round(klineInfo.Change25m, 2),
                        };

                        rd.SaveToSql();

                        if (pancakeKNNV3 == null) pancakeKNNV3 = new PancakeKNN_V3(Config.cfg.DbServerName, Config.cfg.dbName, Config.cfg.dbUser, Config.cfg.dbPassword);
                        var knnResult_v30123 = new KNNResult();
                        BetSide betSide = BetSide.@null;

                        try
                        {
                            knnResult_v30123 = pancakeKNNV3.KNNDecide_0123(kValue: 2);  //26/////500-5   100-5   200-5  300-5  600-5
                            predictionRoundID = knnResult_v30123.RoundID;
                            Log.ShowInUI($"RoundID:{knnResult_v30123.RoundID}" +
                                $"  kValue: {knnResult_v30123.kValue}" +
                                $"  score: {knnResult_v30123.Score:f2}" +
                                $"  AgainstPercent: {knnResult_v30123.AgainstPercent:P2}" +
                                $"  KnnPrediction:{knnResult_v30123.PredictionIsBull}");
                            pancakeKNNV3.RecordPredictionToSQL(knnResult_v30123);
                            betSide = knnResult_v30123.PredictionIsBull ? BetSide.BULL : BetSide.BEAR;
                            if (knnResult_v30123.RoundID == 0)
                            {
                                Log.ShowInUI("knn_v30123 result abnomal!");
                                betSide = BetSide.@null;
                            }


                            if (this.Disposing || this.IsDisposed) return;
                            Prediction_Table.AddObject(new PredictionItem() { RoundID = knnResult_v30123.RoundID, Prediction = betSide, Result = BetSide.@null, Time = DateTime.Now.ToString("HH:mm/MM-dd") });
                        }
                        catch (Exception ex)
                        {
                            Log.ShowInUI("exception on knn predicting！" + ex.ToString());
                        }



                        if (KNN_AutobetEnabled == false)
                        {
                            betSide = BetSide.@null;
                        }


                        var betAmount = double.Parse(bet_Amount_BNB_tbox.Text);



                        var lastAutoBet = AutoBetLogList.Find(f => f.RoundID == predictionRoundID - 1);
                        if (lastAutoBet != null)
                        {
                            Config.ReadConfig();
                            var baseAmount = Config.cfg.betAmountInBNB;
                            var addtionAmount = baseAmount;
                            addtionAmount = 0;


                            var roundResult = CheckCanWin(lastAutoBet.Side);
                            if (roundResult == RoundResult.LOST)
                            {
                                betAmount = (lastAutoBet.Amount * 2) + addtionAmount;
                                bet_Amount_BNB_tbox.Text = $"{betAmount}";
                                Log.ShowInUI($"this round faild, next round double to {betAmount}");
                            }
                            else if (roundResult == RoundResult.UNKNOW)
                            {
                                if (lastAutoBet.Amount < betAmount * 2 * 2 * 2 * 2)
                                {
                                    betAmount = (lastAutoBet.Amount * 2) + addtionAmount;
                                    bet_Amount_BNB_tbox.Text = $"{betAmount}";
                                    Log.ShowInUI($"this round unknow, next round double to {betAmount}");
                                }
                                else
                                if (lastAutoBet.Amount > betAmount * 2 * 2 * 2 * 2 * 2)
                                {
                                    bet_Amount_BNB_tbox.Text = baseAmount.ToString();
                                    betAmount = baseAmount;
                                    Log.ShowInUI($"this round faild, next round reset to {betAmount}");
                                }
                                else
                                {
                                    betAmount = (lastAutoBet.Amount) + addtionAmount;
                                    Log.ShowInUI($"this round unknow, next round keep to {betAmount}");
                                }
                            }
                            else if (roundResult == RoundResult.WIN)
                            {
                                bet_Amount_BNB_tbox.Text = baseAmount.ToString();
                                betAmount = baseAmount;
                                Log.ShowInUI($"this round success, next round reset to {betAmount}");
                            }
                        }


                        var quickBet = false;
                        switch (betSide)
                        {
                            case BetSide.BULL:
                                {
                                    bet_UP_btn_Click(null, null);
                                    game.ManualBet(BetSide.BULL, betAmount, QuickBet: quickBet);
                                    IsThisRoundAutoBetted = true;
                                    AutoBetLogList.Add(new AutoBetLog() { Amount = betAmount, RoundID = predictionRoundID, Side = BetSide.BULL, Win = false });
                                }
                                break;
                            case BetSide.BEAR:
                                {
                                    bet_DOWN_btn_Click(null, null);
                                    game.ManualBet(BetSide.BEAR, betAmount, QuickBet: quickBet);
                                    IsThisRoundAutoBetted = true;
                                    AutoBetLogList.Add(new AutoBetLog() { Amount = betAmount, RoundID = predictionRoundID, Side = BetSide.BEAR, Win = false });
                                }
                                break;
                            default:
                                break;
                        }
                        if (betSide != BetSide.@null)
                            Log.ShowInUI($"knn autobet {betSide} for {betAmount}");


                        CheckingToBet = false;
                    }
                    else
                    {
                        Log.ShowInUI("trades server dead.");
                    }
                }
            }

            if (oldRoundStartTime != game.CurrentRoundStartTime)
            {
                //oldRoundStartTime = game.CurrentRoundStartTime;
                //m5KlineStartTime = game.CurrentRoundStartTime.AddSeconds(300 - 40);
            }
        }

        private RoundResult CheckCanWin(BetSide side)
        {
            var value = 0.03;
            var chainLink = GameController.chainlinkPrice.Percent(LastClosePrice);
            var latest = latestPrice.Percent(LastClosePrice);
            Log.ShowInUI($"chainLink:{chainLink:f3}  latest:{latest:f3}");
            if (chainLink > value && latest > value)
                if (side == BetSide.BULL) return RoundResult.WIN;
                else
                if (side == BetSide.BEAR) return RoundResult.LOST;
            if (chainLink < 0 && latest < 0)
                if (side == BetSide.BULL) return RoundResult.LOST;
                else
                if (side == BetSide.BEAR) return RoundResult.WIN;
            return RoundResult.UNKNOW;
        }

        public enum RoundResult
        {
            WIN, LOST, UNKNOW
        }

        List<AutoBetLog> AutoBetLogList = new List<AutoBetLog>();

        public class AutoBetLog
        {
            public int RoundID;
            public BetSide Side;
            public double Amount;
            public bool Win;
        }

        DateTime lastTimeUpdateLP = DateTime.Now;
        long lastTimeUpdateLP_Ticks = 0;
        double lastLP = 0;
        private double ShowPriceOffset()
        {
            var chainlinkPriceOffset = latestPrice.Percent(GameController.chainlinkPrice);
            if (lastTimeUpdateLP_Ticks != GameController.chainlinkPriceTime.Ticks)
            {
                label_ChainLinkPrice.Text = $"Lp: {GameController.chainlinkPrice:f2}";
                lastLP = GameController.chainlinkPrice;
                lastTimeUpdateLP = DateTime.Now;
                lastTimeUpdateLP_Ticks = GameController.chainlinkPriceTime.Ticks;
            }
            label_lastLPTime.Text = $"{(DateTime.Now - lastTimeUpdateLP).TotalSeconds:f0}sec ago";
            label_LastPrice.Text = $"cp: {latestPrice:f2}";
            label_PriceOffset.Text = $"{chainlinkPriceOffset:f2}%";
            if (chainlinkPriceOffset > 0)
            {
                label_ChainLinkPrice.ForeColor = Color.Red;
                label_PriceOffset.ForeColor = Color.Red;
            }
            else
            {
                label_ChainLinkPrice.ForeColor = Color.Green;
                label_PriceOffset.ForeColor = Color.Green;
            }
            return chainlinkPriceOffset;
        }

        bool CheckingToBet = false;


        //https://github.com/PingmanTools/websocket-sharp/



        WebSocket webSocket = null;


        List<Trade> sec10TradeList = new List<Trade>();
        List<Trade> sec2TradeList = new List<Trade>();
        DateTime lastUpdateSec2 = DateTime.Now;
        BaseKLine kline_10s = new BaseKLine();
        long LatestTradesTime = 0;
        double latestPrice = 0;
        private void GoBNB_WebsocketSubscrib(string Proxy = "")
        {
            //webSocket = new WebSocket("wss://stream.binance.com:9443/stream?streams=bnbusdt@ticker/bnbusdt@kline_5m");
            webSocket = new WebSocket("wss://stream.binance.com:9443/ws");
            if (Proxy.Length > 1)
            {
                webSocket.SetProxy(Proxy, null, null);
            }

            var subscribString = "{\"method\": \"SUBSCRIBE\",\"params\": [\"bnbusdt@trade\",\"bnbusdt@kline_5m\"],  \"id\": 1}";

            webSocket.EmitOnPing = true;
            webSocket.OnMessage += (sender, e) =>
            {
                if (e.IsPing)
                {
                    if (webSocket.Ping() == false)
                    {
                        webSocket.Ping();
                    }
                }
                else
                {
                    var jtoken = JToken.Parse(e.Data);
                    var ee = (string)jtoken["e"];
                    if (ee != null)
                    {
                        if (ee == "trade")
                        {
                            var obj = jtoken;
                            var trade = new Trade();
                            trade.time = new DateTime().FromTimeStamp((long)obj["T"]);
                            trade.price = (double)obj["p"];
                            trade.vol = (double)obj["q"];
                            trade.buying = (long)obj["b"] > (long)obj["a"];

                            if (latestPrice != trade.price)
                            {
                                latestPrice = trade.price;
                                label_LastPrice.Invoke(new Action(() =>
                                {
                                    label_LastPrice.Text = $"cp: {latestPrice:f2}";
                                }));
                            }


                            //ShowBigTradeInRichBox(trade);



                            LatestTradesTime = (long)obj["T"];


                            sec2TradeList.Add(trade);
                            sec10TradeList.Add(trade);



                            if ((trade.time - lastUpdateSec2).TotalSeconds >= 0.3)
                            {

                                lastUpdateSec2 = trade.time;
                                #region update the 5m kline

                                BaseKLine kline = klineList_5m.Last();

                                if (m5KlineStartTime < trade.time)
                                {
                                    kline.Full = true;

                                    m5KlineStartTime = trade.time.AddSeconds(600 - KlineCountdownTimeInSecounds);
                                }

                                if (kline.Full)
                                {
                                    kline = new BaseKLine();
                                    kline.interval = "5m";
                                    kline.time = trade.time;
                                    kline.open = trade.price;
                                    kline.low = trade.price;
                                    kline.high = trade.price;
                                    kline.volume = trade.vol;
                                    kline.Full = false;
                                    klineList_5m.Add(kline);
                                }


                                var sec2Kline = sec2TradeList.GetKLine("2s");
                                sec2TradeList.Clear();

                                if (kline.high < sec2Kline.high) kline.high = sec2Kline.high;
                                if (kline.low > sec2Kline.low) kline.low = sec2Kline.low;
                                kline.volume += sec2Kline.volume;


                                kline.close = trade.price;
                                UpdateUI(kline);

                                #endregion

                            }

                            kline_10s = sec10TradeList.GetKLine10s();
                        }

                        else if (ee == "kline")
                        {
                            var obj = jtoken["k"];
                            var interval = obj["i"].ToString();
                            if (interval == "5m")
                            {
                                var klineDown = bool.Parse(obj["x"].ToString());

                                if (klineDown == false)
                                    klineList_5mTrue.Add(new BaseKLine());

                                BaseKLine kline5m = klineList_5mTrue.Last();
                                kline5m.time = new DateTime().FromTimeStamp((long)obj["t"]);
                                kline5m.open = (double)obj["o"];
                                kline5m.close = (double)obj["c"];
                                kline5m.high = (double)obj["h"];
                                kline5m.low = (double)obj["l"];
                                kline5m.volume = (double)obj["v"];
                                kline5m.interval = "5mTrue";
                                UpdateUI(kline5m);
                            }
                        }
                        //Console.WriteLine("data: " + e.Data);

                    }
                };

            };
            webSocket.OnClose += (s, e) =>
            {
                while (true)
                {
                    try
                    {
                        var msg = $"trades websocket server disconnect cus {e.Reason}, watting for reconnecting.";
                        Log.ShowInUI(msg);
                        _log.Error(msg);
                        Thread.Sleep(40000);
                        webSocket.Connect();
                        if (webSocket.IsAlive)
                        {
                            Log.ShowInUI("reconnecting websocket server success");
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Error($"exception on reconnecting websocket server. {ex}");
                    }
                }
            };

            webSocket.OnOpen += (s, e) =>
            {
                Log.ShowInUI($"websocket connected.");

                webSocket.Send(subscribString);
                Log.ShowInUI($"subscrib successed");
            };


            webSocket.Connect();
        }


        #region richbox  

        public delegate void LogAppendDelegate(Color color, string text);

        public void LogAppendMethod(Color color, string text)
        {
            //if (!richTextBox_BigOrderLog.ReadOnly)
            //    richTextBox_BigOrderLog.ReadOnly = true;
            this.rickTb.Select(this.rickTb.Text.Length, 0);
            this.rickTb.Focus();
            rickTb.SelectionColor = color;
            rickTb.AppendText(text);
        }

        public void LogError(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppendMethod);
            rickTb.Invoke(la, Color.Red, DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + text);
        }
        public void LogWarning(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppendMethod);
            rickTb.Invoke(la, Color.Blue, DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + text);
        }
        public void LogMessage(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppendMethod);
            rickTb.Invoke(la, Color.Green, DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + text);
        }

        #endregion


        public class BigOrderInfo
        {
            public DateTime Time;
            public string BuyingStr;
            public bool Buying;
            public double Price;
            public double Count;
        }

        public double BigOrderValue = 500;
        public double BigTradeValue = 30;

        List<BigOrderInfo> bigOrders_Buy = new List<BigOrderInfo>(30);
        List<BigOrderInfo> bigOrders_Sell = new List<BigOrderInfo>(30);
        private void CheckBigOrderInfo(OrderBook trade)
        {
            bigOrders_Buy.Clear();
            bigOrders_Sell.Clear();
            var bigsAsks = trade.asks.Where(w => w.vol > BigOrderValue).Take(10).ToList();
            var bigsBids = trade.bids.Where(w => w.vol > BigOrderValue).Take(10).ToList();
            foreach (var item in bigsAsks)
            {
                var big = new BigOrderInfo()
                {
                    Buying = true,
                    BuyingStr = "Buy",
                    Count = item.vol,
                    Price = item.price,
                    Time = DateTime.Now
                };
                bigOrders_Buy.Add(big);
            }


            foreach (var item in bigsBids)
            {
                var big = new BigOrderInfo()
                {
                    Buying = false,
                    BuyingStr = "Sell",
                    Count = item.vol,
                    Price = item.price,
                    Time = DateTime.Now
                };
                bigOrders_Sell.Add(big);
            }


            //if (BigOrder_Table_Sell.InvokeRequired)
            //{
            //    BigOrder_Table_Buy.Invoke(new Action(() =>
            //    {
            //        BigOrder_Table_Buy.SetObjects(bigOrders_Buy);
            //    }));
            //    BigOrder_Table_Sell.Invoke(new Action(() =>
            //    {
            //        BigOrder_Table_Sell.SetObjects(bigOrders_Sell);
            //    }));
            //}
            //else
            //{
            //    BigOrder_Table_Sell.SetObjects(bigOrders_Sell);
            //    BigOrder_Table_Buy.SetObjects(bigOrders_Buy);
            //    //if (BigOrder_Table.Items.Count > 0)
            //    //    BigOrder_Table.Items[BigOrder_Table.Items.Count - 1].EnsureVisible();

            //}
        }


        DateTime m5KlineStartTime = DateTime.Now;


        private void UpdateUI(BaseKLine kline)
        {
            inds.CalcIndicatorsWhenDataUpdated();

            if (kline.interval == "5m")
            {
                chartBNB_M5.Invoke(new Action(() =>
                {
                    chartBNB_M5.AppendKline(kline.ToBar_my(), true, true);
                }));
            }
            else if (kline.interval == "5mTrue")
            {
                chartBNB_M5True.Invoke(new Action(() =>
                {
                    chartBNB_M5True.AppendKline(kline.ToBar_my(), true, true);
                }));
            }
        }



        //binance api  fapi   dapi   https://fapi.binance.com/fapi/v1/depth?symbol=BTCUSDT&limit=100

        private List<BaseKLine> GetBNB_Kline_Indicator(int count = 90, string inverVal = "5m", DateTime begin = default, DateTime end = default)
        {
            var start = (long)begin.ToKlineTimestamp() * 1000;
            var stop = (long)end.ToKlineTimestamp() * 1000;
            ////1639960200000

            var endpoint = "https://api.binance.com/api/v3";
            //endpoint = "https://fapi.binance.com/fapi/v1";

            string response;
            if (begin == default || end == default)
                response = HttpGet($"{endpoint}/klines?symbol=BNBUSDT&interval={inverVal}&limit={count}");
            else
                response = HttpGet($"{endpoint}/klines?symbol=BNBUSDT&interval={inverVal}&startTime={start}&endTime={stop}&limit={count}");
            var list = new List<BaseKLine>();
            var jt = JToken.Parse(response);
            var childs = jt.Children();
            foreach (var item in childs)
            {
                var kline = new BaseKLine();
                kline.time = new DateTime().FromTimeStamp(Convert.ToInt64(item[0]));
                kline.open = Convert.ToDouble(item[1]);
                kline.high = Convert.ToDouble(item[2]);
                kline.low = Convert.ToDouble(item[3]);
                kline.close = Convert.ToDouble(item[4]);
                kline.volume = Convert.ToDouble(item[5]);
                kline.Full = true;
                kline.interval = "5m";
                list.Add(kline);
            }
            list.Last().Full = false;
            return list;
        }


        public class KLineWithIndicator
        {
            /// <summary>
            /// K线
            /// </summary>
            private List<BinanceKLine> kLine;
            public KLineWithIndicator(List<BinanceKLine> kLine)
            {
                this.kLine = kLine;
            }

        }




        private string HttpGet(string url, Encoding encoding = null)
        {
            try
            {
                if (encoding == null) encoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, encoding);
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception)
            {
                return "";
            }
        }

        object contextMenuStrip1Source = null;
        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            contextMenuStrip1.Tag = contextMenuStrip1.SourceControl;
            contextMenuStrip1Source = contextMenuStrip1.SourceControl;
        }

        private void ChangeIndicatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                ToolStripItem menuItem = sender as ToolStripItem;
                var chart = contextMenuStrip1Source as MyChart;
                chart.ChangeIndicator(menuItem.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        private void PredictionTable_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            e.Item.ForeColor = Color.White;
            var item = (PredictionItem)e.Model;
            if (item.Prediction == BetSide.@null) return;
            if (item.Result == BetSide.@null)
            {
                e.Item.BackColor = Color.Black;
                return;
            }


            if (item.Result == item.Prediction)
                e.Item.BackColor = Color.Green;
            else
                e.Item.BackColor = Color.Red;
        }


        public class OrderBook
        {
            public DateTime lastUpdateId { get; set; }
            public List<Price_Vol> bids = new List<Price_Vol>();
            public List<Price_Vol> asks = new List<Price_Vol>();

            public double GetGuessBuyPrice()
            {
                var list = bids.OrderByDescending(o => o.price * o.vol).ToList();
                var fist = list.First();
                list.RemoveAt(0);

                var finalpfice = fist.price;
                foreach (var item in list)
                {
                    if (item.price > fist.price)
                    {
                        finalpfice += (item.price * item.vol) / (fist.price * fist.vol);
                    }
                    else
                    {
                        finalpfice -= (item.price * item.vol) / (fist.price * fist.vol);
                    }
                }
                return finalpfice;
            }

            public double GetGuessSellPrice()
            {
                var list = asks.OrderByDescending(o => o.price * o.vol).ToList();
                var fist = list.First();
                list.RemoveAt(0);

                var finalpfice = fist.price;
                foreach (var item in list)
                {
                    if (item.price > fist.price)
                    {
                        finalpfice += (item.price * item.vol) / (fist.price * fist.vol);
                    }
                    else
                    {
                        finalpfice -= (item.price * item.vol) / (fist.price * fist.vol);
                    }
                }
                return finalpfice;
            }
        }
        public class Price_Vol
        {
            public double price { get; set; }
            public double vol { get; set; }
        }

        private async Task<OrderBook> GetBNB_OrderBook(int count = 30)
        {
            var response = await Task<string>.Run(() =>
            {
                return HttpGet($"https://api.binance.com/api/v3/depth?symbol=BNBUSDT&limit={count}");
            });

            var orders = new OrderBook();
            try
            {
                var jt = JToken.Parse(response);
                var bids = jt["bids"].Children();
                foreach (var item in bids)
                {
                    var ask = new Price_Vol();
                    ask.price = Convert.ToDouble(item[0]);
                    ask.vol = Convert.ToDouble(item[1]);
                    orders.asks.Add(ask);

                }

                var asks = jt["asks"].Children();
                foreach (var item in asks)
                {
                    var bid = new Price_Vol();
                    bid.price = Convert.ToDouble(item[0]);
                    bid.vol = Convert.ToDouble(item[1]);
                    orders.bids.Add(bid);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error on GetBNB_OrderBook ! " + e.ToString());
            }
            return orders;
        }

        private void Pankou_Table_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            var item = (PankouInfo)e.Model;
            if (item.OrderDiff > 0)
                e.Item.ForeColor = Color.Red;
            else
                e.Item.ForeColor = Color.Green;
        }

        public class PankouInfo
        {
            public int Level;
            public double Price;
            public double OrderDiff;

            public static double GetOrderDiffRatio(OrderBook ordersBook, int level)
            {
                var buyVol = ordersBook.bids.Take(level).Sum(s => s.vol * s.price);
                var selVol = ordersBook.asks.Take(level).Sum(s => s.vol * s.price);
                var diffvol = buyVol - selVol;

                var ratio = buyVol.Percent(selVol);
                return ratio;
            }
        }

        List<PankouInfo> pankouList = new List<PankouInfo>(20);


        List<BuySellStronger> BuyStrongers = new List<BuySellStronger>();
        public class BuySellStronger
        {
            public int Level;
            public int buyCount;
            public int SellCount;
        }

        object BuyStrongerLocker = new object();
        private async void toolStripMenuItemRefreshBooks_Click(object sender, EventArgs e)
        {
            var orders = await GetBNB_OrderBook(100);
            for (int i = 10; i <= 99; i += 10)
            {
                try
                {
                    var pankouInfo = pankouList[(i / 10) - 1];

                    pankouInfo.Level = i;
                    pankouInfo.Price = orders.bids[i].price;
                    pankouInfo.OrderDiff = PankouInfo.GetOrderDiffRatio(orders, i);

                    //pankouList[i / 10] = pankouInfo;
                    var redCount = 0;
                    var greenCount = 0;
                    if (pankouInfo.OrderDiff > 0)
                        redCount++;
                    else
                        greenCount++;

                    lock (MainForm.self.BuyStrongerLocker)
                    {
                        BuyStrongers.Add(new BuySellStronger() { Level = i, buyCount = redCount, SellCount = greenCount });
                    }


                }
                catch (Exception ex)
                {
                    _log.Error("exception on RefreshBooks" + ex.ToString());
                }
            }

            CheckBigOrderInfo(orders);




            //CheckBNBForBearOrBull
            {
                var countBnbForDown = orders.asks.Where(w => w.price > LastClosePrice).Sum(s => s.vol);
                label_bnbDown.Text = $"DOWN:{countBnbForDown:f1}";
                var countBnbForUp = orders.bids.Where(w => w.price < LastClosePrice).Sum(s => s.vol);
                label_bnbUPUP.Text = $"PULL:{countBnbForUp:f1}";
            }


            if (this.Disposing || this.IsDisposed) return;
            if (OrderBooks_Table.Objects == null)
                OrderBooks_Table.SetObjects(pankouList);
            else
                OrderBooks_Table.UpdateObjects(pankouList);
        }

        private void IntervalForOrders_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (IntervalForOrders_checkbox.Checked)
            {
                timer_for_orders.Enabled = true;
                timer_for_orders.Interval = int.Parse(textBox_invervalVal.Text);
            }
            else
            {
                timer_for_orders.Enabled = false;
                timer_for_orders.Interval = int.Parse(textBox_invervalVal.Text);
            }
        }

        private void textBox_invervalVal_TextChanged(object sender, EventArgs e)
        {
            timer_for_orders.Interval = int.Parse(textBox_invervalVal.Text);
        }

        private void timer_for_orders_Tick(object sender, EventArgs e)
        {
            toolStripMenuItemRefreshBooks_Click(null, null);
        }

        public static bool BetBull = false;
        private void bet_UP_btn_Click(object sender, EventArgs e)
        {
            BetBull = true;
            label_betsidt.Text = "BULL";
            label_betsidt.ForeColor = Color.Red;
        }

        private void bet_DOWN_btn_Click(object sender, EventArgs e)
        {
            BetBull = false;
            label_betsidt.Text = "BEAR";
            label_betsidt.ForeColor = Color.LimeGreen;
        }

        private void bet_Amount_BNB_tbox_TextChanged(object sender, EventArgs e)
        {
            //Config.cfg.betAmountInBNB = double.Parse(bet_Amount_BNB_tbox.Text);
            //Config.SaveConfig();
        }

        private async void button_ManualBull_Click(object sender, EventArgs e)
        {
            try
            {
                IsThisRoundBetted = true;
                button_ManualBull.Enabled = false;

                var slowKDJ = chartBNB_M5.baseIndi as IndicatorSlowStochasticOscillator;
                if (slowKDJ != null)
                {
                    double.TryParse(bet_Amount_BNB_tbox.Text, out double amountToBet);
                    //if (CheckProfit(amountToBet))

                    bet_UP_btn_Click(null, null);
                    try
                    {
                        if (remainSeconds < 20)
                            await game.ManualBet(BetSide.BULL, amountToBet, QuickBet: true);
                        else
                            await game.ManualBet(BetSide.BULL, amountToBet);
                    }
                    catch (Exception ex)
                    {
                        Log.ShowInUI($"Bet bull exception! {ex.Message}");
                    }

                }
            }
            catch (Exception ex1)
            {

                Log.ShowInUI($"Bet bull exception1！{ex1.Message}");
            }
            finally
            {
                button_ManualBull.Enabled = true;
            }
        }

        private async void button_ManualBear_Click(object sender, EventArgs e)
        {
            try
            {
                IsThisRoundBetted = true;
                button_ManualBear.Enabled = false;
                var slowKDJ = chartBNB_M5.baseIndi as IndicatorSlowStochasticOscillator;
                if (slowKDJ != null)
                {

                    bet_DOWN_btn_Click(null, null);
                    try
                    {
                        if (remainSeconds < 20)
                            await game.ManualBet(BetSide.BEAR, double.Parse(bet_Amount_BNB_tbox.Text), QuickBet: true);
                        else
                            await game.ManualBet(BetSide.BEAR, double.Parse(bet_Amount_BNB_tbox.Text));
                    }
                    catch (Exception ex)
                    {
                        Log.ShowInUI($"Bet bull exception!{ex.Message}");
                    }
                }

            }
            catch (Exception ex1)
            {
                Log.ShowInUI($"Bet bull exception1 !{ex1.Message}");
            }
            finally
            {
                button_ManualBear.Enabled = true;

            }
        }




        private void button_initBnb_Click(object sender, EventArgs e)
        {
            bet_Amount_BNB_tbox.Text = Config.cfg.betAmountInBNB.ToString();
        }

        public static int WinerCount = 0;

        public decimal StartBalanceBNB { get; private set; }

        private void button_double_Click(object sender, EventArgs e)
        {
            ResetBetAmount_BNB(true);
        }

        private void ResetBetAmount_BNB(bool doubleBet)
        {
            double.TryParse(bet_Amount_BNB_tbox.Text, out double amountBnb);
            if (doubleBet)
            {
                if (amountBnb >= 2.3) amountBnb = Config.cfg.betAmountInBNB;
                bet_Amount_BNB_tbox.Text = $"{(amountBnb * 2)}";
            }
            else
            {
                bet_Amount_BNB_tbox.Text = $"{Config.cfg.betAmountInBNB:f2}";
            }
        }

        private bool CheckProfit(double amountToBet)
        {
            if (GameController.balanceBNB - StartBalanceBNB > (decimal)(amountToBet))
                return true;
            else return false;

        }

        private void KNN_Autobet_chkbox_CheckedChanged(object sender, EventArgs e)
        {
            Config.cfg.AutoBet = EnableAutobet_chkbox.Checked;
        }

    }

}
