using System;
using System.Collections.Generic;
using System.Text;

namespace SALEDM_MODEL.Response.File
{
    public class FileUploadRes
    {
        public string fileName { get; set; }
        public string path { get; set; }
        public string fullpath { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }
}
