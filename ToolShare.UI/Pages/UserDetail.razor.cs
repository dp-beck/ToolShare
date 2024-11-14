using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ToolShare.Data;
using ToolShare.Data.Models;
using ToolShare.UI.DTOs;

namespace ToolShare.UI.Pages
{
    public partial class UserDetail
    {
        
        [CascadingParameter]
        protected AppUserDTO userInfo { get; set; }
        
    }
}