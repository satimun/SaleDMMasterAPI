using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SALEDM_MODEL.Request
{
    public class FilesReq
    {
        public string fileName { get; set; }
        public string path { get; set; }
        public string fullpath { get; set; }
        public string file { get; set; }
        public string DBMode { get; set; }
        public string ConnStr { get; set; }
        public IFormFile FileToUpload { get; set; }
    }
}
