using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShare.Data.Models;

namespace ToolShare.UI.Dtos
{
    public class ToolDto
    {
        public int ToolId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int BorrowingPeriodInDays { get; set; }
        public string? ToolPhotoUrl { get; set; }

        public ToolStatus ToolStatus { get; set; }
        public string? ToolOwnerName { get; set; }
        public string? ToolBorrowerName { get; set; }
        public string? ToolRequesterName { get; set; }
        public DateOnly? DateBorrowed { get; set; }
        public DateOnly? DateDue => DateBorrowed?.AddDays(BorrowingPeriodInDays);
    }
}