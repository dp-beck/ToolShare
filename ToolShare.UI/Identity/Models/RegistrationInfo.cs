using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Cache;
using System.Threading.Tasks;

namespace ToolShare.UI.Identity.Models
{
    /// <summary>
    /// Registration info provided by a new user
    /// </summary>
    public class RegistrationInfo
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Password { get; set; }
        public string? AboutMe { get; set; }
    }
}