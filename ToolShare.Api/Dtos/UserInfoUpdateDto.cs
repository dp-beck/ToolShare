namespace ToolShare.Api.Dtos;

public class UserInfoUpdateDto
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string AboutMe { get; set; }
}