using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShare.Data.Models
{
    public class Tool
    {
        [Key]
        public int ToolId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string? Description { get; set; }
        
        public int BorrowingPeriodInDays { get; set; }

        public string? ToolPhotoUrl { get; set; }

        public ToolStatus ToolStatus { get; set; } = ToolStatus.Available;
        public string OwnerId { get; set; }

        [Required]
        public AppUser ToolOwner { get; set; }
        
        public string? BorrowerId { get; set; }
        
        public AppUser? ToolBorrower { get; set; }
        public string? RequesterId { get; set; }
        public AppUser? ToolRequester { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateOnly DateDue { get; set; }

    }
}