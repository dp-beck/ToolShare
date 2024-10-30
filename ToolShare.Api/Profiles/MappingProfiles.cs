using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ToolShare.Api.Dtos;
using ToolShare.Data.Models;

namespace ToolShare.Api.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            this.CreateMap<AppUser, AppUserDto>();
            this.CreateMap<Pod, PodDto>();
        }
    }
}