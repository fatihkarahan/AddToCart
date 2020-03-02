using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Enums
{
    /// <summary>The result enum.</summary>
    public enum ResultStatusEnum
    {
        /// <summary>The success.</summary>
        Success = 1,

        /// <summary>The un success.</summary>
        UnSuccess = 2,

        /// <summary>The return to url.</summary>
        ReturnToUrl = 6,

        /// <summary>The error.</summary>
        Error = 3,

        /// <summary>The exception.</summary>
        Exception = 4,

        /// <summary>The error.</summary>
        NoData = 5,

        /// <summary>The locked.</summary>
        Locked = 6,

        /// <summary>The validation error.</summary>
        ValidationError = 7,

        /// <summary>The access error.</summary>
        AccessError = 8
    }
}
