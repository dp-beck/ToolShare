using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ToolShare.Api.Dtos;
using ToolShare.Data;
using ToolShare.Data.Models;

namespace ToolShare.Api.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            this.CreateMap<AppUser, AppUserDto>().ReverseMap();
            this.CreateMap<Pod, PodDto>().ReverseMap();
            this.CreateMap<Tool, ToolDto>().ReverseMap();
        }
    }
}