using FluentAssertions;
using Listening.Core.ViewModels.Steg;
using Listening.Infrastructure.Exceptions;
using Listening.Infrastructure.Services;
using Listening.Server;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Xunit;

namespace Unit.Services
{
    public class FunctionServiceSpec : BaseUnitTest<FunctionService>
    {
        public FunctionServiceSpec()
        {
            _sut = new FunctionService();
        }

        [Fact]
        public void ShouldGetPointsFromFunction1()
        {
            var func = new FuncEnhancedDto
            {
                StartIndex = 0,
                Step = 1,
                Description = "Pow(x, 2)"
            };
            var size = new Size(240, 320);
            var length = 9;
            var pffParams = new PointsFromFuncParams
            {
                FuncDto = func,
                PictureSize = size,
                Length = length
            };

            var actual = _sut.GetPointsFromFunction(pffParams).Array;
            var expected = new int[length, 2];

            expected[0, 0] = 0; // x
            expected[0, 1] = 0; // y

            expected[1, 0] = 1; // x
            expected[1, 1] = 1; // y

            expected[2, 0] = 2; // x
            expected[2, 1] = 4; // y

            expected[3, 0] = 3; // x
            expected[3, 1] = 9; // y

            expected[4, 0] = 4; // x
            expected[4, 1] = 16; // y

            expected[5, 0] = 5; // x
            expected[5, 1] = 25; // y

            expected[6, 0] = 6; // x
            expected[6, 1] = 36; // y

            expected[7, 0] = 7; // x
            expected[7, 1] = 49; // y

            expected[8, 0] = 8; // x
            expected[8, 1] = 64; // y

            //expected[9, 0] = 9; // x
            //expected[9, 1] = 0; // y


            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ShouldGetPointsFromFunction2()
        {
            var func = new FuncEnhancedDto
            {
                StartIndex = 0,
                Step = 1,
                Description = "3 * sin(x*PI/6) + 5"
            };
            var size = new Size(240, 320);
            var length = 3;
            var pffParams = new PointsFromFuncParams
            {
                FuncDto = func,
                PictureSize = size,
                Length = length
            };

            var actual = _sut.GetPointsFromFunction(pffParams).Array;
            var expected = new int[length, 2];

            expected[0, 0] = 0; // x
            expected[0, 1] = 5; // y

            expected[1, 0] = 1; // x
            expected[1, 1] = 6; // y

            expected[2, 0] = 2; // x
            expected[2, 1] = 8; // y

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ShouldGetPointsFromFunction3()
        {
            var func = new FuncEnhancedDto
            {
                StartIndex = 0,
                Step = 1,
                Description = "-Pow(x, 2)+1"
            };
            var size = new Size(240, 320);
            var length = 9;
            var pffParams = new PointsFromFuncParams
            {
                FuncDto = func,
                PictureSize = size,
                Length = length
            };

            Action act = () => { _sut.GetPointsFromFunction(pffParams); };

            act.Should().Throw<StegException>(GlobalConstats.STEG_TOO_HUGE_MESSAGE);
        }
    }
}
