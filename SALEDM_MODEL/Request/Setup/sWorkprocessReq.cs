using System;
using System.Collections.Generic;
using System.Text;

namespace SALEDM_MODEL.Request.Setup
{
    public class sWorkprocessReq : IRequestModel
    {
        public string DBMode { get; set; }
        public string ConnStr { get; set; }
        public string MODE { get; set; }
        public string wp_code { get; set; }
        public string wp_desc { get; set; }
        public int? wp_seq { get; set; }
        public string wp_grp_desc { get; set; }
        public int? admin_seq { get; set; }
        public string Edit_userid { get; set; }
    }
}
