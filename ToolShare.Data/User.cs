using Microsoft.AspNetCore.Identity;

namespace ToolShare.Data;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set;}
    public string? AboutMe { get; set; }
    public string? ProfilePhotoUrl { get; set; }
    public int PodId { get; set; }
    public DateTime CreatedAt { get; } = DateTime.Now;

}
