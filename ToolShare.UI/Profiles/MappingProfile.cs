using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ToolShare.Data.Models;
using ToolShare.UI.Dtos;
using ToolShare.UI.DTOs;

namespace ToolShare.UI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<AppUser, AppUserDTO>().ReverseMap();
            this.CreateMap<Pod, PodDTO>().ReverseMap();
            this.CreateMap<Tool, ToolDTO>().ReverseMap();
            this.CreateMap<Tool, UpdateToolDTO>().ReverseMap();
        } 
    }
}