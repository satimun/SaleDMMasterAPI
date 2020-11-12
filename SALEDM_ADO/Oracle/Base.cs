using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_ADO.Oracle
{
    public abstract class Base
    {
        public static string conString { get; set; }

        private string GetConnect(string con)
        {
            return string.IsNullOrWhiteSpace(con) ? "User Id=PERS;Password=pers123;Data Source=191.20.2.36:1521/HRMS:Min Pool Size=10,Incr Pool Size=5;Decr Pool Size=2;Connection Timeout=60;" : con;
        }

        // Query
        protected IEnumerable<T> Query<T>(string cmd, OracleDynamicParameters param = null)
        {
            using (OracleConnection conn = new OracleConnection(GetConnect(conString)))
            {
                conn.Open();
                var res = SqlMapper.Query<T>(conn, cmd, param, null, true, 60);
                conn.Close();
                return res;
            }
        }
        protected IEnumerable<T> Query<T>(OracleTransaction transaction, string cmd, OracleDynamicParameters param = null)
        {
            if (transaction == null) { return Query<T>(cmd, param); }
            var res = SqlMapper.Query<T>(transaction.Connection, cmd, param, transaction, true, 60);
            return res;
        }

        public int ExecuteNonQuery(string cmd, List<OracleParameter> param)
        {
            using (OracleConnection conn = new OracleConnection(GetConnect(conString)))
            {
                conn.Open();
                OracleCommand command = new OracleCommand(cmd, conn);
                param.ForEach(x => command.Parameters.Add(x));
                var res = command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                if (res != 1) { throw new Exception("Not Success."); }
                return res;
            }
        }

        public int ExecuteNonQuery(OracleTransaction transac, string cmd, List<OracleParameter> param)
        {
            if (transac == null) { return ExecuteNonQuery(cmd, param); }

            OracleCommand command = new OracleCommand(cmd, transac.Connection);
            param.ForEach(x => command.Parameters.Add(x));
            var res = command.ExecuteNonQuery();
            if (res != 1) { throw new Exception("Not Success."); }
            return res;
        }

        // open connection
        public static OracleConnection OpenConnection()
        {
            OracleConnection conn = new OracleConnection(conString);
            return conn;
        }
        public static OracleTransaction BeginTransaction()
        {
            var conn = OpenConnection();
            conn.Open();
            return conn.BeginTransaction();
        }
    }
}
