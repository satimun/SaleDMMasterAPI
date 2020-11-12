using System;
using System.Collections.Generic;
using System.Text;

namespace SALEDM_MODEL.Response.Oauth
{
    public class OauthLoginRes
    {
        public string usercode { get; set; }
        public string username { get; set; }
        public string token { get; set; }
        public string GROUPID { get; set; }
        public string BranCode { get; set; }
        public string BranCodePD { get; set; }
        public string BranCodeSt { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? Expired_date { get; set; }

        public string ICQNO { get; set; }
        public string EMAIL { get; set; }

        public  List<OauthLoginRes> UserLst { get; set; }

        public ResultDataResponse _result = new ResultDataResponse();
    }

  
}
