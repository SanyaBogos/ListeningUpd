using Listening.Core.Entities.Custom;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Server.Entities.Specialized.Text;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Server.Services;
using Listening.Server.Utilities;
using Listening.Core.ViewModels.Text;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Listening.Web.Tests.Specialized.UnitTests.Helpers;
using AutoMapper;
using Listening.Core.Profiles;
using Listening.Web;

namespace Unit.Services
{
    public class TextServiceSpec : BaseUnitTest<TextService>
    {
        private readonly Mock<ITextsMongoRepository> _textRepositoryMock;
        private readonly Mock<IResultEFRepository> _resultEFRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IGlobalCache<TextEnhanced, string>> _textCacheMock;
        //private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;

        public TextServiceSpec()
        {
            _textRepositoryMock = new Mock<ITextsMongoRepository>();
            _resultEFRepositoryMock = new Mock<IResultEFRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _textCacheMock = new Mock<IGlobalCache<TextEnhanced, string>>();
            _sut = new TextService(_userManagerMock.Object, _textRepositoryMock.Object,
                _resultEFRepositoryMock.Object, _userRepositoryMock.Object, _mapper, _textCacheMock.Object);

            //Mapper.Initialize(cfg =>
            //{
            //    cfg.AddProfile<ListeningMapperProfile>();
            //});
        }

        [Fact]
        public async Task ShouldStoreTextInCacheAfterInsertWithNecessaryValues()
        {
            var textDto = new TextDto()
            {
                Country = "US",
                Title = "New one",
                Text = "One two three... Four, five?!...",
                AudioName = "new audio.mp3"
            };
            var textDtos = new TextDto[] { textDto };

            _textRepositoryMock.Setup(x => x.Insert(It.IsAny<IEnumerable<Text>>()))
                .Returns(Task.CompletedTask);
            _textCacheMock.Setup(x => x.Insert(It.IsAny<TextEnhanced>()));

            await _sut.Insert(textDtos);

            _textCacheMock.Verify(x => x.Insert(It.Is<TextEnhanced>(
                y => y.Text.Equals(textDto.Text) && y.WordsInParagraphs != null
                    && y.WordsInParagraphs.Length > 0
                    && y.CountsInParagraphs != null && y.CountsInParagraphs.Length > 0
                    && y.ParagrphsSymbolsCounts != null && y.ParagrphsSymbolsCounts.Length > 0
                )));
        }
    }
}
