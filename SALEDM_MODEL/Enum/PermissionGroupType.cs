using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SALEDM_MODEL.Enum
{
    public enum PermissionGroupType
    {
        [DisplayAttribute(Name = "Page View", Order = 0)]
        PAGEVIEW = 'V',

        [DisplayAttribute(Name = "Button", Order = 1)]
        BUTTON = 'B',

        [DisplayAttribute(Name = "API", Order = 2)]
        API = 'A',

    }
}
