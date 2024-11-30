using System.ComponentModel.DataAnnotations;

namespace ToolShare.Api.Dtos
{
    public class RegistrationDto
    {
        [StringLength(maximumLength: 20, MinimumLength = 5)]
        public required string UserName { get; set; }
        
        [StringLength(maximumLength: 50, MinimumLength = 5)]
        [EmailAddress]
        public required string Email { get; set; }
        
        [StringLength(50)]
        public string? FirstName { get; set; }
        
        [StringLength(50)]
        public string? LastName { get; set; }

        public required string Password { get; set; }

        [StringLength(500)]
        public string? AboutMe { get; set; }
        
        [StringLength(500)]
        public string ProfilePhotoUrl { get; set; } = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1731154587/blank-profile-picture-973460_1280_pxrhwm.png";
    }
}