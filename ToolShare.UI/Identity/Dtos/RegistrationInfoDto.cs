namespace ToolShare.UI.Identity.Dtos
{
    /// <summary>
    /// Registration info provided by a new user
    /// </summary>
    public class RegistrationInfoDto
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Password { get; set; }
        public string? AboutMe { get; set; }
        public string? ProfilePhotoUrl { get; set; }
    }
}