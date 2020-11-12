using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SALEDM_MODEL.Enum
{
    public enum UpdateFlag
    {
        [DisplayAttribute(Name = "Add New", Order = 0)]
        ADD = 'A',

        [DisplayAttribute(Name = "Modify", Order = 1)]
        MODIFIY = 'M',

        [DisplayAttribute(Name = "Clone", Order = 2)]
        CLONE = 'C',
    }
}
