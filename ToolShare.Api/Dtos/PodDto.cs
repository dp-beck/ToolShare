using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShare.Api.Dtos
{
    public class PodDto
    {
        public int PodId { get; set; }
        public string Name { get; set; }
        public AppUserDto? PodManager { get; set; }
        public List<AppUserDto>? PodMembers { get; set; }
    }
}