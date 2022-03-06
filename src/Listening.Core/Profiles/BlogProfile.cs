using AutoMapper;
using Listening.Core.Entities.Specialized.MiniBlog;
using Listening.Core.ViewModels.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Listening.Core.Profiles
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Post, PostDto>()
                .ForMember(x => x.TopicIds, opt => opt.MapFrom(s => s.PostTopics.Select(y => y.TopicId).ToArray()))
                .ReverseMap();
            CreateMap<Post, PostDescriptionDto>()
                .ForMember(x => x.TopicIds, opt => opt.MapFrom(s => s.PostTopics.Select(y => y.TopicId).ToArray()))
                .ReverseMap();
            CreateMap<Post, SinglePostDto>()
                .ForMember(x => x.Topics, opt => opt.MapFrom(s => s.PostTopics.Select(y => y.Topic.Name).ToArray()))
                .ReverseMap();
            CreateMap<Post, PostWriteDto>().ReverseMap();
            CreateMap<Attachment, AttachmentDto>().ReverseMap();
            CreateMap<Topic, TopicDto>().ReverseMap();
            CreateMap<Priority, PriorityDto>().ReverseMap();
        }
    }
}
