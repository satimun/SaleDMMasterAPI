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
    public class zGroupUserAdo : Base
    {
        private static zGroupUserAdo instant;

        public static zGroupUserAdo GetInstant()
        {
            if (instant == null) instant = new zGroupUserAdo();
            return instant;
        }

        private zGroupUserAdo()
        {

        }

        public List<zGroupUser> GetData(zUSERReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = "select * FROM [saledm].[dbo].[zGroupUser]";
            sql += " where 1 = 1";

            if (!String.IsNullOrEmpty(d.GROUPID))
            {
                sql += " and [GROUPID] = " + QuoteStr(d.GROUPID.Trim());
            }

            var res = Query<zGroupUser>(sql, param, conStr).ToList();
            return res;
        }

        public List<DropdownListRes> GetUserDDl(zUSERReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = "select [GROUPID] id, [GROUP_NAME] name ,([GROUPID] + ' : ' + [GROUP_NAME] ) description ";
            sql += " from FROM [saledm].[dbo].[zGroupUser]";

            var res = Query<DropdownListRes>(sql, param, conStr).ToList();
            return res;
        }

    }
}
