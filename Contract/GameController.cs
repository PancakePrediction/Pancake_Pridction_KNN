using Common.Logging;
using ML_Accord_KNN.Sqllib;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json.Linq;
using pancakeChainlikePrice;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Pancake_Pridction_KNN.Contract
{
    public partial class GameController
    {

        public List<RoundLog> roundLogList = new List<RoundLog>();
        public static GameController self;
        private readonly List<IDisposable> _disposables = new List<IDisposable>();
        string rpc_Endpoint; 
        string websocketNode;
        string wallletPrivateKey;
        string pancakePredictionContractAddress;
        Account walllet;
        string abi;
        ContractHandler contractHandler;
        Web3 web3;
        pancakeChainlikePriceService CL_PriceSrv;
        string proxy;
        private BigInteger CurrBettingRoundID = 0;
        public static double chainlinkPrice = 0;
        public static DateTime chainlinkPriceTime = DateTime.Now;
        public static decimal balanceBNB = 0;

        public DateTime CurrentRoundStartTime = DateTime.Now;
        ILog _log = Program.logger;
        public GameController()
        {
            self = this;
            pancakePredictionContractAddress = "0x18B2A687610328590Bc8F2e5fEdDe3b582A49cdA";
            abi = File.ReadAllText(@"Contract\pancake_Prediction.json");

            proxy = Config.cfg.Proxy;

            websocketNode = Config.cfg.websocketNode;
            rpc_Endpoint = Config.cfg.rpc_Endpoint;

            wallletPrivateKey = Config.cfg.wallletPrivateKey;
            walllet = new Account(wallletPrivateKey, (BigInteger?)(BigInteger)56);
        }

        public Task<BigInteger> CurrentEpochQueryAsync(BlockParameter blockParameter = null)
        {
            return contractHandler.QueryAsync<CurrentEpochFunction, BigInteger>(null, blockParameter);
        }

        public async Task StartPrediction()
        {
            web3 = new Web3(walllet, rpc_Endpoint);

            CL_PriceSrv = new pancakeChainlikePriceService();

            contractHandler = web3.Eth.GetContractHandler(pancakePredictionContractAddress);

            var task = Task.Factory.StartNew(async () =>
            {
                Log.ShowInUI($"refresh rounds task started.");
                var oldRoundID = 0;
                var api = new pancakeAPIService();
                while (true)
                {
                    try
                    {
                        var curRoundID = (int)await api.CurrentEpochQueryAsync().TimeoutAfter(TimeSpan.FromSeconds(5));
                        _log.Debug($"currentEpoch:{curRoundID}");
                        if (curRoundID > oldRoundID)
                        {
                            oldRoundID = curRoundID;
                            CurrBettingRoundID = curRoundID;
                            await NewRoundBegin(curRoundID);
                        }

                        var latestRoundLinkPriceData = await CL_PriceSrv.LatestRoundDataQueryAsync();
                        chainlinkPrice = (double)latestRoundLinkPriceData.Answer / 100000000;
                        chainlinkPriceTime = new DateTime().FromPancakeTimeStamp((long)latestRoundLinkPriceData.UpdatedAt); ;
                        await Task.Delay(2000);
                    }
                    catch (Exception ex)
                    {
                        _log.Debug($"exception on getting roundID, API CurrentEpochQueryAsync. {ex.Message}");
                    }
                }

            });

            var task1 = Task.Factory.StartNew(async () =>
            {
                Log.ShowInUI($"refresh balance task started.");
                while (true)
                {
                    try
                    {
                        balanceBNB = await GetBalance_BNB();
                        await Task.Delay(5000);
                    }
                    catch (Exception ex)
                    {
                        _log.Debug($"exception on getting balance. {ex.Message}");
                    }
                }

            });
        }

        private async Task<BetSide> GetRoundResult(int roundID)
        {
            var roundInfo = await GetRoundInfo(roundID).TimeoutAfter(TimeSpan.FromSeconds(5));
            return roundInfo.ClosePrice > roundInfo.LockPrice ? BetSide.BULL : BetSide.BEAR;
        }

        public static double GetLatestChainLikePrice()
        {
            chainlinkPrice = (double)self.CL_PriceSrv.LatestAnswerQueryAsync().Result / 100000000;
            return chainlinkPrice;
        }
        private async Task NewRoundBegin(int curRoundID)
        {
            ProcessStartRoundEvent(curRoundID);
            ProcessEndRoundEvent(curRoundID);
        }

        private async Task ProcessEndRoundEvent(int roundID)
        {
            try
            {
                var oldRoundID = roundID - 2;

                RoundsOutput roundInfo = new RoundsOutput();

                while (true)
                {
                    try
                    {
                        roundInfo = await GetRoundInfo(oldRoundID).TimeoutAfter(TimeSpan.FromSeconds(5));
                        if (roundInfo.ClosePrice > 0) break;
                        await Task.Delay(2000);
                    }
                    catch (Exception ex)
                    {
                        _log.Error($"exception on ProcessEndRoundEvent when getting finished round info.{ex}");
                    }
                }

                var round = roundLogList.Find(f => f.roundID == oldRoundID);
                var evtPrice = (double)roundInfo.ClosePrice / 100000000;
                BetSide result = BetSide.@null;
                if (round != null)
                {
                    if (evtPrice > round.ClosePrice)
                    {
                        round.Result = BetSide.BULL;
                    }
                    else
                    {
                        round.Result = BetSide.BEAR;
                    }
                    if (round.BetSide == round.Result)
                    {
                        round.Winner = true;
                        Config.cfg.faildCount = 0;
                        Config.SaveConfig();
                    }
                    else
                    {
                        Config.cfg.faildCount++;
                        Config.SaveConfig();
                    }
                    result = round.Result;
                }


                if (round == null)
                {
                    round = new RoundLog() { BetSide = BetSide.@null };
                    result = await GetRoundResult(oldRoundID);
                }

                var roundWin = ((round.BetSide == BetSide.@null || round == null) ? "Not bet" : round.Winner.ToString());
                Log.ShowInUI($"end round ID :{oldRoundID}  price:{evtPrice:f2}  result: {result}  Win: {roundWin}");
                if (roundWin == "False")
                {
                    lostTimes++;
                    if (lostTimes >= 3)
                    {
                        OnEvent(null, new ContractEventArgs() { Type = ConEventType.StopAutoBet });
                    }
                }
                else lostTimes = 0;


                RecordRoundResultToSQL(oldRoundID, result == BetSide.BULL);

                OnEvent(null, new ContractEventArgs() { Type = ConEventType.EndRound, price = evtPrice, RoundID = roundID, Win = round == null ? false : round.Winner, RoundResult = result });

                var roundNext = roundLogList.Find(f => f.roundID == roundID - 1);
                if (roundNext != null)
                    roundNext.ClosePrice = evtPrice;

                if (round != null && round.Winner)
                {
                    try
                    {
                        MainForm.WinerCount++;
                        var faildTimes = 0;
                        Log.ShowInUI($"Ready to be claim prize automatically for roundID:{oldRoundID} ");
                        while (await ClaimPrize(oldRoundID).TimeoutAfter(TimeSpan.FromSeconds(20)) == false)
                        {
                            faildTimes++;
                            if (faildTimes > 3) break;
                            await Task.Delay(5000);
                        }
                        Log.ShowInUI($"claim prize success...");
                    }
                    catch (Exception ex)
                    {
                        Log.ShowInUI($"claim prize timeout {ex}");
                    }
                }
            }
            catch (Exception e)
            {
                var str = $"Exception on ProcessEndRoundEvent :{e.Message}";
                Debug.WriteLine(str);
                Log.ShowInUI(str);
            }
        }


        private void RecordRoundResultToSQL(int oldRoundID, bool IsBull)
        {
            if (!string.IsNullOrEmpty(Config.cfg.DbServerName))
            {
                var conn = new SQL_Connection(Config.cfg.DbServerName, Config.cfg.dbName, Config.cfg.dbUser, Config.cfg.dbPassword);
                conn.sqlCmd.CommandText = $"update RoundData set IsBull={(IsBull ? 1 : 0)} where RoundID={oldRoundID}";
                conn.sqlCmd.ExecuteNonQuery();
            }
        }


        public static int lostTimes = 0;


        private async Task ProcessStartRoundEvent(int roundID)
        {
            try
            {
                double betAmountInBNB = Config.cfg.betAmountInBNB;

                CurrentRoundStartTime = DateTime.Now;

                OnEvent(null, new ContractEventArgs() { Type = ConEventType.NewRoundStarted, RoundID = roundID });

                var log = new RoundLog();

                #region Get round info 
                RoundsOutput newRoundInfo = new RoundsOutput();

                while (true)
                {
                    try
                    {
                        newRoundInfo = await GetRoundInfo(roundID).TimeoutAfter(TimeSpan.FromSeconds(5));
                        if (newRoundInfo.Epoch > 0) break;
                        await Task.Delay(2000);
                    }
                    catch (Exception)
                    {
                        _log.Error($"exception on GetRoundInfo in ProcessStartRoundEvent");
                    }
                }
                _log.Info(JObject.FromObject(newRoundInfo).ToString());
                log.roundID = (int)roundID;
                log.TotalAmount = (double)Web3.Convert.FromWei(newRoundInfo.TotalAmount);
                log.BearAmount = (double)Web3.Convert.FromWei(newRoundInfo.BearAmount);
                log.BullAmount = (double)Web3.Convert.FromWei(newRoundInfo.BullAmount);
                log.ClosePrice = (double)Web3.Convert.FromWei(newRoundInfo.ClosePrice);//0
                log.RewardAmount = (double)Web3.Convert.FromWei(newRoundInfo.RewardAmount);

                //fix the time
                CurrentRoundStartTime = new DateTime().FromPancakeTimeStamp((long)newRoundInfo.StartTimestamp);
                #endregion

                log.BetSide = BetSide.@null;
                roundLogList.Add(log);
            }
            catch (Exception e)
            {
                var str = $"Exception on ProcessStartRoundEvent :{e.Message}";
                Debug.WriteLine(str);
                Log.ShowInUI(str);
            }
        }


        public async Task<decimal> GetBalance_BNB()
        {
            try
            {
                HexBigInteger hexBigInteger = await new Web3(rpc_Endpoint).Eth.GetBalance.SendRequestAsync(walllet.Address);
                return await Task.FromResult(Web3.Convert.FromWei(hexBigInteger.Value));
            }
            catch (Exception ex)
            {
                Console.WriteLine("error on GetBalance_BNB " + ex.Message);
            }
            return 0;
        }


        public async Task<bool> ManualBet(BetSide betSide, double betAmountInBNB, bool QuickBet = false)
        {
            if (CurrBettingRoundID == 0)
            {
                Log.ShowInUI("Bet faild! CurrBettingRoundID is zero");
                return false;
            }
            var betBull = betSide == BetSide.BULL;
            BetSide side;

            int Gas = 590000;
            var gasprice = 6000000000;// await web3.Eth.GasPrice.SendRequestAsync();
            if (QuickBet)
            {
                gasprice = (long)(gasprice * 4);
                Gas = (int)(Gas * 2);
            }


            HexBigInteger gasUsed = new HexBigInteger(0);
            if (betBull)
            {
                try
                {
                    side = BetSide.BULL;

                    var betBearFunctionTxnReceipt = await contractHandler.SendRequestAndWaitForReceiptAsync(new BetBullMsg()
                    {
                        Epoch = CurrBettingRoundID,
                        AmountToSend = Web3.Convert.ToWei(betAmountInBNB),
                        Gas = Gas,
                        GasPrice = gasprice
                    }).TimeoutAfter(TimeSpan.FromSeconds(20));
                    var succeeded = betBearFunctionTxnReceipt.Succeeded();
                    if (!succeeded) side = BetSide.@null;
                    else gasUsed = betBearFunctionTxnReceipt.GasUsed;
                }
                catch (Exception ex)
                {
                    Log.ShowInUI($"BetBull faild :{ex.Message}");
                    side = BetSide.@null;
                }
            }
            else
            {
                side = BetSide.BEAR;
                try
                {
                    var betBearFunctionTxnReceipt = await contractHandler.SendRequestAndWaitForReceiptAsync(new BetBearMsg()
                    {
                        Epoch = CurrBettingRoundID,
                        AmountToSend = Web3.Convert.ToWei(betAmountInBNB),
                        Gas = Gas,
                        GasPrice = gasprice
                    }).TimeoutAfter(TimeSpan.FromSeconds(20));

                    var succeeded = betBearFunctionTxnReceipt.Succeeded();
                    if (!succeeded) side = BetSide.@null;
                    else gasUsed = betBearFunctionTxnReceipt.GasUsed;
                }
                catch (Exception ex)
                {
                    Log.ShowInUI($"BetBear faild :{ex.Message}");
                    side = BetSide.@null;
                }
            }

            if (side == BetSide.@null)
            {
                Log.ShowInUI($"Bet {(betBull ? "BetBull" : "BetBear")} faild! BNB:{betAmountInBNB}");
                return false;
            }
            else
            {
                Log.ShowInUI($"Bet {(betBull ? "BetBull" : "BetBear")} success! Gas Used: {gasUsed}  BNB:{betAmountInBNB}");
                if (roundLogList.Count > 0)
                    roundLogList.Last().BetSide = side;
                return true;
            }

        }

        public Task<BigInteger> CurrentEpochQueryAsync(CurrentEpochFunction currentEpochFunction, BlockParameter blockParameter = null)
        {
            return contractHandler.QueryAsync<CurrentEpochFunction, BigInteger>(currentEpochFunction, blockParameter);
        }

        public Task<RoundsOutput> GetRoundInfo(BigInteger roundID)
        {
            if (roundID == BigInteger.Zero)
            {
                roundID = CurrentEpochQueryAsync(null).Result;
            }
            var roundMsg = new RoundsMsg() { roundID = roundID };
            return contractHandler.QueryDeserializingToObjectAsync<RoundsMsg, RoundsOutput>(roundMsg);
        }

        public async Task<bool> ClaimPrize(int roundID = 0)
        {
            int backward = 5;

            if (CurrBettingRoundID == 0)
            {
                CurrBettingRoundID = await CurrentEpochQueryAsync(null);
                return false;
            }

            var gasprice = await web3.Eth.GasPrice.SendRequestAsync();
            var claim = new ClaimFunction()
            {
                Epochs = new List<BigInteger>(),
                Gas = 390000,
                GasPrice = gasprice
            };
            if (roundID > 0)
            {
                var claimableFunction = new ClaimableFunction();
                claimableFunction.Epoch = roundID;
                claimableFunction.User = walllet.Address;
                var claimableFunctionReturn = await contractHandler.QueryAsync<ClaimableFunction, bool>(claimableFunction);
                if (claimableFunctionReturn)
                {
                    claim.Epochs.Add(roundID);
                }
            }
            else
            {
                for (var i = 1; i <= backward; i++)
                {
                    var epochToCheck = CurrBettingRoundID - i;

                    var claimableFunction = new ClaimableFunction();
                    claimableFunction.Epoch = epochToCheck;
                    claimableFunction.User = walllet.Address;
                    var claimableFunctionReturn = await contractHandler.QueryAsync<ClaimableFunction, bool>(claimableFunction);
                    if (claimableFunctionReturn)
                    {
                        claim.Epochs.Add(epochToCheck);
                    }
                }
            }


            if (claim.Epochs.Count > 0)
            {
                var claimFunctionTxnReceipt = await contractHandler.SendRequestAndWaitForReceiptAsync(claim);
                if (claimFunctionTxnReceipt.Succeeded())
                {
                    Log.ShowInUI($"claim success! Gas used: {claimFunctionTxnReceipt.GasUsed}");
                }
                else
                {
                    Log.ShowInUI($"claim faild! Gas used: {claimFunctionTxnReceipt.GasUsed}\r\n Status: {claimFunctionTxnReceipt.Status}");
                }
                return true;
            }
            else
                Log.ShowInUI($"no prize claimable.");
            return false;
        }



        public event EventHandler<ContractEventArgs> OnEvent;

        public class ContractEventArgs : EventArgs
        {
            public string Data { get; }
            public ConEventType Type { get; internal set; }
            public byte[] RawData { get; }
            public double price;
            public int RoundID;
            internal bool Win;
            public BetSide RoundResult;
        }

        public enum ConEventType
        {
            NewRoundStarted,
            EndRound,
            StopAutoBet
        }
    }
}
