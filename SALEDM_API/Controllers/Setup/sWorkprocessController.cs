using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SALEDM_API.Engine.Setup;

namespace SALEDM_API.Controllers.Setup
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class sWorkprocessController : Base
    {
        public sWorkprocessController(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        [HttpPost("Workprocess")]
        public async Task<dynamic> Workprocess([FromBody] dynamic data)
        {
            var res = new sWorkprocessApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));
        }
    }
}
