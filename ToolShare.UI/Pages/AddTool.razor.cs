using Microsoft.AspNetCore.Components;
using ToolShare.UI.DTOs;
using ToolShare.UI.Services;

namespace ToolShare.UI.Pages;

public partial class AddTool : ComponentBase
{
    private ToolDTO ToolDto { get; set; } = new ToolDTO();
    private bool success;
    private bool error;
    private string Message = string.Empty;
    private string StatusClass = string.Empty;
    [Inject]
    public required IToolsDataService ToolsDataService { get; set; }
    
    private async Task HandleValidSubmit()
    {
       var addedTool = await ToolsDataService.CreateTool(ToolDto);
        if (addedTool != null)
        {
            success = true;
            error = false;
            StatusClass = "alert alert-success";
            Message = "You sucessfully created a new tool!"; 
        }
        else 
        {
            success = false;
            error = true;
            StatusClass = "alert-danger";
            Message = "Something went wrong! Please try again"; 
        }
    }

    private void HandleInvalidSubmit()
    {
        return;
    }
}