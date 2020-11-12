
using Microsoft.Extensions.Configuration;
using SALEDM_API.Constant;
using SALEDM_MODEL.Response;
using SALEDM_MODEL.Response.Oauth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALEDM_API.Engine.Authorization.Oauth
{
    public class OauthAccessTokenGetApi : Base<dynamic>
    {
        public OauthAccessTokenGetApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(dynamic dataReq, ResponseAPI dataRes)
        {
            DBMode = Convert.ToString(dataReq);
            dataRes.ServerAddr = ConnectionString();
            //dataRes.DBMode = DBMode;
            var res = SALEDM_ADO.Mssql.Authorization.muAccessTokenAdo.GetInstant().Search(AccessToken,null,conString);

            if (res.Count == 1)
            {
                SALEDM_ADO.Mssql.Authorization.muAccessTokenAdo.GetInstant().Update(this.AccessToken,null,conString);
            }
            else
            {
                this.AccessToken = Core.Util.EncryptUtil.NewID(this.IPAddress);
                SALEDM_ADO.Mssql.Authorization.muAccessTokenAdo.GetInstant().Insert(this.AccessToken, this.IPAddress, this.UserAgent,null,conString);
            }

            dataRes.data = new OauthAccessTokenGetRes() { accessToken = this.AccessToken };

            StaticValue.GetInstant().AccessKey();

        }
    }
}
