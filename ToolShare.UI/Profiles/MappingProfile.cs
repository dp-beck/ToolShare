using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ToolShare.Data.Models;
using ToolShare.UI.Dtos;

namespace ToolShare.UI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<AppUser, AppUserDto>()
                .ForMember(
                    dest => dest.PodJoinedName,
                    opt => opt.MapFrom(src => src.PodJoined.Name))
                .ForMember(
                    dest => dest.PodManagedName,
                    opt => opt.MapFrom(src => src.PodManaged.Name))
                .ReverseMap();
                
            this.CreateMap<Pod, PodDto>().ReverseMap();
            this.CreateMap<Tool, ToolDto>().ReverseMap();
            this.CreateMap<Tool, UpdateToolDto>().ReverseMap();
            this.CreateMap<Pod, LimitedPodInfoDto>()
                .ForMember(
                    dest => dest.PodManagerName, 
                    opt => opt.MapFrom(src => src.PodManager.UserName))
                .ForMember(
                    dest => dest.PodManagerEmail, 
                    opt => opt.MapFrom(src => src.PodManager.Email))
                .ReverseMap();

        } 
    }
}