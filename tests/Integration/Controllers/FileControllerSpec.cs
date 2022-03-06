using Listening.Server.Controllers.api;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Integration;

namespace Integration.Controllers
{
    [Collection("Database collection")]
    public class FileControllerSpec : BaseIntegrationTest<FileController>
    {
        public FileControllerSpec(DatabaseFixture fixture)
        {
            _fixture = fixture;
            //_sut = new FileController();
        }
    }
}
