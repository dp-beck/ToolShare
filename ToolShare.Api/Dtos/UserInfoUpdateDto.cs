using System.ComponentModel.DataAnnotations;

namespace ToolShare.Api.Dtos;

public class UserInfoUpdateDto
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
    
    [StringLength(500)]
    public string? AboutMe { get; set; }
}