using System;
using System.Collections.Generic;
using System.Text;

namespace SALEDM_MODEL.Request.Line
{
    public class NotifyPushMessage : IRequestModel
    {
        public string DBMode { get; set; }
        public string ConnStr { get; set; }
        public string MODE { get; set; }
        public string type { get; set; }
        public string message { get; set; }
        public string imageFullsize { get; set; }
        public string imageThumbnail { get; set; }
        public string token { get; set; }
    }
}
