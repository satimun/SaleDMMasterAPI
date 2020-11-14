using System;
using System.Collections.Generic;
using System.Text;

namespace SALEDM_MODEL.Data.Mssql.Setup
{
    public class sWorkprocessAdmin
    {
        public string wpa_code { get; set; }
        public string wpa_desc { get; set; }
        public int? wpa_seq { get; set; }
        public int? wp_seq { get; set; }
        public string Edit_userid { get; set; }
        public DateTime? Edit_date { get; set; }
        public DateTime? Edit_datetime { get; set; }
        public string Edit_userdetail { get; set; }
    }
}
