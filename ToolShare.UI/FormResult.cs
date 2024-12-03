using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShare.UI.Identity.Models
{
    /// <summary>
    /// Response for login and registration.
    /// </summary>
    public class FormResult
    {
         /// <summary>
        /// Gets or sets a value indicating whether the action was successful.
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// On failure, the problem details are parsed and returned in this array.
        /// </summary>
        public string[] ErrorList { get; set; } = [];
    }
}