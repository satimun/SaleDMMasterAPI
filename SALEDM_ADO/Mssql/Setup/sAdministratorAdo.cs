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
    public class sAdministratorAdo : Base
    {
        private static sAdministratorAdo instant;

        public static sAdministratorAdo GetInstant()
        {
            if (instant == null) instant = new sAdministratorAdo();
            return instant;
        }

        private sAdministratorAdo()
        {

        }

        public List<sAdministrator> GetData(sAdministratorReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = " Select A.* ,A.Edit_userid + ' : ' + U.DETAIL Edit_userdetail ";
            sql += " from [sAdministrator] A left outer join zUSER U on U.USER_ID = A.Edit_userid";
            sql += "  where 1 =1";

            if (!String.IsNullOrEmpty(d.admin_code))
            {
                sql += " and admin_code = " + QuoteStr(d.admin_code.Trim());
            }

            if (!String.IsNullOrEmpty(d.admin_desc))
            {
                sql += " and admin_desc = " + QuoteStr(d.admin_desc.Trim() );
            }

            if(d.admin_seq != null)
            {
                sql += " and admin_seq = " + d.admin_seq;
            }

            if (!String.IsNullOrEmpty(d.search))
            {
                sql += " and ( admin_code like " + QuoteStr("%" + d.search.Trim() + "%");
                sql += " or admin_desc like " + QuoteStr("%" + d.search.Trim() + "%") + ")";
            }


            var res = Query<sAdministrator>(sql, param, conStr).ToList();
            return res;
        }

        public int Insert(sAdministratorReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@admin_code", d.admin_code);
            param.Add("@admin_desc", d.admin_desc);
            param.Add("@Edit_userid", d.Edit_userid);

            sql = "INSERT INTO sAdministrator (admin_code, admin_desc, Edit_userid, Edit_date, Edit_datetime) " +
                "VALUES (@admin_code, @admin_desc, @Edit_userid, DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())), GETDATE());";
            var res = ExecuteNonQuery(transac, sql, param, conStr);
            return res;
        }

        public int Update(sAdministratorReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@admin_code", d.admin_code);
            param.Add("@admin_desc", d.admin_desc);
            param.Add("@Edit_userid", d.Edit_userid);
            param.Add("@admin_seq", d.admin_seq);

            sql = "Update sAdministrator set admin_code = @admin_code , admin_desc = @admin_desc, Edit_userid = @Edit_userid " +
                " , Edit_date = DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())), Edit_datetime = GETDATE()" +
                "Where admin_seq = @admin_seq ;";
            var res = ExecuteNonQuery(transac, sql, param, conStr);
            return res;
        }

        public int Delete(sAdministratorReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@admin_seq", d.admin_seq);

            sql = "Delete from sAdministrator " +
                "Where admin_seq = @admin_seq ;";
            var res = ExecuteNonQuery(transac, sql, param, conStr);
            return res;
        }


    }
}
