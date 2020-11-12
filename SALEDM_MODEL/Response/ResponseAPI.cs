using System;
using System.Collections.Generic;
using System.Text;

namespace SALEDM_MODEL.Response
{
    public class ResponseAPI
    {
        public string status { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public dynamic data = "";
        public string ServerAddr { get; set; }
    }
}
