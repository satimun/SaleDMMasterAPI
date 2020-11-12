using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SALEDM_MODEL.Enum
{
    public enum Status
    {
        [DisplayAttribute(Name = "Active", Order = 0)]
        ACTIVE = 'A',

        [DisplayAttribute(Name = "Inactive", Order = 1)]
        INACTIVE = 'I',

        [DisplayAttribute(Name = "Cancel", Order = 2)]
        CANCEL = 'C',
    }
}
