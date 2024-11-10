using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ToolShare.UI.Models;

namespace ToolShare.UI.Services
{
    public class PodsService(HttpClient http) : IPodsService
    {
        public async Task<List<Pod>> GetPods()
        {
            var requestUri = "api/pods";
            return await http.GetFromJsonAsync<List<Pod>>(requestUri) ?? [];
        }
    }
}