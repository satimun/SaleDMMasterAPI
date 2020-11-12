using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SALEDM_MODEL.Enum
{
    public enum ErrorCode
    {
        [DisplayAttribute(Description = "Unknown.")]
        U000,

        /******Oauth*******/
        /// <summary>You do not have access.</summary>
        [DisplayAttribute(Description = "You do not have access.")]
        O000,

        /// <summary>You logout.</summary>
        [DisplayAttribute(Description = "You logout.")]
        O001,

        /// <summary>Login Time out.</summary>
        [DisplayAttribute(Description = "Login Time out.")]
        O002,

        /******Permission*******/
        /// <summary>You do not have permission.</summary>
        [DisplayAttribute(Description = "You do not have permission.")]
        P000,


        /******Valid*******/
        /// <summary>Please check I'm not a robot.</summary>
        [DisplayAttribute(Description = "Please check I'm not a robot.")]
        V000,

        /// <summary>Duplicate data.</summary>
        [DisplayAttribute(Description = "Duplicate data.")]
        V001,

        /// <summary>Save failed.</summary>
        [DisplayAttribute(Description = "Save failed.")]
        V002,

        /// <summary>Delete failed.</summary>
        [DisplayAttribute(Description = "Delete failed.")]
        V003,

    }
}
