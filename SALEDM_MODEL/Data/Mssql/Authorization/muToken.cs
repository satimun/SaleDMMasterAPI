using System;
using System.Collections.Generic;
using System.Text;

namespace SALEDM_MODEL.Data.Mssql.Authorization
{
    public class muToken
    {
        public string Code;
        public string UserCode;
        public string AccessToken_Code;
        public DateTime ExpiryTime;
        public string Type;
        public string Status;
        public string UpdateBy;
        public DateTime? Timestamp;
    }
}
