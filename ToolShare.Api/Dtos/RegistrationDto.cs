using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShare.Api.Dtos
{
    public class RegistrationDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string AboutMe { get; set; }
        public string ProfilePhotoUrl { get; set; } = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1731154587/blank-profile-picture-973460_1280_pxrhwm.png";
    }
}