using System.ComponentModel.DataAnnotations;
using ToolShare.Data.Models;

namespace ToolShare.Api.Dtos
{
    public class ToolDto
    {
        public int ToolId { get; set; }

        [StringLength(50)]
        public required string Name { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive integer.")]
        public int BorrowingPeriodInDays { get; set; }
        public string? ToolPhotoUrl { get; set; }

        public required ToolStatus ToolStatus { get; set; }
        public string? ToolOwnerName { get; set; }
        public string? ToolBorrowerName { get; set; }
        public string? ToolRequesterName { get; set; }
        public DateOnly? DateBorrowed { get; set; }
        public DateOnly? DateDue => DateBorrowed?.AddDays(BorrowingPeriodInDays);

    }
}