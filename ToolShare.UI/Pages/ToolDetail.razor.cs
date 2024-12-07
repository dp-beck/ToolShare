using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using ToolShare.UI.DTOs;
using ToolShare.UI.Services;

namespace ToolShare.UI.Pages;

public partial class ToolDetail : ComponentBase
{
    private MudForm? form;
    private bool validForm = true;
    private string[] errors = [];
    private bool _isLoading {get;set;} = true;
    public ToolDTO Tool { get; set; }
    public UpdateToolDTO UpdateToolDTO { get; set; }
    private EditContext? editContext;
    [Parameter]
    public int ToolId { get; set; }
    
    [Inject]
    public required IToolsDataService ToolsDataService { get; set; }

    [Inject]
    public required ISnackbar Snackbar { get; set; }
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

    private async Task HandleEditSubmit()
    {
        var result = await ToolsDataService.UpdateTool(ToolId, UpdateToolDTO);
        if (result.Succeeded)
        {
            Snackbar.Add("Tool successfully updated!", Severity.Success);    
        }
        else
        {
            Snackbar.Add($"Error: {result.ErrorList}", Severity.Error);
        }
    }
}