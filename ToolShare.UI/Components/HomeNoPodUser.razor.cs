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
    private bool success;
    private bool error; 
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
        var addedPod = await PodsDataService.InitializeNewPod(podDTO);
        if (addedPod != null)
        {
            success = true;
            error = false;
            StatusClass = "alert alert-success";
            Message = "You sucessfully created a new pod. Please log out and log in to begin managing your new pod!"; 
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