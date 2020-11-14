using System;
using System.Collections.Generic;
using System.Text;

namespace SALEDM_MODEL.Request.Setup
{
    public class sWorkprocessAdminReq : IRequestModel
    {
        public string DBMode { get; set; }
        public string ConnStr { get; set; }
        public string MODE { get; set; }
        public string wpa_code { get; set; }
        public string wpa_desc { get; set; }
        public int? wpa_seq { get; set; }
        public int? wp_seq { get; set; }
        public string Edit_userid { get; set; }
        public string search { get; set; }
        public int[] id { get; set; }
    }
}
