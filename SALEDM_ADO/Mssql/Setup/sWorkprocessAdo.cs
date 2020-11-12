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
    public class sWorkprocessAdo : Base
    {
        private static sWorkprocessAdo instant;

        public static sWorkprocessAdo GetInstant()
        {
            if (instant == null) instant = new sWorkprocessAdo();
            return instant;
        }

        private sWorkprocessAdo()
        {

        }

        public List<sWorkprocess> GetData(sWorkprocessReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();            

            sql = " Select * from [sWorkprocess] where 1 = 1";

            if (d.admin_seq != null)
            {
                sql += " admin_seq = " + d.admin_seq;
            }

            if (!String.IsNullOrEmpty(d.wp_code))
            {
                sql += " wp_code = " + QuoteStr(d.wp_code.Trim());
            }

            if (!String.IsNullOrEmpty(d.wp_desc))
            {
                sql += " wp_desc = " + QuoteStr(d.wp_desc.Trim());
            }

            if (d.wp_seq != null)
            {
                sql += " wp_seq = " + d.wp_seq;
            }

            var res = Query<sWorkprocess>(sql, param, conStr).ToList();
            return res;
        }

        public int Insert(sWorkprocessReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@wp_code", d.wp_code);
            param.Add("@wp_desc", d.wp_desc);
            param.Add("@wp_grp_desc", d.wp_grp_desc);
            param.Add("@admin_seq", d.admin_seq);
            param.Add("@Edit_userid", d.Edit_userid);

            sql = "INSERT INTO sWorkprocess (wp_code, wp_desc,wp_grp_desc,admin_seq, Edit_userid, Edit_date, Edit_datetime) " +
                "VALUES (@wp_code, @wp_desc,@wp_grp_desc,@admin_seq, @Edit_userid, DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())), GETDATE());";
            var res = ExecuteNonQuery(transac, sql, param, conStr);
            return res;
        }

        public int Update(sWorkprocessReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@wp_code", d.wp_code);
            param.Add("@wp_desc", d.wp_desc);
            param.Add("@wp_grp_desc", d.wp_grp_desc);
            param.Add("@Edit_userid", d.Edit_userid);
            param.Add("@wp_seq", d.wp_seq);

            sql = "Update sWorkprocess set wp_code = @wp_code , wp_desc = @wp_desc, wp_grp_desc = @wp_grp_desc, Edit_userid = @Edit_userid " +
                " , Edit_date = DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())), Edit_datetime = GETDATE()" +
                "Where wp_seq = @wp_seq ;";
            var res = ExecuteNonQuery(transac, sql, param, conStr);
            return res;
        }

        public int Delete(sWorkprocessReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@wp_seq", d.wp_seq);

            sql = "Delete from sWorkprocess " +
                "Where wp_seq = @wp_seq ;";
            var res = ExecuteNonQuery(transac, sql, param, conStr);
            return res;
        }

        public int DeleteByAdmin(sWorkprocessReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@admin_seq", d.admin_seq);

            sql = "Delete from sWorkprocess " +
                "Where admin_seq = @admin_seq ;";
            var res = ExecuteNonQuery(transac, sql, param, conStr);
            return res;
        }



    }
}
