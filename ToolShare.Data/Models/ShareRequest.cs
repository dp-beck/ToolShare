using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShare.Data.Models
{
    public class ShareRequest
    {
        [Key]
        public int ShareRequestId { get; set; }
        [Required]
        public Tool ToolRequested { get; set; }

        public int ToolOwnerId { get; set; }
        [Required]
        public AppUser ToolOwner { get; set; }

        public int ToolRequestorId { get; set; }
        [Required]
        public AppUser ToolRequestor { get; set; }

        public DateTime DateRequested { get; set; } = DateTime.Today;
        
        public bool? IsShareRequested { get; set; } = null;
    }
}