using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SALEDM_MODEL.Enum
{
    public enum ActionFlag
    {
        [DisplayAttribute(Name = "Operate", Order = 0)]
        OPERATE = 'O',

        [DisplayAttribute(Name = "Not Approved", Order = 1)]
        NOTAPPROVED = 'N',

        [DisplayAttribute(Name = "Approved", Order = 2)]
        APPROVED = 'A',
    }
}
