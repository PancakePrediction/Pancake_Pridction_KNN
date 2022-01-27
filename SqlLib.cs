using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Pancake_Pridction_KNN.SqlLib
{
    public static class SQL_Info
    {
        #region connection info        
        public static string DbServerName;
        public static string dbName;
        public static string dbUser;
        public static string dbPassword;
        #endregion
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
            password = SQL_Info.dbPassword;

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
                throw new Exception(string.Format("can not connect to SQL\r\n {0}", ex.Message));
            }
        }



        public bool UpdateToTable(DataTable dt, string tableName)
        {
            try
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn))
                {
                    bulkCopy.DestinationTableName = tableName;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        bulkCopy.ColumnMappings.Add(dt.Columns[i].Caption.ToString(), dt.Columns[i].Caption.ToString());
                    }
                    bulkCopy.WriteToServer(dt, DataRowState.Modified); //DataRowState.Modified 
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                //sqlConn.Close();
            }
            return false;
        }


        public bool AddToTable(DataTable dt, string tableName)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn))
                {
                    bulkCopy.DestinationTableName = tableName;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        bulkCopy.ColumnMappings.Add(dt.Columns[i].Caption.ToString(), dt.Columns[i].Caption.ToString());
                    }
                    //DataView dataview = dt.DefaultView;
                    //var dtTemporary = dataview.ToTable(false, "date");
                    //foreach (DataRow item in dtTemporary.Rows)
                    //{
                    //    Console.WriteLine(item["Date"].ToString());
                    //}
                    bulkCopy.WriteToServer(dt, DataRowState.Added | DataRowState.Modified); //DataRowState.Added 
                }
                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllText("ExceptionLog_SQL.log", ex.ToString());
                Console.SetCursorPosition(0, 15);
            }
            finally
            {
                //sqlConn.Close();
            }
            return false;
        }

        public DataTable GetEmptyDataTable(string tbname)
        {
            var dt = new DataTable();
            using (var da = new SqlDataAdapter($"SELECT * FROM {tbname} where id = 0", this.sqlConn))
            {
                da.Fill(dt);
            }
            return dt;
        }


    }




}
