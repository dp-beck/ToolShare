using System.ComponentModel.DataAnnotations;

namespace ToolShare.Data.Models
{
    public class Tool
    {
        [Key]
        public int ToolId { get; set; }
        
        [StringLength(50)]
        public required string Name { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive integer.")]
        public int BorrowingPeriodInDays { get; set; }

        [StringLength(500)]
        public string? ToolPhotoUrl { get; set; }

        public ToolStatus ToolStatus { get; set; } = ToolStatus.Available;
        
        [StringLength(50)]
        public string? OwnerId { get; set; }

        public required AppUser ToolOwner { get; set; }

        [StringLength(50)]
        public string? BorrowerId { get; set; }
        
        public AppUser? ToolBorrower { get; set; }
        
        [StringLength(50)]
        public string? RequesterId { get; set; }
        public AppUser? ToolRequester { get; set; }
        
        public DateOnly? DateBorrowed { get; set; }

        public DateOnly? DateDue => DateBorrowed?.AddDays(BorrowingPeriodInDays);
    }
}