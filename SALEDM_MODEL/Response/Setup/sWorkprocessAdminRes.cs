using System;
using System.Collections.Generic;
using System.Text;
using SALEDM_MODEL.Data.Mssql.Setup;

namespace SALEDM_MODEL.Response.Setup
{
    public class sWorkprocessAdminRes
    {
        public List<sWorkprocessAdmin> WorkprocessAdminLst { get; set; }
        public sWorkprocessAdmin WorkprocessAdmin { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();

    }
}
