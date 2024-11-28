namespace ToolShare.UI.DTOs
{
    public class PodDTO
    {
        public int PodId { get; set; }
        public string Name { get; set; }  
        public AppUserDTO? podManager { get; set; }
        public ICollection<AppUserDTO>? PodMembers { get; set; } = new List<AppUserDTO>();
    }
}