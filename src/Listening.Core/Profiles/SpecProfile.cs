using AutoMapper;
using Listening.Core.Entities.Specialized.Knowledge;
using Listening.Core.ViewModels.Spec;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.Profiles
{
    public class SpecProfile : Profile
    {
        public SpecProfile()
        {
            CreateMap<Course, CourseDto>()
                .ForMember(x => x.Type, opt => opt.MapFrom(s => s.Type.Name))
                .ForMember(x => x.Author, opt => opt.MapFrom(s => s.Author.Name))
                .ReverseMap();
            CreateMap<Course, CourseHeaderDto>()
                .ForMember(x => x.Author, opt => opt.MapFrom(s => s.Author.Name))
                .ReverseMap();
            CreateMap<Folder, FolderDto>().ReverseMap();
            CreateMap<Video, VideoDto>()
                .ForMember(x => x.Ext, opt => opt.MapFrom(s => s.VideoType.Name))
                .ReverseMap();
            CreateMap<Entities.Specialized.Knowledge.Type, TypeDto>().ReverseMap();
            CreateMap<Entities.Specialized.Knowledge.Type, TypeHeaderDto>().ReverseMap();
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Book, BookDto>()
                .ForMember(x => x.FType, opt => opt.MapFrom(s => s.FileType.Name))
                .ReverseMap();
            CreateMap<TimeStamp, TimeStampDto>().ReverseMap();
        }
    }
}
