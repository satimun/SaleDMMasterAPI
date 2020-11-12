using System;
using System.Collections.Generic;
using System.Text;

namespace SALEDM_MODEL.Response.Setup
{
    public class zUSERRes : DropdownListRes
    {
        public List<DropdownListRes> UserList { get; set; }
        public List<DropdownListRes> GroupList { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }
}
