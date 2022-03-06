using AutoMapper;
using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels.AccountViewModels;

namespace Listening.Core.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}
