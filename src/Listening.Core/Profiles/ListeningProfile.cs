using AutoMapper;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Server.Entities.Specialized.Text;
using Listening.Core.ViewModels.Text;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.Profiles
{
    public class ListeningProfile : Profile
    {
        public ListeningProfile()
        {
            CreateMap<TextDto, TextEnhanced>();
            //x.CreateMap<TextEnhanced, Text>();
            CreateMap<TextEnhanced, Text>()
                //.ForMember(d => d.TextId, opt => opt.MapFrom(s => ObjectId.Parse(s.TextId)))
                .ForMember(d => d.Id, opt => opt.MapFrom(s => ObjectId.Parse(s.Id)))
                .ReverseMap();
            CreateMap<Text, TextDescriptionDto>()
                .ForMember(y => y.TextId, opt => opt.MapFrom(s => s.Id));
            CreateMap<Text, TextDescriptionEnhancedDto>()
                .ForMember(y => y.TextId, opt => opt.MapFrom(s => s.Id));
            CreateMap<TextDto, Text>()
                //.ForMember(d => d.TextId, opt => opt.MapFrom(s => ObjectId.Parse(s.TextId)))
                .ForMember(d => d.Id, opt => opt.MapFrom(s => ObjectId.Parse(s.Id)))
                .ReverseMap();
        }
    }
}
