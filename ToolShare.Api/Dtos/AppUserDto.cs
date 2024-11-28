using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShare.Data;
using ToolShare.Data.Models;

namespace ToolShare.Api.Dtos
{
    public class AppUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AboutMe { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string PodJoinedName { get; set; }
        public int PodJoinedId { get; set; }
        public PodDto PodJoined { get; set; }
        public int PodManagedId { get; set; }
        public PodDto PodManaged { get; set; }  
        public ICollection<ToolDto> ToolsOwned { get; set; }
        public ICollection<ToolDto> ToolsBorrowed { get; set; }
    }
}