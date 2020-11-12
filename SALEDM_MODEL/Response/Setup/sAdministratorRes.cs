using System;
using System.Collections.Generic;
using System.Text;
using SALEDM_MODEL.Data.Mssql.Setup;

namespace SALEDM_MODEL.Response.Setup
{
    public class sAdministratorRes
    {        
        public List<sAdministrator> AdministratorLst { get; set; }
        public sAdministrator Administrator { get; set; }
        public List<sWorkprocess> WorkprocessList { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }
}
