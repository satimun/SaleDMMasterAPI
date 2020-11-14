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
    public class sAdministratorApi : Base<sAdministratorReq>
    {
        public sAdministratorApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(sAdministratorReq dataReq, ResponseAPI dataRes)
        {
            var res = new sAdministratorRes();
            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                res._result.DBMode = DBMode;

                var mode = !String.IsNullOrEmpty(dataReq.MODE) ? dataReq.MODE.Trim().ToLower() : dataReq.MODE;

                switch (mode)
                {
                    case "view":
                        res = getdata(dataReq, res, conString);
                        break;

                    case "add":
                        res = insert(dataReq, res, conString);
                        break;

                    case "edit":
                        res = update(dataReq, res, conString);
                        break;

                    case "delete":
                        if (dataReq.id != null && dataReq.id.Length > 0)
                        {
                            res = deleteAll(dataReq, res, conString);
                        }
                        else
                        {
                            res = delete(dataReq, res, conString);
                        }
                        
                        break;

                    default:
                        res = getall(dataReq,res, conString);
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

        private sAdministratorRes getall(sAdministratorReq dataReq, sAdministratorRes res, string conStr = null)
        {
            var lst = SALEDM_ADO.Mssql.Setup.sAdministratorAdo.GetInstant().GetData(dataReq, null, conStr);
            res.AdministratorLst = lst;

            return res;
        }

        private sAdministratorRes getdata(sAdministratorReq dataReq, sAdministratorRes res, string conStr = null)
        {
            var lst = SALEDM_ADO.Mssql.Setup.sAdministratorAdo.GetInstant().GetData(dataReq, null, conStr);
            res.Administrator = lst.FirstOrDefault();

            return res;
        }

        private sAdministratorRes insert(sAdministratorReq dataReq, sAdministratorRes res, string conStr = null)
        {
            try
            {     
                sAdministratorReq req1 = new sAdministratorReq()
                {
                    admin_code = dataReq.admin_code
                };
                var lst1 = SALEDM_ADO.Mssql.Setup.sAdministratorAdo.GetInstant().GetData(req1, null, conStr);
                if(lst1 !=null && lst1.Count > 0)
                {
                    throw new Exception("รหัส Function การทำงานในระบบซ้ำ");
                }

                var state = SALEDM_ADO.Mssql.Setup.sAdministratorAdo.GetInstant().Insert(dataReq, null, conStr);
                res._result._code = "200";
                res._result._message = "";
                res._result._status = "OK";
            }
            catch(Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            finally
            {
                var lst = SALEDM_ADO.Mssql.Setup.sAdministratorAdo.GetInstant().GetData(dataReq, null, conStr);
                res.Administrator = lst.FirstOrDefault();
            }
            

            return res;
        }

        private sAdministratorRes update(sAdministratorReq dataReq, sAdministratorRes res, string conStr = null)
        {
            sAdministratorReq req1 = new sAdministratorReq()
            {
                admin_seq = dataReq.admin_seq
            };

            try
            {
                
                var lst1 = SALEDM_ADO.Mssql.Setup.sAdministratorAdo.GetInstant().GetData(req1, null, conStr);
                if (lst1 != null && lst1.Count > 0)
                {
                    var state = SALEDM_ADO.Mssql.Setup.sAdministratorAdo.GetInstant().Update(dataReq, null, conStr);
                    res._result._code = "200";
                    res._result._message = "";
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
                var lst = SALEDM_ADO.Mssql.Setup.sAdministratorAdo.GetInstant().GetData(req1, null, conStr);
                res.Administrator = lst.FirstOrDefault();
            }


            return res;
        }

        private sAdministratorRes delete(sAdministratorReq dataReq, sAdministratorRes res, string conStr = null)
        {
            try
            {
                sAdministratorReq req1 = new sAdministratorReq()
                {
                    admin_seq = dataReq.admin_seq
                };
                var lst1 = SALEDM_ADO.Mssql.Setup.sAdministratorAdo.GetInstant().GetData(req1, null, conStr);
                if (lst1 != null && lst1.Count > 0)
                {
                    sWorkprocessReq req2 = new sWorkprocessReq()
                    {
                        admin_seq = dataReq.admin_seq
                    };
                    var lst2 = SALEDM_ADO.Mssql.Setup.sWorkprocessAdo.GetInstant().GetData(req2, null, conStr);
                    if (lst2 != null && lst2.Count > 0)
                    {
                        res._result._code = "400";
                        res._result._message = "ไม่สามารถลบข้อมูลได้ เนื่องจาก Function การทำงานในระบบนี้ถูกกำหนดรายละเอียดของการตรวจสอบสิทธิ์แล้ว";
                        res._result._status = "Bad Request";
                    }
                    else
                    {
                        var state = SALEDM_ADO.Mssql.Setup.sAdministratorAdo.GetInstant().Delete(dataReq, null, conStr);
                        res._result._code = "200";
                        res._result._message = "";
                        res._result._status = "OK";
                    }
                        
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
                var lst = SALEDM_ADO.Mssql.Setup.sAdministratorAdo.GetInstant().GetData(dataReq, null, conStr);
                res.Administrator = lst.FirstOrDefault();
            }


            return res;
        }

        private sAdministratorRes deleteAll(sAdministratorReq dataReq, sAdministratorRes res, string conStr = null)
        {
            try
            {
                string err = "";
                foreach (int seq in dataReq.id)
                {
                    sAdministratorReq req1 = new sAdministratorReq()
                    {
                        admin_seq = seq
                    };
                    var lst1 = SALEDM_ADO.Mssql.Setup.sAdministratorAdo.GetInstant().GetData(req1, null, conStr);
                    if (lst1 != null && lst1.Count > 0)
                    {
                        sWorkprocessReq req2 = new sWorkprocessReq()
                        {
                            admin_seq = seq
                        };
                        var lst2 = SALEDM_ADO.Mssql.Setup.sWorkprocessAdo.GetInstant().GetData(req2, null, conStr);
                        if (lst2 != null && lst2.Count > 0)
                        {
                            err += " Function การทำงานในระบบนี้ถูกกำหนดรายละเอียดของการตรวจสอบสิทธิ์แล้ว";
                        }
                        else
                        {
                            var state = SALEDM_ADO.Mssql.Setup.sAdministratorAdo.GetInstant().Delete(req1, null, conStr);
                        }

                    }
                    else
                    {
                        err += "ไม่พบข้อมูล ";
                    }
                }

                if (String.IsNullOrEmpty(err))
                {                    
                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";

                }
                else
                {
                    res._result._code = "400";
                    res._result._message = "ไม่สามารถลบข้อมูลได้ เนื่องจาก " + err;
                    res._result._status = "Bad Request";
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
                var lst = SALEDM_ADO.Mssql.Setup.sAdministratorAdo.GetInstant().GetData(dataReq, null, conStr);
                res.Administrator = lst.FirstOrDefault();
            }


            return res;
        }


    }
}
