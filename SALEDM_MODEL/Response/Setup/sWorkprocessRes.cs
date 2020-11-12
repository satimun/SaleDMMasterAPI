using System;
using System.Collections.Generic;
using System.Text;
using SALEDM_MODEL.Data.Mssql.Setup;

namespace SALEDM_MODEL.Response.Setup
{
    public class sWorkprocessRes
    {
        public List<sWorkprocess> WorkprocessList { get; set; }
        public sWorkprocess Workprocess { get; set; }
        public List<sWorkprocessAdmin> WorkprocessAdminLst { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }
}
