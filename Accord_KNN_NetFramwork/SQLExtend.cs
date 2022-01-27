using ML_Accord_KNN.Sqllib;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Accord_KNN.SQLExtend
{
    public static class SQLExtend
    {
        public static SQL_Connection conn = null;

        public static void FillDataFromSQL(this List<RoundData> dataList)
        {
            var table = "RoundData";

            if (conn == null) conn = new SQL_Connection();
            conn.sqlCmd.CommandText =
                $"SELECT * FROM [StockData].[dbo].[{table}] order by RoundID";
            var sdr = conn.sqlCmd.ExecuteReader();
            GetListFromSdr(dataList, sdr);
        }

        public static void GetLastItemsFromSQL(this List<RoundData> dataList, int latestRoundID)
        {
            var table = "RoundData";

            if (conn == null) conn = new SQL_Connection();
            conn.sqlCmd.CommandText =
                $"SELECT * FROM [StockData].[dbo].[{table}] where RoundID>{latestRoundID} order by RoundID";
            var sdr = conn.sqlCmd.ExecuteReader();
            GetListFromSdr(dataList, sdr);
        }

        public static RoundData GetRoundDataFromSql(this RoundData roundData, int roundID)
        {
            var table = "RoundData";

            if (conn == null) conn = new SQL_Connection();
            conn.sqlCmd.CommandText =
                $"SELECT * FROM [StockData].[dbo].[{table}] where RoundID={roundID}";
            var sdr = conn.sqlCmd.ExecuteReader();

            while (sdr.Read())
            {
                roundData = GetRoundDataFromSdr(sdr);
            }
            sdr.Close();
            return roundData;
        }

        private static void GetListFromSdr(List<RoundData> dataList, System.Data.SqlClient.SqlDataReader sdr)
        {
            while (sdr.Read())
            {
                var data = GetRoundDataFromSdr(sdr);
                dataList.Add(data);
            }
            sdr.Close();
        }

        private static RoundData GetRoundDataFromSdr(System.Data.SqlClient.SqlDataReader sdr)
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
                Change = Convert.ToDouble(sdr["Change"]),
                DownShadowLine = Convert.ToDouble(sdr["DownShadowLine"]),
                OnBollMiddle = Convert.ToDouble(sdr["OnBollMiddle"]),
                UpShadowLine = Convert.ToDouble(sdr["UpShadowLine"]),
            };
            return data;
        }




        public static double[] GetValues_V3(this RoundData data, RoundData before)
        {
            return new double[]
            {
                data.Last10sChange,
                data.KDChange,
                data.Change,
                data.OnBollMiddle,

                before.Last10sChange,
                before.KDChange,
                before.Change,
                before.OnBollMiddle,
            };
        }

    }
}
