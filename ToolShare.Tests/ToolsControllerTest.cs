namespace ToolShare.Tests;

// public async Task<IActionResult> GetAllTools()
// {
//     try
//     {
//         var tools = await _toolsRepository.GetAllWithIncludes(
//             t => t.ToolOwner, t => t.ToolBorrower!
//         );
//
//         List<ToolDto> toolDtos = _mapper.Map<List<ToolDto>>(tools);
//
//         return Ok(toolDtos);
//     }
//     catch (Exception)
//     {
//         return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
//     }
// }

[TestClass]
public class ToolsControllerTest
{
    [TestMethod]
    public void GetAllTools_ReturnsAllTools()
    {
        // Arrange
        // Act
        // Assert
    }
}