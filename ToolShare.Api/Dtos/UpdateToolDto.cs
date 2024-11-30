using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ToolShare.Data.Models;

namespace ToolShare.Api.Dtos
{
    public class UpdateToolDto
    {
        [StringLength(50)]
        public required string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
        public int BorrowingPeriodInDays { get; set; }
    }
}