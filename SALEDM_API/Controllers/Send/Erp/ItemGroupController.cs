using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Apis.ERP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASSETKKF_API.Controllers.Send.Erp
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class ItemGroupController :   Base
    {
        [HttpPost("Sync")]
        public async Task<dynamic> SyneErp([FromBody] dynamic data)
        {
            var res = new ItemGroupSyncApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

    }
}