using System;
using System.Collections.Generic;
using System.Text;

namespace SALEDM_MODEL.Data.Mssql.Authorization
{
    public class muAccessToken
    {
        public string Code;
        public string IPAddress;
        public string Agent;
        public int CountUse;
        public string Status;
        public int? UpdateBy;
        public DateTime? Timestamp;
    }
}
