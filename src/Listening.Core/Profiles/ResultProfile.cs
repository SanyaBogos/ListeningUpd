using AutoMapper;
using Listening.Core.Entities.Specialized.Feedback;
using Listening.Core.ViewModels.Feedback;
using Listening.Core.ViewModels.ListeningResult;
using Listening.Server.Entities.Specialized.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.Profiles
{
    public class ResultProfile : Profile
    {
        public ResultProfile()
        {
            CreateMap<Result, ResultDto>().ReverseMap();
            CreateMap<Result, ResultIdDto>().ReverseMap();
            CreateMap<ResultIdDto, ResultUpdateTimeDto>().ReverseMap();
        }
    }
}
