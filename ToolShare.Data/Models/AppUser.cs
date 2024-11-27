using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ToolShare.Data.Models;

public class AppUser : IdentityUser
{
    [StringLength(50)]
    public string? FirstName { get; set; }
    
    [StringLength(50)]
    public string? LastName { get; set;}

    [StringLength(500)]
    public string? AboutMe { get; set; }

    [StringLength(500)]
    public string? ProfilePhotoUrl { get; set; }
    
    public ICollection<Tool> ToolsOwned { get; set; } = new List<Tool>();
    
    public ICollection<Tool> ToolsBorrowed { get; set; } = new List<Tool>();
    
    public ICollection<Tool> ToolsRequested { get; set; } = new List<Tool>();
    
    public int? PodJoinedId { get; set; }
    
    public Pod? PodJoined { get; set; }
    
    public int? PodManagedId { get; set; }
    
    public Pod? PodManaged { get; set; }
}
