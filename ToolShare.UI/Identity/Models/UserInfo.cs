using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShare.UI.Identity.Models
{
    /// <summary>
    /// User info from identity endpoint to establish claims.
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// The email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// A value indicating whether the email has been confirmed yet.
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// The list of claims for the user.
        /// </summary>
        public Dictionary<string, string> Claims { get; set; } = [];

        public string userName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string AboutMe { get; set; } = string.Empty;
        public string ProfilePhotoUrl { get; set; } = string.Empty;
    }
}