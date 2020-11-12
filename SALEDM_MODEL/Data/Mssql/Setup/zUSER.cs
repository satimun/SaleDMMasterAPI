using System;
using System.Collections.Generic;
using System.Text;

namespace SALEDM_MODEL.Data.Mssql.Setup
{
    public class zUSER
    {
        public int? UserId { get; set; }
        public string USER_ID { get; set; }
        public string zPASSWORD { get; set; }
        public string GROUPID { get; set; }
        public string DETAIL { get; set; }
        public DateTime? LastLogin { get; set; }
        public string InpID { get; set; }
        public DateTime? InpDT { get; set; }
        public string BranCode { get; set; }
        public string BranCodePD { get; set; }
        public string BranCodeSt { get; set; }
        public string FullName { get; set; }
        public DateTime? Expired_date { get; set; }
        public int Day_Expired { get; set; }
        public string SYSTEMCOD { get; set; }
        public string SYSTEMDESC { get; set; }
        public string SYSTEMDATE { get; set; }
        public string SYSTEMDATETIME { get; set; }
        public string SYSTEMID { get; set; }
        public string ICQNO { get; set; }
        public string EMAIL { get; set; }

    }
}
