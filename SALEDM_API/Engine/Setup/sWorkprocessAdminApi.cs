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
    public class sWorkprocessAdminApi : Base<sWorkprocessAdminReq>
    {
        public sWorkprocessAdminApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(sWorkprocessAdminReq dataReq, ResponseAPI dataRes)
        {
            var res = new sWorkprocessAdminRes();
            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                res._result.DBMode = DBMode;

                var mode = !String.IsNullOrEmpty(dataReq.MODE) ? dataReq.MODE.Trim().ToLower() : dataReq.MODE;

                switch (mode)
                {
                    case "getdata":
                        res = getdata(dataReq, res, conString);
                        break;

                    case "insert":
                        res = insert(dataReq, res, conString);
                        break;

                    case "update":
                        res = update(dataReq, res, conString);
                        break;

                    case "delete":
                        res = delete(dataReq, res, conString);
                        break;

                    default:
                        res = getall(dataReq, res, conString);
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

        private sWorkprocessAdminRes getall(sWorkprocessAdminReq dataReq, sWorkprocessAdminRes res, string conStr = null)
        {
            var lst = SALEDM_ADO.Mssql.Setup.sWorkprocessAdminAdo.GetInstant().GetData(dataReq, null, conStr);
            res.WorkprocessAdminLst = lst;

            return res;
        }

        private sWorkprocessAdminRes getdata(sWorkprocessAdminReq dataReq, sWorkprocessAdminRes res, string conStr = null)
        {
            var lst = SALEDM_ADO.Mssql.Setup.sWorkprocessAdminAdo.GetInstant().GetData(dataReq, null, conStr);
            res.WorkprocessAdmin = lst.FirstOrDefault();

            return res;
        }

        private sWorkprocessAdminRes insert(sWorkprocessAdminReq dataReq, sWorkprocessAdminRes res, string conStr = null)
        {
            try
            {
                sWorkprocessAdminReq req1 = new sWorkprocessAdminReq()
                {
                    wpa_code = dataReq.wpa_code,
                    wp_seq = dataReq.wp_seq
                };
                var lst1 = SALEDM_ADO.Mssql.Setup.sWorkprocessAdminAdo.GetInstant().GetData(req1, null, conStr);
                if (lst1 != null && lst1.Count > 0)
                {
                    throw new Exception("รหัสผู้มีสิทธิ์การทำงานในระบบซ้ำ");
                }

                var state = SALEDM_ADO.Mssql.Setup.sWorkprocessAdminAdo.GetInstant().Insert(dataReq, null, conStr);
                res._result._code = "200";
                res._result._message = "บันทึกข้อมูลเรียบร้อยแล้ว";
                res._result._status = "OK";
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            finally
            {
                var lst = SALEDM_ADO.Mssql.Setup.sWorkprocessAdminAdo.GetInstant().GetData(dataReq, null, conStr);
                res.WorkprocessAdminLst = lst;
            }


            return res;
        }

        private sWorkprocessAdminRes update(sWorkprocessAdminReq dataReq, sWorkprocessAdminRes res, string conStr = null)
        {
            sWorkprocessAdminReq req1 = new sWorkprocessAdminReq()
            {
                wpa_seq = dataReq.wpa_seq
            };

            try
            {

                var lst1 = SALEDM_ADO.Mssql.Setup.sWorkprocessAdminAdo.GetInstant().GetData(req1, null, conStr);
                if (lst1 != null && lst1.Count > 0)
                {
                    var state = SALEDM_ADO.Mssql.Setup.sWorkprocessAdminAdo.GetInstant().Update(dataReq, null, conStr);
                    res._result._code = "200";
                    res._result._message = "บันทึกข้อมูลเรียบร้อยแล้ว";
                    res._result._status = "OK";
                }
                else
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Not Found";
                }


            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            finally
            {
                var lst = SALEDM_ADO.Mssql.Setup.sWorkprocessAdminAdo.GetInstant().GetData(dataReq, null, conStr);
                res.WorkprocessAdmin = lst.FirstOrDefault();
            }


            return res;
        }

        private sWorkprocessAdminRes delete(sWorkprocessAdminReq dataReq, sWorkprocessAdminRes res, string conStr = null)
        {
            try
            {
                sWorkprocessAdminReq req1 = new sWorkprocessAdminReq()
                {
                    wpa_seq = dataReq.wpa_seq
                };
                var lst1 = SALEDM_ADO.Mssql.Setup.sWorkprocessAdminAdo.GetInstant().GetData(req1, null, conStr);
                if (lst1 != null && lst1.Count > 0)
                {
                    var state = SALEDM_ADO.Mssql.Setup.sWorkprocessAdminAdo.GetInstant().Delete(dataReq, null, conStr);
                    res._result._code = "200";
                    res._result._message = "ลบข้อมูลเรียบร้อยแล้ว";
                    res._result._status = "OK";

                }
                else
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Not Found";
                }


            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            finally
            {
                var lst = SALEDM_ADO.Mssql.Setup.sWorkprocessAdminAdo.GetInstant().GetData(dataReq, null, conStr);
                res.WorkprocessAdminLst = lst;
            }


            return res;
        }


    }
}
