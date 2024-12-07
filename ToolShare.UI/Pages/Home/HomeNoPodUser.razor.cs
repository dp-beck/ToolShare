using Microsoft.AspNetCore.Components;
using MudBlazor;
using ToolShare.UI.Dtos;
using ToolShare.UI.Services;

namespace ToolShare.UI.Pages.Home;

public partial class HomeNoPodUser : ComponentBase
{
    private MudForm form;
    string[] errors = { };
    public IEnumerable<LimitedPodInfoDto>? Pods { get; set; }
    string podName = string.Empty;
    private bool success;
    private bool validForm;

    [Inject]
    public required IPodsDataService PodsDataService { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        Pods = await PodsDataService.GetAllPodsLimitedInfoForNoPodUser();
    }
    
    private async Task HandleSubmit()
    {
        var result = await PodsDataService.InitializeNewPod(podName);
        if (result.Succeeded)
        {
            success = true;
        }
        else 
        {
            errors = result.ErrorList;
        }
    }
}