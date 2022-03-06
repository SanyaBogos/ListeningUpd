using Listening.Core.Entities.Custom;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace Infrastructure
{
    public class BaseTest<T> where T : class
    {
        internal protected T _sut;

        public BaseTest()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
        }
    }
}
