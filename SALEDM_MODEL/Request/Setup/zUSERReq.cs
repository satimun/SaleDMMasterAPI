﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SALEDM_MODEL.Request.Setup
{
    public class zUSERReq : IRequestModel
    {
        public string DBMode { get; set; }
        public string ConnStr { get; set; }
        public string MODE { get; set; }
        public string USER_ID { get; set; }
        public string GROUPID { get; set; }

    }
}
