using FluentAssertions;
using Listening.Server.Services;
using Integration;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Listening.Server.Services.Contracts;
using System.Linq;

namespace Integration.Services
{
    [Collection("Database collection")]
    public class OpticalCharacterRecognitionServiceSpec : BaseIntegrationTest<OpticalCharacterRecognitionService>
    {
        public OpticalCharacterRecognitionServiceSpec(DatabaseFixture fixture)
        {
            _fixture = fixture;
            var hostingEnv = (IWebHostEnvironment)_fixture.Server.Host.Services
                .GetService(typeof(IWebHostEnvironment));
            var fileService = (IFileService)_fixture.Server.Host.Services
                .GetService(typeof(IFileService));
            _sut = new OpticalCharacterRecognitionService(_fixture.Configuration, fileService, hostingEnv);
        }

        [Fact]
        public void ShouldConvertImageToTextCorrectly()
        {
            var expectedResult =
@"The (quick) [brown] {fox} jumps!
Over the $43,456.78 <lazy> #90 dog
& duck/goose, as 12.5% of E-mail
from aspammer@website.com is spam.
Der ,,schnelle” braune Fuchs springt
ﬁber den faulen Hund. Le renard brun
«rapide» saute par-dessus le chien
paresseux. La volpe marrone rapida
salta sopra i] cane pigro. El zorro
marrén répido salta sobre el perro
perezoso. A raposa marrom répida
salta sobre 0 C50 preguieoso.";

            var type = _fixture.OcrImageName.Split(".").Last();
            var bytes = File.ReadAllBytes($@"{_fixture.OcrImagePath}{_fixture.OcrImageName}");
            var base64 = Convert.ToBase64String(bytes);

            var result = _sut.GetRecognitionResult($@"data:image/{type};base64,{base64}" , "eng+deu");
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
