using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ToolShare.UI.Dtos;
using ToolShare.UI.DTOs;
using ToolShare.UI.Services;

namespace ToolShare.UI.Pages;

public partial class ToolDetail : ComponentBase
{
    private bool _isLoading {get;set;} = true;
    public ToolDTO Tool { get; set; }
    public UpdateToolDTO UpdateToolDTO { get; set; }
    private EditContext? editContext;
    private String Message {get;set;} = String.Empty;
    [Parameter]
    public int ToolId { get; set; }
    
    [Inject]
    public required IToolsDataService ToolsDataService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Tool = await ToolsDataService.FindToolById(ToolId);
        
        UpdateToolDTO = new UpdateToolDTO()
        {
            Name = Tool.Name,
            Description = Tool.Description,
            BorrowingPeriodInDays = Tool.BorrowingPeriodInDays
        };
        
        _isLoading = false;
    }
    
    protected override async Task OnParametersSetAsync()
    {
        _isLoading = true;
        Tool = await ToolsDataService.FindToolById(ToolId);
        _isLoading = false;
    }

    private async Task<String> HandleEditSubmit()
    {
        Message = await ToolsDataService.UpdateTool(ToolId, UpdateToolDTO);
        return Message;
    }
}