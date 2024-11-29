using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShare.Api.Dtos
{
    public class LimitedPodInfoDto
    {
        public string Name { get; set; }
        public string PodManagerName { get; set; }
        public string PodManagerEmail { get; set; }
    }
}