using System;
using System.Collections.Generic;
using System.Text;

namespace SALEDM_MODEL.Common
{
    public class SearchReq
    {
        public string txtSearch { get; set; }
        public List<int?> ID { get; set; }
        public List<string> Code { get; set; }
        public List<string> Status { get; set; }
    }
}
