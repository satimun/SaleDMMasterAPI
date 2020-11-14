using Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SALEDM_MODEL.Request.Oauth;
using SALEDM_API.Constant;
using SALEDM_MODEL.Response;
using SALEDM_MODEL.Response.Oauth;

namespace SALEDM_API.Engine.Authorization.Oauth
{
    public class OauthLoginApi : Base<OauthLoginReq>
    {
        public OauthLoginApi(IConfiguration configuration)
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

            var user = SALEDM_ADO.Mssql.Setup.zUSERAdo.GetInstant().GetData(new SALEDM_MODEL.Request.Setup.zUSERReq() { USER_ID = dataReq.username, MODE = "LOGIN" },null,conString);
            if (user == null) { throw new Exception("ไม่พบชื่อผู้ใช้งาน"); }

            var Expired = user.Where(s => s.Expired_date < DateTime.Now )
                              .Select(s => s)
                              .ToList();
            if (Expired.Count > 0) { throw new Exception("หมดอายุการใช้งาน ไม่มีสิทธิ์เข้าใช้โปรแกรม."); }           

            var pass = Core.Util.EncryptUtil.ENDCodeNEW(dataReq.password.Trim());
            pass = dataReq.password.Trim();
            var Password = user.Where(s => s.zPASSWORD == pass)
                              .Select(s => s)
                              .ToList();

            
            if (Password.Count > 0)
            {
                var _token = Core.Util.EncryptUtil.Hash(pass);
                res.token = _token.NewID();

                var obj = user.FirstOrDefault();
                res.username = obj.DETAIL;
                res.usercode = obj.USER_ID;
                res.GROUPID = obj.GROUPID;
                res.BranCode = obj.BranCode;
                res.BranCodePD = obj.BranCodePD;
                res.BranCodeSt = obj.BranCodeSt;
                res.ICQNO = obj.ICQNO;
                res.EMAIL = obj.EMAIL;
                res.Expired_date = obj.Expired_date;

                SALEDM_ADO.Mssql.Authorization.muTokenAdo.GetInstant().Insert(new SALEDM_MODEL.Data.Mssql.Authorization.muToken()
                {
                    UserCode = obj.USER_ID,
                    AccessToken_Code = this.AccessToken,
                    Code = res.token,
                    Status = "A",
                    Type = "L",
                    ExpiryTime = DateTime.Now.AddMinutes(480)

                }, obj.USER_ID, null,conString);


                /*
                if (config.TrueForAll(x => x.EmailLogin == true))
                {
                    var access = Ado.Mssql.Table.AccessToken.GetInstant().Search(this.AccessToken).FirstOrDefault();
                    string subject = "Login Notification";
                    string body = $"<p><b>Dear {user.Username} ,</b></p>" +
                    $"<p>This is notify you of a successful login to your account.</p>" +
                    $"<p>Login Time: {DateTime.UtcNow.ToString()}</p>" +
                    $"<p>IP Address: {access.IPAddress}</p>" +
                    $"<p>User Agent: {access.Agent}</p>";

                    Task.Run(() => Core.SendMail.SendMail.Send(user.Email, subject, body));
                }
               */
                dataRes.data = res;
                StaticValue.GetInstant().TokenKey();
            }
            else
            {
                throw new Exception("Username or Password was incorrect");
            }

        }
    }
}
