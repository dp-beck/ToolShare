using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using ToolShare.UI.DTOs;

namespace ToolShare.UI.Components;

public partial class HomeUser : ComponentBase
{
    [CascadingParameter]
    protected AppUserDTO userInfo { get; set; }
    
    private string nameFilter = string.Empty;
    private PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
    public IQueryable<ToolDTO> filteredTools { get; set; }

}