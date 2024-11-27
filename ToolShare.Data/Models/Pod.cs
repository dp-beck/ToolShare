using System.ComponentModel.DataAnnotations;

namespace ToolShare.Data.Models
{
    public class Pod
    {
        [Key]
        public int PodId { get; set; }
        
        [StringLength(50)]
        public required string Name { get; set; }  
        
        public required AppUser PodManager { get; set; }
        
        public ICollection<AppUser> PodMembers { get; set; } = new List<AppUser>();
    }
}