using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShare.Data.Models
{
    public class JoinPodRequest
    {
        [Key]
        public int JoinPodRequestId { get; set; }
        public int RequestorId { get; set; }
        [Required]
        public AppUser Requestor { get; set; }
        public int PodId { get; set; }
        public Pod RequestedPod { get; set; }

        public int PodManagerId { get; set; }
        [Required]
        public AppUser PodManager { get; set; }


    }
}