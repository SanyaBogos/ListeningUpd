using AutoMapper;
using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels.AccountViewModels;
using Listening.Core.ViewModels.Admin;

namespace Listening.Core.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<ApplicationUserDto, ApplicationUser>().ReverseMap();
        }
    }
}
