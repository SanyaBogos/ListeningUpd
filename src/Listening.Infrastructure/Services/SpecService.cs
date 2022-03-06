using AutoMapper;
using Listening.Core.ViewModels.Spec;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Infrastructure.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Services
{
    public class SpecService : ISpecService
    {
        private readonly ISpecCourseEFRepository _specCourseEFRepository;
        private readonly IMapper _mapper;

        public SpecService(ISpecCourseEFRepository specCourseEFRepository,
            IMapper mapper
            )
        {
            _specCourseEFRepository = specCourseEFRepository;
            _mapper = mapper;
        }

        public async Task<TypeHeaderDto[]> GetHeaderDescription(long userId)
        {
            var header = await _specCourseEFRepository.GetHeaderDescription(userId);
            var headerDto = _mapper.Map<TypeHeaderDto[]>(header);
            return headerDto;
        }

        public async Task<CourseDto> GetCourse(int id, long userId)
        {
            var course = await _specCourseEFRepository.GetVideoDescriptions(id, userId);
            var courseDto = _mapper.Map<CourseDto>(course);
            return courseDto;
        }
    }
}
