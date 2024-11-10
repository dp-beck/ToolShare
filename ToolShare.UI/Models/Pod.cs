using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShare.Data.Models;

namespace ToolShare.UI.Models
{
    public class Pod
    {
        public string? Name { get; set; }  
        public AppUser podManager { get; set; }
        public ICollection<AppUser> PodMembers { get; set; } = new List<AppUser>();
    }
}