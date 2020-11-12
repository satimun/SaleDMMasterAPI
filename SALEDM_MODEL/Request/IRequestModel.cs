using System;
using System.Collections.Generic;
using System.Text;

namespace SALEDM_MODEL.Request
{
    public interface IRequestModel
    {
         string DBMode { get; set; }
         string ConnStr { get; set; }
         string MODE { get; set; }
    }
}
