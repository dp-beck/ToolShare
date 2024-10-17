using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShare.Data
{
    public class Tool
    {
        public int ToolId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int BorrowingPeriodInDays { get; set; }
        public int IsOwnedById { get; set; }
        public int IsPossessedById { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}