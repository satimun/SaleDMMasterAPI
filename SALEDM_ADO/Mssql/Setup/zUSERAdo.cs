using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using SALEDM_MODEL.Data.Mssql.Setup;
using SALEDM_MODEL.Request.Setup;
using SALEDM_MODEL.Response.Setup;

namespace SALEDM_ADO.Mssql.Setup
{
    public class zUSERAdo : Base
    {
        private static zUSERAdo instant;

        public static zUSERAdo GetInstant()
        {
            if (instant == null) instant = new zUSERAdo();
            return instant;
        }

        private zUSERAdo()
        {

        }

        public List<zUSER> GetData(zUSERReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = "select *  FROM [saledm].[dbo].[zUSER]";
            sql += " where 1 = 1";

            if (String.IsNullOrEmpty(d.MODE))
            {
                sql += " and DATEADD(dd, 0, DATEDIFF(dd, 0, [Expired_date])) >= DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))";
            }

            if (!String.IsNullOrEmpty(d.USER_ID))
            {
                sql += " and [USER_ID] = " + QuoteStr(d.USER_ID.Trim());
            }

            if (!String.IsNullOrEmpty(d.GROUPID))
            {
                sql += " and [GROUPID] = " + QuoteStr(d.GROUPID.Trim());
            }

            if (!String.IsNullOrEmpty(d.search))
            {
                sql += " and ( USER_ID like " + QuoteStr("%" + d.search.Trim() + "%");
                sql += " or detail like " + QuoteStr("%" + d.search.Trim() + "%") + ")";
            }

            var res = Query<zUSER>(sql, param, conStr).ToList();
            return res;
        }

        public List<DropdownListRes> GetUserDDl(zUSERReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = "select [USER_ID] id, [DETAIL] name ,([USER_ID] + ' : ' + [DETAIL] ) description ";
            sql += "  FROM [saledm].[dbo].[zUSER]";
            sql += " where DATEADD(dd, 0, DATEDIFF(dd, 0, [Expired_date])) >= DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))";

            if (!String.IsNullOrEmpty(d.USER_ID))
            {
                sql += " and [USER_ID] = " + QuoteStr(d.USER_ID.Trim());
            }

            if (!String.IsNullOrEmpty(d.GROUPID))
            {
                sql += " and [GROUPID] = " + QuoteStr(d.GROUPID.Trim());
            }

            if (!String.IsNullOrEmpty(d.search))
            {
                sql += " and ( USER_ID like " + QuoteStr("%" + d.search.Trim() + "%");
                sql += " or detail like " + QuoteStr("%" + d.search.Trim() + "%") + ")";
            }

            var res = Query<DropdownListRes>(sql, param, conStr).ToList();
            return res;
        }

        public List<zUSER> CheckSuperAdmin(zUSERReq d, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@USERCODE", d.USER_ID);

            sql = "select *  FROM [saledm].[dbo].[zUSER]";
            sql += " where GROUPID IN ('01') ";
            if (!String.IsNullOrEmpty(d.USER_ID))
            {
                sql += " and [USER_ID] = " + QuoteStr(d.USER_ID.Trim());
            }

            var res = Query<zUSER>(sql, param, conStr).ToList();
            return res;
        }

        public List<zUSER> CheckPermission(zUSERReq d, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " Select * from [sAdministrator] A , [sWorkprocess] B , [sWorkprocessAdmin] C";
            sql += " where a.admin_seq = B.admin_seq and B.wp_seq = C.wp_seq";
            sql += " and admin_code = " + QuoteStr(d.admin_code.Trim());
            sql += " and wp_code = " + QuoteStr(d.wp_code.Trim());
            sql += " and wpa_code = " + QuoteStr(d.USER_ID.Trim());

            var res = Query<zUSER>(sql, param, conStr).ToList();
            return res;
        }



    }
}
