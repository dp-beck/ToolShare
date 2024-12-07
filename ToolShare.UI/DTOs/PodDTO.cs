namespace ToolShare.UI.Dtos
{
    public class PodDto
    {
        public int PodId { get; set; }
        public string Name { get; set; }  
        public AppUserDto? podManager { get; set; }
        public ICollection<AppUserDto>? PodMembers { get; set; } = new List<AppUserDto>();
    }
}