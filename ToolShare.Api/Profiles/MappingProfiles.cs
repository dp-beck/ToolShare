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

            this.CreateMap<Pod, LimitedPodInfoDTO>()
                .ForMember(
                    dest => dest.PodManagerName, 
                    opt => opt.MapFrom(src => src.PodManager.UserName))
                .ForMember(
                    dest => dest.PodManagerEmail, 
                    opt => opt.MapFrom(src => src.PodManager.Email))
                .ReverseMap();

            this.CreateMap<Tool, ToolDto>()
                .ForMember(
                    dest => dest.ToolOwnerName,
                    opt => opt.MapFrom(src => src.ToolOwner.UserName))
                .ForMember(
                    dest => dest.ToolBorrowerName,
                    opt => opt.MapFrom(src => src.ToolBorrower.UserName))
                .ForMember(
                    dest => dest.ToolRequesterName,
                    opt => opt.MapFrom(src => src.ToolRequester.UserName))
                .ReverseMap();
            
            this.CreateMap<Tool, UpdateToolDto>().ReverseMap();
        }
    }
}