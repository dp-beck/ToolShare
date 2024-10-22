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
        public string RequestorId { get; set; }
        [Required]
        public AppUser Requestor { get; set; }
        public int PodId { get; set; }
        public Pod RequestedPod { get; set; }

        public string PodManagerId { get; set; }
        [Required]
        public AppUser PodManager { get; set; }


    }
}