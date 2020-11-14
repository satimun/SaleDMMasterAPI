using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SALEDM_MODEL.Request.Setup;
using SALEDM_MODEL.Response;
using SALEDM_MODEL.Response.Setup;

namespace SALEDM_API.Engine.Setup
{
    public class zUSERApi : Base<zUSERReq>
    {
        public zUSERApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(zUSERReq dataReq, ResponseAPI dataRes)
        {
            var res = new zUSERRes();
            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                res._result.DBMode = DBMode;

                var mode = !String.IsNullOrEmpty(dataReq.MODE) ? dataReq.MODE.Trim().ToLower() : dataReq.MODE;
                switch (mode)
                {
                    case "permission":
                        res = CheckPermission(dataReq, res, conString);
                        break;
                    default:
                        res = getdata(dataReq, res, conString);
                        break;
                }
            }
            catch (SqlException ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Execute exception Error";
            }
            catch (InvalidOperationException ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Connection Exception Error";
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            dataRes.data = res;
        }

        private zUSERRes getdata(zUSERReq dataReq, zUSERRes res, string conStr = null)
        {
            var lst = SALEDM_ADO.Mssql.Setup.zUSERAdo.GetInstant().GetData(dataReq, null, conStr);
            res.USER = lst.FirstOrDefault();

            return res;
        }

        private zUSERRes CheckPermission(zUSERReq dataReq, zUSERRes res, string conStr = null)
        {
            var lst = SALEDM_ADO.Mssql.Setup.zUSERAdo.GetInstant().CheckPermission(dataReq,  conStr);
            var state = lst != null && lst.Count > 0 ? true : false;

            res.permission = state;

            return res;
        }


    }
}
