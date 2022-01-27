using Pancake_Pridction_KNN.SqlLib;
using SharedModels;
using StockIndicatorLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;

namespace Pancake_Pridction_KNN
{
    public static class Extend
    {
        static SQL_Connection conn = null;        //new SQL_Connection();


        public static BaseKLine GetKLine(this List<Trade> list, string interval)
        {
            var kline = new BaseKLine();
            var first = list.First();
            var last = list.Last();

            kline.time = first.time;
            kline.open = first.price;
            kline.close = last.price;

            var orderd = list.OrderByDescending(o => o.price).ToList();
            kline.high = orderd.First().price;
            kline.low = orderd.Last().price;
            kline.volume = orderd.Sum(s => s.vol);
            kline.interval = interval;
            return kline;
        }


        public static BaseKLine GetKLine10s(this List<Trade> list)
        {
            var kline = new BaseKLine();
            var first = list.First();
            var last = list.Last();

            list.RemoveAll(r => r.time < last.time.AddSeconds(-10));
            first = list.First();
            last = list.Last();

            kline.time = first.time;
            kline.open = first.price;
            kline.close = last.price;
            kline.change = kline.close.Percent(kline.open);

            var orderd = list.OrderByDescending(o => o.price).ToList();
            kline.high = orderd.First().price;
            kline.low = orderd.Last().price;
            kline.volume = orderd.Sum(s => s.vol);
            kline.interval = "10s";
            return kline;
        }
  
        public static long ToTimeStamp(this System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (long)(time - startTime).TotalMilliseconds;
        }

        public static DateTime FromTimeStamp(this System.DateTime time, long timeSpan)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return startTime.AddMilliseconds(timeSpan);
        }


        /// <summary>
        /// pancake timestamp
        /// </summary>
        /// <param name="time"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static DateTime FromPancakeTimeStamp(this System.DateTime time, long timeSpan)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return startTime.AddSeconds(timeSpan);
        }


        public static Bar_my ToBar_my(this BaseKLine kLine)
        {
            return new Bar_my()
            {
                amount = 0,
                close = Convert.ToDouble(kLine.close),
                frequency = "",
                high = Convert.ToDouble(kLine.high),
                low = Convert.ToDouble(kLine.low),
                open = Convert.ToDouble(kLine.open),
                position = 0,
                preClose = Convert.ToDouble(kLine.close),
                symbol = "",
                time = kLine.time,
                volume = Convert.ToDouble(kLine.volume)

            };
        }

        public static List<Bar_my> ToBarList(this List<BaseKLine> bnbList)
        {
            var list = new List<Bar_my>();
            foreach (var item in bnbList)
            {
                list.Add(item.ToBar_my());
            }
            return list;
        }



        public static bool SaveToSql(this RoundData data)
        {
            if (conn == null) conn = new SQL_Connection();
            conn.sqlCmd.CommandText = $"if not exists(SELECT 1 FROM [StockData].[dbo].[RoundData] where RoundID = '{data.RoundID}') " +

                $"INSERT INTO [StockData].[dbo].[RoundData] " +
                $"([RoundID] ,[Last10sChange] ,[LinkPriceSecounds] ,[Offset] ,[KDChange] ,[Trend] ,[KDRatio2] ,[KDRatio3] ,[IsBull]," +
                $"[Change],[UpShadowLine],[DownShadowLine],[OnBollMiddle]," +
                $"[ChangeBefore],[K2],[D2],[K3],[D3],[BollWidth],[BollChange],[Change10m],[Change15m],[Change20m],[Change25m]) VALUES " +
                $"({data.RoundID} ,{data.Last10sChange} ,{data.LinkPriceSecounds} ,{data.Offset} ,{data.KDChange} ,{data.Trend} ,{data.KDRatio2} ,{data.KDRatio3} ," +
                $"{(data.IsBull ? 1 : 0)},{data.Change},{data.UpShadowLine},{data.DownShadowLine},{data.OnBollMiddle}," +
                $"{data.ChangeBefore},{data.K2},{data.D2},{data.K3},{data.D3},{data.BollWidth},{data.BollChange},{data.Change10m}," +
                $"{data.Change15m},{data.Change20m},{data.Change25m})";
            return Convert.ToBoolean(conn.sqlCmd.ExecuteNonQuery());
        }


        public static void FillDataFromSQL(this List<RoundData> dataList)
        {
            var table = "RoundData";

            if (conn == null) conn = new SQL_Connection();
            conn.sqlCmd.CommandText =
                $"SELECT * FROM [StockData].[dbo].[{table}] order by RoundID";
            var sdr = conn.sqlCmd.ExecuteReader();

            while (sdr.Read())
            {
                var data = new RoundData()
                {
                    RoundID = Convert.ToInt32(sdr["RoundID"]),
                    IsBull = Convert.ToBoolean(sdr["IsBull"]),
                    KDChange = Convert.ToDouble(sdr["KDChange"]),
                    KDRatio2 = Convert.ToDouble(sdr["KDRatio2"]),
                    KDRatio3 = Convert.ToDouble(sdr["KDRatio3"]),
                    Last10sChange = Convert.ToDouble(sdr["Last10sChange"]),
                    LinkPriceSecounds = Convert.ToInt32(sdr["LinkPriceSecounds"]),
                    Offset = Convert.ToDouble(sdr["Offset"]),
                    Trend = Convert.ToDouble(sdr["Trend"]),
                };
                dataList.Add(data);
            }

            sdr.Close();
        }

        public static string nowTime { get { return $"{DateTime.Now}-->"; } }

    }

    public static class TaskTimeOutExtend
    {
        public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout)
        {
            using (var timeoutCancellationTokenSource = new CancellationTokenSource())
            {
                var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));
                if (completedTask == task)
                {
                    timeoutCancellationTokenSource.Cancel();
                    return await task;  // Very important in order to propagate exceptions
                }
                else
                {
                    throw new TimeoutException($"async task timeout! TimeSpan: totalsecounds {timeout.TotalSeconds}");
                }
            }
        }


        public static async Task TimeoutAfter(this Task task, TimeSpan timeout)
        {
            using (var timeoutCancellationTokenSource = new CancellationTokenSource())
            {
                var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));
                if (completedTask == task)
                {
                    timeoutCancellationTokenSource.Cancel();
                    await task;  // Very important in order to propagate exceptions
                }
                else
                {
                    throw new TimeoutException($"async task timeout! TimeSpan: totalsecounds {timeout.TotalSeconds}");
                }
            }
        }
    }
}

