using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ToolShare.Api.Dtos;
using ToolShare.Data.Models;

namespace ToolShare.Api.Profiles
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            this.CreateMap<AppUser, AppUserDto>();
        }
    }
}