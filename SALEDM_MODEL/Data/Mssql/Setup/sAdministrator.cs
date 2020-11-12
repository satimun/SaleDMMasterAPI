using System;
using System.Collections.Generic;
using System.Text;

namespace SALEDM_MODEL.Data.Mssql.Setup
{
    public class sAdministrator
    {
        public string admin_code { get; set; }
        public string admin_desc { get; set; }
        public int? admin_seq { get; set; }
        public string Edit_userid { get; set; }
        public DateTime? Edit_date { get; set; }
        public DateTime? Edit_datetime { get; set; }
    }
}
