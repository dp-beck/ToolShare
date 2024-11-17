using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShare.Data;
using ToolShare.Data.Models;

namespace ToolShare.UI.DTOs
{
    public class AppUserDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AboutMe { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public int PodJoinedId { get; set; }
        public string PodJoinedName { get; set; }
        public string PodManagedName { get; set; }
    }
}