using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.ComponentModel;


namespace SALEDM_ADO.Mssql
{
    public abstract class Base
    {               
        public static string conString { get; set; }
        public static string sql { get; set; }
        private static string error { get; set; }

        private string getConStr(string conStr)
        {
             return string.IsNullOrEmpty(conStr) ? conString : conStr;
        }

        protected string QuoteStr(string str)
        {
            return String.IsNullOrEmpty(str) ? "''" : "\'" + str.Replace("'", $"{(char)39}") + "\'";
        }

        protected T ExecuteScalar<T>(string cmdTxt, DynamicParameters parameter = null, string conStr = null)
        {
            using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
            {
                var res = SqlMapper.ExecuteScalar<T>(conn, cmdTxt, parameter, null, 600);
                return res;
            }
        }
        protected T ExecuteScalarSP<T>(string cmdTxt, DynamicParameters parameter = null, string conStr = null)
        {
            using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
            {
                var res = SqlMapper.ExecuteScalar<T>(conn, cmdTxt, parameter, null, 600, System.Data.CommandType.StoredProcedure);
                return res;

            }

        }

        protected int ExecuteNonQuery(string cmdTxt, DynamicParameters parameter = null, string conStr = null)
        {
            using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
            {
                var res = SqlMapper.Execute(conn, cmdTxt, parameter, null, 600);
                return res;

            }

        }
        protected int ExecuteNonQuerySP(string cmdTxt, DynamicParameters parameter = null, string conStr = null)
        {
            using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
            {
                var res = SqlMapper.Execute(conn, cmdTxt, parameter, null, 600, System.Data.CommandType.StoredProcedure);
                return res;

            }

        }

        protected IEnumerable<T> ExecQuery<T>(string cmdTxt, string conStr = null)
        {
            using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
            {
                var res = conn.Query<T>(cmdTxt);
                return res;

            }

        }

        protected IEnumerable<T> Query<T>(string cmdTxt, DynamicParameters parameter = null, string conStr = null)
        {
            using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
            {
                var res = SqlMapper.Query<T>(conn, cmdTxt, parameter, null, true, 600);
                return res;

            }

        }

        protected IEnumerable<T> QuerySP<T>(string spName, DynamicParameters parameter = null, string conStr = null)
        {
            using (SqlConnection conn = new SqlConnection(getConStr(conStr)))
            {
                var res = SqlMapper.Query<T>(conn, spName, parameter, null, true, 600, System.Data.CommandType.StoredProcedure);
                return res;

            }

        }
        protected IEnumerable<T> QuerySP<T>(SqlTransaction transaction, string spName, DynamicParameters parameter = null, string conStr = null)
        {
            if (transaction == null)
            {
                return QuerySP<T>(spName, parameter, conStr);
            }
            var res = transaction.Connection.Query<T>(spName, parameter, transaction, true, 600, System.Data.CommandType.StoredProcedure);
            return res;

        }

        // Transaction Rollback
        protected T ExecuteScalar<T>(SqlTransaction transaction, string cmdTxt, DynamicParameters parameter = null, string conStr = null)
        {
            if (transaction == null)
            {
                return ExecuteScalar<T>(cmdTxt, parameter, conStr);
            }
            var res = SqlMapper.ExecuteScalar<T>(transaction.Connection, cmdTxt, parameter, transaction, 600);
            return res;


        }
        protected int ExecuteNonQuery(SqlTransaction transaction, string cmdTxt, DynamicParameters parameter = null, string conStr = null)
        {
            if (transaction == null)
            {
                return ExecuteNonQuery(cmdTxt, parameter, conStr);
            }
            var res = SqlMapper.Execute(transaction.Connection, cmdTxt, parameter, transaction, 600);
            return res;


        }
        protected IEnumerable<T> Query<T>(SqlTransaction transaction, string cmdTxt, DynamicParameters parameter = null, string conStr = null)
        {
            if (transaction == null)
            {
                return Query<T>(cmdTxt, parameter, conStr);
            }
            var res = SqlMapper.Query<T>(transaction.Connection, cmdTxt, parameter, transaction, true, 600);
            return res;


        }

        public static SqlConnection OpenConnection(string conStr = null)
        {
            SqlConnection conn = new SqlConnection(string.IsNullOrEmpty(conStr) ? conString : conStr);
            return conn;
        }
        public static SqlTransaction BeginTransaction(string conStr = null)
        {
            var conn = OpenConnection(conStr);
            conn.Open();
            return conn.BeginTransaction();
        }

        public static DataTable ToDataTable(IEnumerable<dynamic> items)
        {
            if (items == null) return null;
            var data = items.ToArray();
            if (data.Length == 0) return null;

            var dt = new DataTable();
            foreach (var pair in ((IDictionary<string, object>)data[0]))
            {
                dt.Columns.Add(pair.Key, (pair.Value ?? string.Empty).GetType());
            }
            foreach (var d in data)
            {
                dt.Rows.Add(((IDictionary<string, object>)d).Values.ToArray());
            }
            return dt;
        }

        
    }
}
