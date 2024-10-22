using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ToolShare.Data.Models;

namespace ToolShare.Data
{
    public class Tool
    {
        [Key]
        public int ToolId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string? Description { get; set; }
        
        public int BorrowingPeriodInDays { get; set; }
        public ToolStatus ToolStatus { get; set; } = ToolStatus.AvailableForBorrowing;
        public int OwnerId { get; set; }

        [Required]
        public AppUser ToolOwner { get; set; }
        
        public int? BorrowerId { get; set; }
        
        public AppUser? ToolBorrower { get; set; }
        
        public DateTime CreatedAt { get; set; }

    }
}