using FluentAssertions;
using Infrastructure;
using Infrastructure.Helpers;
using Listening.Core.Entities.Custom;
using Listening.Infrastructure.Exceptions;
using Listening.Infrastructure.Services.Contracts;
using Listening.Server.Controllers.api;
using Listening.Server.Services.Contracts;
using Listening.Core.ViewModels.Text;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Unit.Controllers
{
    public class TextControllerSpec : BaseUnitTest<TextController>
    {
        private readonly Mock<IFileService> _fileServiceMock;
        private readonly Mock<ITextService> _textServiceMock;
        private readonly Mock<IResultService> _resultServiceMock;
        private readonly Mock<IUserService> _userServiceMock;

        public TextControllerSpec()
        {
            _fileServiceMock = new Mock<IFileService>();
            _textServiceMock = new Mock<ITextService>();
            _resultServiceMock = new Mock<IResultService>();
            _userServiceMock = new Mock<IUserService>();
            _sut = new TextController(_userManagerMock.Object,
                _textServiceMock.Object, _resultServiceMock.Object, _userServiceMock.Object, _mapper,
                _fileServiceMock.Object);
            _sut.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        class TextNotFilledData : EnumerableDataAbstract
        {
            public TextNotFilledData()
            {
                var str = "someStr";
                _data = new List<object[]>
                {
                    new object[]{ new TextDto { Id = "1", Title = str, Text = str, Country = str },
                        "AUDIO_OR_VIDEO_IS_NECESSARY" },
                    new object[]{ new TextDto { Id = "1", Title = str, Text = str, AudioName = str, VideoName = str, Country = str },
                        "NOT_ALLOWED_TO_USE_BOTH_AUDIO_AN_VIDEO" },
                };
            }

            //private List<object[]> GenerateData(string correctValue, string incorrectValue, string[] fieldsToFill)
            //{
            //    var objectsList = new List<object[]>();
            //    //var list = new List<TextDto>();

            //    var commonFillingList = GenerateComonFillingList(fieldsToFill);
            //    foreach (var listOfArgs in commonFillingList)
            //    {
            //        var textDto = new TextDto();
            //        foreach (var arg in fieldsToFill)
            //            textDto.GetType().GetProperty(arg).SetValue(textDto,
            //                listOfArgs.Contains(arg) ? incorrectValue : correctValue);
            //        //list.Add(textDto);
            //        objectsList.Add(new object[] { textDto, "" });
            //    }

            //    return objectsList;
            //}

            //private List<IEnumerable<string>> GenerateComonFillingList(string[] fieldsToFill)
            //{
            //    var index = 1;
            //    var commonFillingList = fieldsToFill.GetKCombs(index++).ToList();
            //    while (index <= fieldsToFill.Length)
            //        commonFillingList.AddRange(fieldsToFill.GetKCombs(index++));
            //    return commonFillingList;
            //}
        }


        [Theory, ClassData(typeof(TextNotFilledData))]
        public void ShouldThrowExceptionIfNotAllNecessaryFieldsAreFilled_InPost(TextDto textDto, string expectedError)
        {
            Action act = () =>
            {
                _sut.PostText(textDto).GetAwaiter().GetResult();
            };

            act.Should().Throw<TextException>().WithMessage(expectedError);
        }

        [Theory, ClassData(typeof(TextNotFilledData))]
        public void ShouldThrowExceptionIfNotAllNecessaryFieldsAreFilled_InPut(TextDto textDto, string expectedError)
        {
            Action act = () =>
            {
                _sut.PutText(textDto).GetAwaiter().GetResult();
            };

            act.Should().Throw<TextException>().WithMessage(expectedError);
        }

        [Fact]
        public async Task ShouldCallInsert_InPost()
        {
            TextDto[] mockedTextDtos = null;
            var userId = RandomHelper.Quantity();
            var paramTextDto = GetTextDto();

            _userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .Returns(Task.FromResult(new ApplicationUser { Id = userId }));
            _textServiceMock.Setup(x => x.Insert(It.IsAny<TextDto[]>()))
                .Callback<TextDto[]>(x => mockedTextDtos = x)
                .Returns(Task.FromResult(new string[] { "newid" }));

            await _sut.PostText(paramTextDto);

            _textServiceMock.Verify(c => c.Insert(It.IsAny<TextDto[]>()), Times.Once);
            mockedTextDtos.Length.Should().Be(1);
            mockedTextDtos.First().Title.Should().Be(paramTextDto.Title);
            mockedTextDtos.First().Text.Should().Be(paramTextDto.Text);
            mockedTextDtos.First().Country.Should().Be(paramTextDto.Country);
            mockedTextDtos.First().AudioName.Should().Be(paramTextDto.AudioName);
            mockedTextDtos.First().Assignee.Should().Be(userId);
        }

        private TextDto GetTextDto()
        {
            var paramTextDto = new TextDto
            {
                Id = RandomHelper.String(),
                Title = RandomHelper.String(),
                Text = RandomHelper.String(),
                Country = RandomHelper.String(),
                AudioName = RandomHelper.String()
            };
            return paramTextDto;
        }
    }
}
