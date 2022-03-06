using Listening.Web.Controllers.api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Integration.Controllers
{
    [Collection("Database collection")]
    public class StegControllerSpec : BaseIntegrationTest<StegController>
    {
        public StegControllerSpec(DatabaseFixture fixture)
        {
            _fixture = fixture;

        }

        [Fact]
        public async Task ShouldInjectAndEjectStegMessage()
        {
            //TODO: implement
            //await _fixture.AnonymousClient.PostAsync("api/", );
        }
    }
}
