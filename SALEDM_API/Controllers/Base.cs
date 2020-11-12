using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SALEDM_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALEDM_API.Controllers
{
    public abstract class Base : ControllerBase
    {
        public IConfiguration Configuration { get; set; }
        public dynamic ResponeValid(ResponseAPI res)
        {
            if (res.status == "F")
            {
                return StatusCode(404, new { code = res.code, message = res.message });
            }
            return res.data;
        }
    }
}
