using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShare.Data.Models
{
    public class Share
    {
        [Key]
        public int ShareId { get; set; }
        
        public int ToolSharedId { get; set; }
        [Required]
        public Tool ToolShared { get; set; }

        public int ToolOwnerId { get; set; }
        [Required]
        public AppUser ToolOwner { get; set; }

        public int ToolBorrowerId { get; set; }
        [Required]
        public AppUser ToolBorrower { get; set; }

        public DateTime DateBorrowed { get; set; } = DateTime.Today;
        public DateTime DateReturned { get; set; }
    }
}