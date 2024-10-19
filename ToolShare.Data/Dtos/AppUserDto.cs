using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShare.Data.Dtos
{
    public class AppUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AboutMe { get; set; }
        public string ProfilePhotoUrl { get; set; }        
    }
}