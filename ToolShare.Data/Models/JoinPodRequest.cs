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
        public string RequesterId { get; set; }
        public AppUser Requester { get; set; }
        public string ReceiverId { get; set; }
        public AppUser Receiver { get; set; }
        public int podRequestedId { get; set; }
        public Pod podRequested { get; set; }


    }
}