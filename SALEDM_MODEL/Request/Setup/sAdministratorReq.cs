using System;
using System.Collections.Generic;
using System.Text;

namespace SALEDM_MODEL.Request.Setup
{
    public class sAdministratorReq : IRequestModel
    {
        public string DBMode { get; set; }
        public string ConnStr { get; set; }
        public string MODE { get; set; }
        public string admin_code { get; set; }
        public string admin_desc { get; set; }
        public int? admin_seq { get; set; }
        public string Edit_userid { get; set; }
        public string search { get; set; }
        public int[] id { get; set; }
    }
}
