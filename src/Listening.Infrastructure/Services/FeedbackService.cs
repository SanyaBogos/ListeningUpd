using AutoMapper;
using Listening.Core.Entities.Specialized.Feedback;
using Listening.Core.ViewModels.Feedback;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Infrastructure.Services.Contracts;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IMapper _mapper;

        public FeedbackService(
            IFeedbackRepository feedbackRepository,
            IMapper mapper
            )
        {
            _feedbackRepository = feedbackRepository;
            _mapper = mapper;
        }

        public async Task<PagedData<FeedbackDto>> GetPagedAsync(FeedbackQueryViewModel query, long userId, bool isSuper)
        {
            var result = await _feedbackRepository.GetPaged(query, userId, isSuper);
            return result;
        }

        public async Task<long> Insert(FeedbackInsertDto feedbackDtos, long userId)
        {
            var feedback = _mapper.Map<Feedback>(feedbackDtos);
            feedback.CreatedTime = DateTime.Now;
            feedback.UserId = userId;
            var id = await _feedbackRepository.InsertOne(feedback);

            return id;
        }
    }
}
