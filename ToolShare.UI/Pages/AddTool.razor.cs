using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using ToolShare.UI.Dtos;
using ToolShare.UI.Services;
namespace ToolShare.UI.Pages;

public partial class AddTool : ComponentBase
{
    private MudForm? form;
    private bool validForm;
    private string[] errors = [];
    private ToolDto ToolDto { get; set; } = new();
    private bool success;
    private string secureUrl = string.Empty;
    private string PhotoUploadMessage = string.Empty;
    [Inject]
    public required IToolsDataService ToolsDataService { get; set; }
    [Inject] public required IJSRuntime JS { get; set; }
    
    private async Task HandleSubmit()
    { 
        ToolDto.ToolPhotoUrl = TransformImageUrl(secureUrl, "w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai");
        var result = await ToolsDataService.CreateTool(ToolDto);
        if (result.Succeeded)
        {
           success = true;
        }
        else 
        {
           errors = result.ErrorList;
        }
    }

    
    private async Task OpenWidget()
    {
        secureUrl = await JS.InvokeAsync<string>("openWidget");
        
        if (!string.IsNullOrEmpty(ToolDto.ToolPhotoUrl))
        {
            PhotoUploadMessage = "Photo successfully uploaded!";
        }
    }

    private string TransformImageUrl(string url, string transformation)
    {
        string[] parts = url.Split(["/upload/"], StringSplitOptions.None);
        if (parts.Length != 2)
        {
            throw new ArgumentException("Invalid Cloudinary URL");
        }
        return parts[0] + "/upload/" + transformation + "/" + parts[1];
    }
    
}