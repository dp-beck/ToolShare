using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ToolShare.Data.Models;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set;}
    public string? AboutMe { get; set; }
    public string? ProfilePhotoUrl { get; set; }
    public DateTime CreatedAt { get; } = DateTime.Now;
    public ICollection<Tool> ToolsOwned { get; set; } = new List<Tool>();
    public ICollection<Tool> ToolsBorrowed { get; set; } = new List<Tool>();
    public int? PodJoinedId { get; set; }
    public Pod? PodJoined { get; set; }
    public int? PodManagedId { get; set; }
    public Pod? PodManaged { get; set; }
}
