using System;
using System.Collections.Generic;
using System.Text;
using SALEDM_MODEL.Data.Mssql.Setup;

namespace SALEDM_MODEL.Response.Setup
{
    public class zUSERRes : DropdownListRes
    {
        public zUSER USER { get; set; }
        public bool permission { get; set; }
        public List<DropdownListRes> UserList { get; set; }
        public List<DropdownListRes> GroupList { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }
}
