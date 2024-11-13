using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShare.Data.Models;

namespace ToolShare.UI.Dtos
{
    public class UpdateToolDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int BorrowingPeriodInDays { get; set; }
    }
}