using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SALEDM_API.Engine.Config;

namespace SALEDM_API.Controllers.Config
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class ConfigSettingController : Base
    {
        public ConfigSettingController(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        [HttpPost("GetData")]
        public async Task<dynamic> GetData([FromBody] dynamic data)
        {
            var res = new ConfigSettings(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }
    }
}
