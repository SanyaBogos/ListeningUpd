using AutoMapper;
using Infrastructure;
using Listening.Core.Entities.Custom;
using Listening.Core.Profiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Unit
{
    public class BaseUnitTest<T> : BaseTest<T> where T : class
    {
        //protected UserManager<ApplicationUser> _userManager;
        protected Mock<UserManager<ApplicationUser>> _userManagerMock;
        protected Mock<IUserStore<ApplicationUser>> _userStoreMock;

        protected readonly long _userId;
        protected readonly ApplicationUser _currentUser;
        protected readonly ControllerContext _context;
        protected readonly IMapper _mapper;

        public BaseUnitTest()
        {
            _userId = 123;
            _currentUser = new ApplicationUser { Id = _userId };

            _userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(_userStoreMock.Object, null, null, null,
                                                                        null, null, null, null, null);
            //_userManager = new UserManager<ApplicationUser>(_userStoreMock.Object,
            //    null, null, null, null,
            //    null, null, null, null);

            _userStoreMock.Setup(x => x.FindByIdAsync(
                It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(_currentUser));

            //_userStoreMock.Setup(x => x.GetUserAsync(
            //    It.IsAny<string>(), It.IsAny<CancellationToken>()))
            //    .Returns(Task.FromResult(_currentUser));

            var claimsIdentities = new ClaimsIdentity[] {
                new ClaimsIdentity(
                    new Claim[] {
                        new Claim (ClaimTypes.NameIdentifier, _userId.ToString())
                    })
            };

            _context = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(claimsIdentities)
                }
            };

            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                // cfg.AddProfile<AutoMapperProfile>();
                cfg.AddProfile<AccountProfile>();
                cfg.AddProfile<ListeningProfile>();
                cfg.AddProfile<ResultProfile>();
                cfg.AddProfile<ChatMapperProfile>();
                cfg.AddProfile<FeedbackProfile>();
                cfg.AddProfile<BlogProfile>();                
            }));
        }
    }
}
