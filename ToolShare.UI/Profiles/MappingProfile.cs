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
            this.CreateMap<AppUser, AppUserDTO>()
                .ForMember(
                    dest => dest.PodJoinedName,
                    opt => opt.MapFrom(src => src.PodJoined.Name))
                .ForMember(
                    dest => dest.PodManagedName,
                    opt => opt.MapFrom(src => src.PodManaged.Name))
                .ReverseMap();
                
            this.CreateMap<Pod, PodDTO>().ReverseMap();
            this.CreateMap<Tool, ToolDTO>().ReverseMap();
            this.CreateMap<Tool, UpdateToolDTO>().ReverseMap();
            this.CreateMap<Pod, LimitedPodInfoDTO>()
                .ForMember(
                    dest => dest.PodManagerName, 
                    opt => opt.MapFrom(src => src.podManager.UserName))
                .ForMember(
                    dest => dest.PodManagerEmail, 
                    opt => opt.MapFrom(src => src.podManager.Email))
                .ReverseMap();

        } 
    }
}