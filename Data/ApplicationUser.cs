using Microsoft.AspNetCore.Identity;

namespace ToolShare.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set;}
    public string? AboutMe { get; set; }
    public string? ProfilePhotoUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int? BelongsToPod { get; set; }
    public int? OwnsPod  { get; set; }
}

