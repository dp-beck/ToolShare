using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolShare.Data;

//TO DO: Rewrite to use Repository Architecture -- Written this way just to test things 
namespace ToolShare.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToolController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ToolController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(_context.Tools.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTool([FromBody] Tool tool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tools.Add(tool);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateTool), new { toolId = tool.ToolId }, tool);
        }

    }
}