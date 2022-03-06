using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration
{
    public class BaseIntegrationTest<T> : BaseTest<T> where T : class
    {
        internal protected DatabaseFixture _fixture;
    }
}
