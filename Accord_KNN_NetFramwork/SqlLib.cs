using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ML_Accord_KNN.Sqllib
{
    public static class SQL_Info
    {     
        public static string DbServerName;
        public static string dbName;
        public static string dbUser;
        public static string dbPass;
    }
    public static class SQLSafeString
    {
        public static string SQLSafe(this string s)
        {
            return s.Replace("-", "").Replace("'", "");
        }

    }

    public class SQL_Connection
    {
        private string server;
        private string username;
        private string dbname;
        private string password;
        private static string connectionString = "";
        public SqlConnection sqlConn;


        public SqlCommand sqlCmd = new SqlCommand();

        public bool isSuccess = false;
        public string errorMessage;


        public SQL_Connection(string pServer, string pDbName, string pUsername, string pPassword)
        {
            server = pServer;
            dbname = pDbName;
            username = pUsername;
            password = pPassword;

            connectionString = string.Format("server={0};database={1};uid={2};pwd={3};pooling=true;connection lifetime=10;min pool size = 1;max pool size=10000", server, dbname, username, password);
            sqlConn = new SqlConnection(connectionString);
            isSuccess = connectToSql();
        }

        public SQL_Connection()
        {
            server = SQL_Info.DbServerName;
            dbname = SQL_Info.dbName;
            username = SQL_Info.dbUser;
            password = SQL_Info.dbPass;

            connectionString = string.Format("server={0};database={1};uid={2};pwd={3};pooling=true;connection lifetime=10;min pool size = 1;max pool size=10000", server, dbname, username, password);
            sqlConn = new SqlConnection(connectionString);
            isSuccess = connectToSql();
        }


        public bool ReConnect()
        {
            bool ret;
            sqlConn.Close();
            sqlConn.ConnectionString = connectionString;
            try
            {
                sqlConn.Open();
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandType = CommandType.Text;
                ret = true;
            }

            catch (System.Exception ex)
            {
                ret = false;
                errorMessage = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            return ret;
        }


        public bool connectToSql()
        {
            sqlConn.ConnectionString = connectionString;
            try
            {
                sqlConn.Open();
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandType = CommandType.Text;
                return true;
            }
            catch (System.Exception ex)
            {
                throw new Exception(string.Format("connecting sql server faild.\r\n {0}", ex.Message));
            }
        }
    }
}
