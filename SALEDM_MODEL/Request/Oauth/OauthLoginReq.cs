using System;
using System.Collections.Generic;
using System.Text;

namespace SALEDM_MODEL.Request.Oauth
{
    public class OauthLoginReq : IRequestModel
    {
        public string DBMode { get; set; }
        public string ConnStr { get; set; }
        public string MODE { get; set; }
        public string usercode { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string twofactor { get; set; }
    }
}
