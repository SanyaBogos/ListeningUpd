using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Listening.Infrastructure.Utilities
{
    public static class HashExtensions
    {
        public static long Hash(this string value)
        {
            var shaHasher = SHA1.Create();
            var md5 = MD5.Create();
            var hashed = shaHasher.ComputeHash(md5.ComputeHash(Encoding.UTF8.GetBytes(value)));
            var ivalue = BitConverter.ToInt64(hashed, 0);
            return ivalue;
        }
    }

}
