using Listening.Core.ViewModels.Spec;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Services.Contracts
{
    public interface ISpecService
    {
        Task<TypeHeaderDto[]> GetHeaderDescription(long userId);
        Task<CourseDto> GetCourse(int id, long userId);
    }
}
