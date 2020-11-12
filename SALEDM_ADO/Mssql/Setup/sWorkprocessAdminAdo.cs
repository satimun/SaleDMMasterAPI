using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using SALEDM_MODEL.Data.Mssql.Setup;
using SALEDM_MODEL.Request.Setup;

namespace SALEDM_ADO.Mssql.Setup
{
    public class sWorkprocessAdminAdo : Base
    {
        private static sWorkprocessAdminAdo instant;

        public static sWorkprocessAdminAdo GetInstant()
        {
            if (instant == null) instant = new sWorkprocessAdminAdo();
            return instant;
        }

        private sWorkprocessAdminAdo()
        {

        }

        public List<sWorkprocessAdmin> GetData(sWorkprocessAdminReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = " Select * from [sWorkprocessAdmin] where 1 = 1 ";

            if (d.wp_seq != null)
            {
                sql += " wp_seq = " + d.wp_seq;
            }

            if (d.wpa_seq != null)
            {
                sql += " wpa_seq = " + d.wpa_seq;
            }

            if (!String.IsNullOrEmpty(d.wpa_code))
            {
                sql += " wpa_code = " + QuoteStr(d.wpa_code.Trim());
            }

            if (!String.IsNullOrEmpty(d.wpa_desc))
            {
                sql += " wpa_desc = " + QuoteStr(d.wpa_desc.Trim());
            }

            var res = Query<sWorkprocessAdmin>(sql, param, conStr).ToList();
            return res;
        }

        public int Insert(sWorkprocessAdminReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@wpa_code", d.wpa_code);
            param.Add("@wpa_desc", d.wpa_desc);
            param.Add("@wp_seq", d.wp_seq);
            param.Add("@Edit_userid", d.Edit_userid);

            sql = "INSERT INTO sWorkprocessAdmin (wpa_code, wpa_desc,wp_seq, Edit_userid, Edit_date, Edit_datetime) " +
                "VALUES (@wpa_code, @wpa_desc,@wp_seq, @Edit_userid, DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())), GETDATE());";
            var res = ExecuteNonQuery(transac, sql, param, conStr);
            return res;
        }

        public int Update(sWorkprocessAdminReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@wpa_code", d.wpa_code);
            param.Add("@wpa_desc", d.wpa_desc);
            param.Add("@wpa_seq", d.wpa_seq);
            param.Add("@Edit_userid", d.Edit_userid);

            sql = "Update sWorkprocessAdmin set wpa_code = @wpa_code , wpa_desc = @wpa_desc, Edit_userid = @Edit_userid " +
                " , Edit_date = DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())), Edit_datetime = GETDATE()" +
                "Where wpa_seq = @wpa_seq ;";
            var res = ExecuteNonQuery(transac, sql, param, conStr);
            return res;
        }

        public int Delete(sWorkprocessAdminReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@wpa_seq", d.wpa_seq);

            sql = "Delete from sWorkprocessAdmin " +
                "Where wpa_seq = @wpa_seq ;";
            var res = ExecuteNonQuery(transac, sql, param, conStr);
            return res;
        }

        public int DeleteByWork(sWorkprocessAdminReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@wp_seq", d.wp_seq);

            sql = "Delete from sWorkprocessAdmin " +
                "Where wp_seq = @wp_seq ;";
            var res = ExecuteNonQuery(transac, sql, param, conStr);
            return res;
        }



    }
}
