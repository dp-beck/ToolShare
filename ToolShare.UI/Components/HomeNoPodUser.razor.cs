using Microsoft.AspNetCore.Components;
using MudBlazor;
using ToolShare.UI.DTOs;
using ToolShare.UI.Services;

namespace ToolShare.UI.Components;

public partial class HomeNoPodUser : ComponentBase
{
    private MudForm form;
    string[] errors = { };
    public IEnumerable<LimitedPodInfoDTO>? Pods { get; set; }
    public PodDTO podDTO { get; set; } = new PodDTO();
    string podName = string.Empty;
    private bool success;
    private bool validForm;
    protected string Message = string.Empty;
    protected string StatusClass = string.Empty;
    
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