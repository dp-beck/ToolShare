using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShare.Data.Models
{
    public class Pod
    {
        [Key]
        public int PodId { get; set; }
        public string? Name { get; set; }  
        public ICollection<AppUser> PodMembers { get; set; } = new List<AppUser>();
    }
}