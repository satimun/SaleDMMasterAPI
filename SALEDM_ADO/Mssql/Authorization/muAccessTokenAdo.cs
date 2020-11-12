using Dapper;
using SALEDM_MODEL.Data.Mssql.Authorization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SALEDM_ADO.Mssql.Authorization
{
    public class muAccessTokenAdo : Base
    {
        private static muAccessTokenAdo instant;

        public static muAccessTokenAdo GetInstant()
        {
            if (instant == null) instant = new muAccessTokenAdo();
            return instant;
        }

        

        private muAccessTokenAdo()
        {
           
        }

        public List<muAccessToken> ListActive(string conStr = null)
        {
            string cmd = "SELECT * FROM muAccessToken " +
                "WHERE Status='A';";
            var res = Query<muAccessToken>(cmd, null, conStr).ToList();
            return res;
        }

        public List<muAccessToken> Search(string Code, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", Code);

            string cmd = "SELECT * FROM muAccessToken " +
                "WHERE Code=@Code;";
            var res = Query<muAccessToken>(cmd, param, conStr).ToList();
            return res;
        }

        public int Update(string Code, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", Code);

            string cmd = $"UPDATE muAccessToken SET " +
                "CountUse=CountUse+1 " +
                "WHERE Code=@Code;";
            var res = ExecuteNonQuery(transac, cmd, param, conStr);
            return res;
        }

        public int Insert(string Code, string IPAddress, string Agent, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", Code);
            param.Add("@IPAddress", IPAddress);
            param.Add("@Agent", Agent);

            string cmd = "INSERT INTO muAccessToken (Code, IPAddress, Agent, CountUse, Status, UpdateBy, Timestamp) " +
                "VALUES (@Code, @IPAddress, @Agent, 1, 'A', 0, GETDATE());";
            var res = ExecuteNonQuery(transac, cmd, param, conStr);
            return res;
        }
    }
}
