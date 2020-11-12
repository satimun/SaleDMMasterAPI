using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SALEDM_MODEL.Enum
{ 
    public enum StatusFlagGroupType
    {
        [DisplayAttribute(Name = "Approval status", Order = 0)]
        APPROVAL = 'A',

        [DisplayAttribute(Name = "Document status", Order = 1)]
        DOCUMENT = 'D',
    }
}
