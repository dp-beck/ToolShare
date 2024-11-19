using Microsoft.AspNetCore.Components;
using ToolShare.UI.DTOs;
using ToolShare.UI.Services;

namespace ToolShare.UI.Pages;

public partial class ToolDetail : ComponentBase
{
    private bool _isLoading {get;set;} = true;
    public ToolDTO Tool { get; set; }
    [Parameter]
    public int ToolId { get; set; }
    
    [Inject]
    public required IToolsDataService ToolsDataService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Tool = await ToolsDataService.FindToolById(ToolId);
        _isLoading = false;
    }
    
    protected override async Task OnParametersSetAsync()
    {
        _isLoading = true;
        Tool = await ToolsDataService.FindToolById(ToolId);
        _isLoading = false;
    }
}