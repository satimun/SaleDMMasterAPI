using Microsoft.Extensions.Configuration;
using SALEDM_API.Constant;
using SALEDM_MODEL.Request.Oauth;
using SALEDM_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALEDM_API.Engine.Authorization.Oauth
{
    public class OauthLogoutApi : Base<OauthLoginReq>
    {
        public OauthLogoutApi(IConfiguration configuration)
        {
            AllowAnonymous = false;
            Configuration = configuration;
        }

        protected override void ExecuteChild(OauthLoginReq dataReq, ResponseAPI dataRes)
        {
            DBMode = dataReq.DBMode;
            dataRes.ServerAddr = ConnectionString();
            SALEDM_ADO.Mssql.Authorization.muTokenAdo.GetInstant().Delete(this.Token, dataReq.usercode,null,conString);

            StaticValue.GetInstant().TokenKey();
        }
    }
}
