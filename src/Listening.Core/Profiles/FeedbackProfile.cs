using AutoMapper;
using Listening.Core.Entities.Specialized.Feedback;
using Listening.Core.ViewModels.Feedback;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.Profiles
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            CreateMap<Feedback, FeedbackDto>().ReverseMap();
            CreateMap<Feedback, FeedbackInsertDto>().ReverseMap();
        }
    }
}
