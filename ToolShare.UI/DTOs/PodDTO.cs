using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShare.Data.Models;

namespace ToolShare.UI.DTOs
{
    public class PodDTO
    {
        public int PodId { get; set; }
        public string Name { get; set; }  
        public AppUser? podManager { get; set; }
        public ICollection<AppUser>? PodMembers { get; set; } = new List<AppUser>();
    }
}