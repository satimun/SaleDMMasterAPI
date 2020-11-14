using System;
using System.Collections.Generic;
using System.Text;

namespace SALEDM_MODEL.Data.Mssql.Setup
{
    public class sWorkprocess
    {
        public string wp_code { get; set; }
        public string wp_desc { get; set; }
        public int? wp_seq { get; set; }
        public string wp_grp_desc { get; set; }
        public int? admin_seq { get; set; }
        public string Edit_userid { get; set; }
        public DateTime? Edit_date { get; set; }
        public DateTime? Edit_datetime { get; set; }
        public string Edit_userdetail { get; set; }
    }
}
