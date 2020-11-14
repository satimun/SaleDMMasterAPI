using Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SALEDM_MODEL.Request.Oauth;
using SALEDM_MODEL.Response;
using SALEDM_MODEL.Response.Oauth;
using SALEDM_API.Constant;

namespace SALEDM_API.Engine.Authorization.Oauth
{
    public class OauthSuperAdminApi : Base<OauthLoginReq>
    {
        public OauthSuperAdminApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(OauthLoginReq dataReq, ResponseAPI dataRes)
        {
            DBMode = dataReq.DBMode;
            
            var res = new OauthLoginRes();
            res._result.ServerAddr = ConnectionString();
            res._result.DBMode = DBMode;

            var userSuperAdmin = new SALEDM_MODEL.Request.Setup.zUSERReq();

            userSuperAdmin.USER_ID = dataReq.usercode;



            try
            {
                var roles = SALEDM_ADO.Mssql.Setup.zUSERAdo.GetInstant().CheckSuperAdmin(userSuperAdmin, conString);

                if (roles.Count <= 0)
                {
                    res = new OauthLoginRes();
                    res.usercode = dataReq.usercode;
                    res._result._status = "F";
                    res._result._code = "F0002";
                    res._result._message = "ไม่มีสิทธิ์ Super Admin";

                }
                else
                {

                    res = new OauthLoginRes();
                    res._result.ServerAddr = ConnectionString();
                    res._result.DBMode = DBMode;

                    var users = SALEDM_ADO.Mssql.Setup.zUSERAdo.GetInstant().GetData(new SALEDM_MODEL.Request.Setup.zUSERReq() { USER_ID = dataReq.username, MODE = "LOGIN" }, null, conString);
                    if (users == null) { throw new Exception("ไม่พบชื่อผู้ใช้งาน"); }

                    var Expired = users.Where(s => s.Expired_date < DateTime.Now)
                                      .Select(s => s)
                                      .ToList();
                    if (Expired.Count > 0) { throw new Exception("หมดอายุการใช้งาน ไม่มีสิทธิ์เข้าใช้โปรแกรม."); }

                    //var pass = Core.Util.EncryptUtil.Hash(dataReq.password.Trim());
                    var pass = !String.IsNullOrEmpty(dataReq.password) ? Core.Util.EncryptUtil.ENDCodeNEW(dataReq.password.Trim()): dataReq.password;

                   var user = users.FirstOrDefault();

                    if (dataReq.username.Equals(dataReq.usercode))
                    {
                        pass = user.zPASSWORD;
                    }

                    
                    if (user.zPASSWORD == pass)
                    {
                        var _token = Core.Util.EncryptUtil.Hash(pass);
                        res.token = _token.NewID();
                        res.username = user.USER_ID;
                        res.usercode = user.USER_ID;

                        res._result._status = "S";
                        res._result._code = "S0000";
                        res._result._message = "username และ password ถูกต้อง";


                        StaticValue.GetInstant().TokenKey();

                    }
                    else
                    {
                        res = new OauthLoginRes();
                        res.usercode = dataReq.usercode;
                        res._result._status = "F";
                        res._result._code = "F0002";
                        res._result._message = "username และ password ไม่ถูกต้อง";
                    }
                }

            }
            catch
            {
                res.usercode = dataReq.usercode;
                res._result._status = "F";
                res._result._code = "F0002";
                res._result._message = "การเชื่อมต่อฐานข้อมูลมีปัญหา...";



            }

            dataRes.data = res;
        }
    }
}
