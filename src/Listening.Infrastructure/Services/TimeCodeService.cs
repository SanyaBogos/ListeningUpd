using AutoMapper;
using Listening.Core.Entities.Specialized.Knowledge;
using Listening.Core.ViewModels.Spec;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Infrastructure.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Services
{
    public class TimeCodeService : ITimeCodeService
    {
        private readonly ITimeStampRepository _timeStampRepository;
        private readonly IMapper _mapper;

        public TimeCodeService(ITimeStampRepository timeStampRepository,
            IMapper mapper
            )
        {
            _timeStampRepository = timeStampRepository;
            _mapper = mapper;
        }

        public async Task<TimeStampUserDto[]> GetByVideoId(int videoId)
        {
            //var timeStamp = _mapper.Map<TimeStamp>(timeStampDto);
            var result = await _timeStampRepository.GetAsync(videoId);
            return result;
        }

        public async Task AddTimeStamp(TimeStampDto timeStampDto)
        {
            var timeStamp = _mapper.Map<TimeStamp>(timeStampDto);
            await _timeStampRepository.AddNowAsync(timeStamp);
        }

        public async Task UpdateTimeStamp(TimeStampDto timeStampDto)
        {
            var timeStamp = _mapper.Map<TimeStamp>(timeStampDto);
            await _timeStampRepository.UpdateNowAsync(timeStamp);
        }

        public async Task DeleteTimeStampsOfVideo(int videoId)
        {
            await _timeStampRepository.DeleteForVideoNowAsync(videoId);
        }

        public async Task DeleteTimeStamp(TimeStampDto timeStampDto)
        {
            var timeStamp = _mapper.Map<TimeStamp>(timeStampDto);
            await _timeStampRepository.DeleteNowAsync(timeStamp);
        }
    }
}
