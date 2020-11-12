using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Apis.ERP;
using ASSETKKF_MODEL.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASSETKKF_API.Controllers.Send.Erp
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class BomController : Base
    {
        [HttpGet("ExSendReq")]
        public async Task<dynamic> ExSend()
        {
            var res = new BomExSendReqAPI();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, new IDCodeDesModel())));
        }

        [HttpPost("SendErp")]
        public async Task<dynamic> SendErp([FromBody] dynamic data)
        {
            var res = new BomSendErpApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, new IDCodeDesModel())));
        }
         
        [HttpPost("ReturnStatusErp")]
        public async Task<dynamic> ReturnStatusErp([FromBody] dynamic data)
        {
            var res = new BomReturnStatusErpApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));
        }
        

    }
}