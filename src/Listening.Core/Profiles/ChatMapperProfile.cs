using AutoMapper;
using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels.Chat;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.Profiles
{
    public class ChatMapperProfile : Profile
    {
        public ChatMapperProfile()
        {
            CreateMap<ChatAvailableUserDto, ApplicationUser>().ReverseMap();
        }
    }
}
