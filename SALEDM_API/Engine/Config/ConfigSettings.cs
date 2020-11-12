using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SALEDM_MODEL.Request.Config;
using SALEDM_MODEL.Response;

namespace SALEDM_API.Engine.Config
{
    public class ConfigSettings : Base<ConfigSetting>
    {
        public ConfigSettings(IConfiguration configuration)
        {
            PermissionKey = "ADMIN";
            Configuration = configuration;

        }


        protected override void ExecuteChild(ConfigSetting dataReq, ResponseAPI dataRes)
        {
            var res = new ConfigSetting();
            try
            {
                DBMode = dataReq.DBMode;
                dataRes.ServerAddr = ConnectionString();
                if (dataReq != null)
                {
                    res.DBMode = dataReq.DBMode;
                    res.Status = true;
                    switch (dataReq.DBMode)
                    {                        
                        case "1":
                            SALEDM_ADO.Mssql.Base.conString = Configuration["ConnSALEDMBak"];
                            res.ConnStr = SALEDM_ADO.Mssql.Base.conString;                            

                            break;

                        case "2":
                            SALEDM_ADO.Mssql.Base.conString = Configuration["ConnSALEDMLocal"];
                            res.ConnStr = SALEDM_ADO.Mssql.Base.conString;

                            break;
                        default:
                            SALEDM_ADO.Mssql.Base.conString =  Configuration["ConnSALEDM"];
                            res.ConnStr = SALEDM_ADO.Mssql.Base.conString;
                            
                            break;
                    }

                    if (!String.IsNullOrEmpty(res.ConnStr))
                    {
                        var arrCon = res.ConnStr.Split(";");
                        if (arrCon.Length > 0)
                        {
                            var arrServer = arrCon[0].Split("=");
                            res.ServerAddr = arrServer[1];
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                res.Status = false;
                res.Message = "ไม่สามารถกำหนดค่า Connection String ได้";
            }
            dataRes.data = res;
        }
    }
}
