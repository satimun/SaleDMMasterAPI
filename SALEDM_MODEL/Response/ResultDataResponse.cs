using System;
using System.Collections.Generic;
using System.Text;

namespace SALEDM_MODEL.Response
{
    public class ResultDataResponse
    {
        public string _status { get; set; }
        public string _code { get; set; }
        public string _message { get; set; }
        public string DBMode { get; set; }
        public string ConnStr { get; set; }
        public string ServerAddr { get; set; }
    }
}
