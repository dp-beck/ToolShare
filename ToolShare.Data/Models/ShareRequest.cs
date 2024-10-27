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

        public string ToolOwnerId { get; set; }
        [Required]
        public AppUser ToolOwner { get; set; }

        public string ToolRequesterId { get; set; }
        [Required]
        public AppUser ToolRequester { get; set; }

        public DateTime DateRequested { get; set; } = DateTime.Today;
        
        public bool? IsShareRequested { get; set; } = null;
    }
}