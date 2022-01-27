using Accord.MachineLearning;
using Accord.Statistics.Analysis;
using ML_Accord_KNN.SQLExtend;
using ML_Accord_KNN.Sqllib;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Accord_KNN
{
    public class PancakeKNN_V3
    {
        List<RoundData> dataList = new List<RoundData>();
        double[][] inputs;
        int[] outputs;


        double[][] inputs_Test;
        int[] outputs_Test;
        public PancakeKNN_V3(string dbServer, string dbName, string dbUser, string dbPassword)
        {
            Sqllib.SQL_Info.DbServerName = dbServer;
            Sqllib.SQL_Info.dbName = dbName;
            Sqllib.SQL_Info.dbUser = dbUser;
            Sqllib.SQL_Info.dbPass = dbPassword;
            dataList.FillDataFromSQL();

        }


        public void FullTestToGetBestKValue(int testRounds = 500)
        {
            var testItem = GetTestItem();
            var bestParam = new BestParam();
            var lessSerialLostParam = new BestParam() { serialLost = 100 };
            var serialLostRoundID = new List<List<int>>();
            int testSetCount = 30;
            for (int kValue = 1; kValue < 50; kValue++)
            {
                var startID = testItem.RoundID - testRounds;

                var list = dataList.Where(w => w.RoundID <= startID).ToList();
                var testItems = dataList.Where(w => w.RoundID > startID).ToList();

                var totalTestCount = testItems.Count;

                var winCount = 0;
                var serialLost = 0;
                var maxSerialLost = 0;
                var tmp_SerialLostRoundID = new List<int>();
                var tmp_ThisRoundMaxSerialLostRoundID = new List<int>();
                var tmp_ThisRoundMaxSerialLostRoundIDList = new List<List<int>>();
                var sleep = 5;
                foreach (var test in testItems)
                {
                    //Add training to the latest data in the previous round of test, and testSetCount is zero to represent all used for training
                    testSetCount = 0;
                    BuildDatas(testSetCount, test.RoundID - 1);

                    var result = KNNDecide(kValue, test);

                    if (result.Score >= 0.01)
                    {
                        if (result.PredictionIsBull == test.IsBull)
                        {
                            winCount++;
                            serialLost = 0;

                            if (tmp_SerialLostRoundID.Count >= tmp_ThisRoundMaxSerialLostRoundID.Count)
                            {
                                tmp_ThisRoundMaxSerialLostRoundID = tmp_SerialLostRoundID.ToList();
                                tmp_ThisRoundMaxSerialLostRoundIDList.Add(tmp_ThisRoundMaxSerialLostRoundID.ToList());
                            }

                            tmp_SerialLostRoundID.Clear();
                        }
                        else
                        {
                            serialLost++;
                            tmp_SerialLostRoundID.Add(test.RoundID);
                        }
                    }
                    else
                        totalTestCount--;

                    if (maxSerialLost < serialLost) maxSerialLost = serialLost;
                }
                var winrate = (double)winCount / totalTestCount;
                if (double.IsNaN(winrate)) continue;
                Console.WriteLine($"Winratio rate：{winrate:p2}  kv:{kValue}  seriallost:{maxSerialLost}");
                if (winrate > bestParam.winRatio)
                {
                    bestParam.winRatio = winrate;
                    bestParam.kValue = kValue;
                    bestParam.testSetCount = testSetCount;
                    bestParam.serialLost = maxSerialLost;
                    bestParam.totalTestCount = totalTestCount;
                }
                if (maxSerialLost < lessSerialLostParam.serialLost || (winrate > lessSerialLostParam.winRatio && maxSerialLost <= lessSerialLostParam.serialLost))
                {
                    lessSerialLostParam.winRatio = winrate;
                    lessSerialLostParam.kValue = kValue;
                    lessSerialLostParam.testSetCount = testSetCount;
                    lessSerialLostParam.serialLost = maxSerialLost;
                    lessSerialLostParam.totalTestCount = totalTestCount;
                    serialLostRoundID.Clear();
                    serialLostRoundID.AddRange(tmp_ThisRoundMaxSerialLostRoundIDList);
                }
            }
            Console.WriteLine($"best winratio params：{bestParam.winRatio:p2}  kv:{bestParam.kValue}  set:{bestParam.testSetCount}  serialLost:{bestParam.serialLost}  totalcount:{bestParam.totalTestCount}");
            Console.WriteLine($"best seriallost params：{lessSerialLostParam.winRatio:p2}  kv:{lessSerialLostParam.kValue}  set:{lessSerialLostParam.testSetCount}  serialLost:{lessSerialLostParam.serialLost}  totalcount:{lessSerialLostParam.totalTestCount}");

            foreach (var item in serialLostRoundID)
            {

                Console.WriteLine($"serial lost IDs：{string.Join(",", item)}");
            }
        }


        public KNNResult KNNDecide_0123(RoundData testData = null, int kValue = 26)
        {
            if (testData == null)
            {
                testData = GetTestItem();
            }
            if (dataList.Count <= kValue) return new KNNResult();
            //Train with the latest sample datas, do not not separated test data from the sample, for prediction
            BuildDatas(0, testData.RoundID - 1);
            if (inputs.Length < kValue) return new KNNResult();

            //Get result
            var result = KNNDecide(kValue, testData);
            return result;
        }



        public KNNResult GetKNN_Result(int kValue, int testSetCount = 100)
        {
            var testItem = GetTestItem();


            BuildDatas(testSetCount);

            KNNResult result = null;
            ConfusionMatrix bestCm = null;
            var resultList = new List<KNNResult>();
            if (kValue < 0)
            {
                int TheBestK_Value = 0;
                for (int i = 1; i <= (int)(inputs.Length / 5); i++)
                {
                    var tmpResult = KNNDecide(i, testItem);
                    resultList.Add(tmpResult);
                    ConfusionMatrix cm = ConfusionMatrix.Estimate(tmpResult.knn, inputs_Test, outputs_Test);
                    double error = cm.Error;  // should be 0.0
                    double acc = cm.Accuracy; // should be 1.0
                    double kappa = cm.Kappa;  // should be 1.0

                    if (bestCm == null)
                        bestCm = cm;

                    if (cm.Accuracy > bestCm.Accuracy)
                    {
                        bestCm = cm;
                        TheBestK_Value = i;
                        result = tmpResult;
                    }
                    //Console.WriteLine($"Acc: {cm.Accuracy}\r\n" +
                    //    $"IsBull: {Convert.ToBoolean(answer)}");
                }

                //Console.WriteLine($"answer:{answer}  BestK:{TheBestK_Value}  LastAcc:{bestCm.Accuracy}  LastErr:{bestCm.Error}");

                result.kValue = TheBestK_Value;
                result.TestSetAccuracy = bestCm.Accuracy;

                var againstCount = resultList.Where(w => w.PredictionIsBull != result.PredictionIsBull).Count();
                var totalCount = resultList.Count;
                result.AgainstPercent = (double)againstCount / totalCount;
            }
            else
            {
                result = KNNDecide(kValue, testItem);
                result.kValue = kValue;
            }
            return result;
        }

        public KNNResult KNNDecide(int k, RoundData testData)
        {
            var knn = new KNearestNeighbors(k: k);

            // We learn the algorithm:
            knn.Learn(inputs, outputs);

            // After the algorithm has been created, we can classify a new instance:
            var before = dataList.Find(f => f.RoundID == testData.RoundID - 1);
            double[] data = null;
            if (before != null)
            {
                data = testData.GetValues_V3(before);
            }
            else return new KNNResult();
            var answer = knn.Decide(data);
            var score = knn.Score(data, answer);
            var nn = knn.GetNearestNeighbors(data, out int[] fda);

            return new KNNResult()
            {
                PredictionIsBull = Convert.ToBoolean(answer),
                Score = score,
                knn = knn,
                RoundID = testData.RoundID
            };

        }




        public RoundData GetRoundData(int roundID)
        {
            var roundData = new RoundData();
            roundData = roundData.GetRoundDataFromSql(roundID);
            return roundData;
        }



        private void BuildDatas(int lastCount, int maxRoundID = 99999999)
        {
            var list = dataList.Where(w => w.RoundID <= maxRoundID - 1).ToList();
            var last = list.Last();
            var samples = list.Take(list.Count - lastCount).ToList();
            var tests = list.Skip(samples.Count).ToList();

            BuildDatas2(samples, tests);
        }



        private void BuildSampleByRoundID(int roundID)
        {
            var samples = dataList.Where(w => w.RoundID <= roundID).ToList();
            var tests = dataList.Skip(samples.Count).ToList();
            BuildDatas2(samples, tests);

        }

        private void BuildDatas2(List<RoundData> samples, List<RoundData> tests)
        {
            #region samples
            var inputsList = new List<double[]>();
            var outputsList = new List<int>();

            for (int i = 1; i < samples.Count; i++)
            {
                var item = samples[i];
                inputsList.Add(item.GetValues_V3(samples[i - 1]));
                outputsList.Add(item.IsBull ? 1 : 0);
            }
            inputs = inputsList.ToArray();
            outputs = outputsList.ToArray();
            #endregion

            #region tests
            var inputs_Test_List = new List<double[]>();
            var outputs_Test_List = new List<int>();

            for (int i = 1; i < tests.Count; i++)
            {
                var item = tests[i];
                inputs_Test_List.Add(item.GetValues_V3(tests[i - 1]));
                outputs_Test_List.Add(item.IsBull ? 1 : 0);
            }

            inputs_Test = inputs_Test_List.ToArray();
            outputs_Test = outputs_Test_List.ToArray();
            #endregion
        }



        private RoundData GetTestItem()
        {
            List<RoundData> latestDataList = new List<RoundData>();
            var last = dataList.LastOrDefault();
            var lastRoundID = last == null ? 0 : last.RoundID;
            latestDataList.GetLastItemsFromSQL(lastRoundID);
            dataList.AddRange(latestDataList);
            var testItem = dataList.Last();
            dataList.Remove(testItem);

            return testItem;
        }

        SQL_Connection conn = null;
        public bool RecordPredictionToSQL( KNNResult data)
        {
            if (conn == null) conn = new SQL_Connection();
            conn.sqlCmd.CommandText = $"if not exists(SELECT 1 FROM [StockData].[dbo].[RoundData_Prediction] where RoundID = '{data.RoundID}') " +

                $"INSERT INTO [StockData].[dbo].[RoundData_Prediction] " +
                $"([RoundID] ,[PredictionIsBull],[PredictionIsBull2] ,[PredictionIsBullV3_0123],[PredictionIsBullV1_0123]," +
                $"[Score] ,[TestSetAccuracy] ,[kValue] ,[AgainstPercent]) VALUES " +
                $"({data.RoundID} ,{(data.PredictionIsBull ? 1 : 0)},{(data.PredictionIsBull2 ? 1 : 0)} ,{(data.PredictionIsBullV3_0123 ? 1 : 0)} ,{(data.PredictionIsBullV1_0123 ? 1 : 0)} ," +
                $"{data.Score} ,{data.TestSetAccuracy} ,{data.kValue} ,{data.AgainstPercent})";
            return Convert.ToBoolean(conn.sqlCmd.ExecuteNonQuery());
        }
    }
}
