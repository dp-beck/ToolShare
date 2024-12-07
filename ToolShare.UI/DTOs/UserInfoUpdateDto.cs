using System.ComponentModel.DataAnnotations;

namespace ToolShare.UI.Dtos;

public class UserInfoUpdateDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string AboutMe { get; set; }
}