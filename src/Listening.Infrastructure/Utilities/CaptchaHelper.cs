using Listening.Infrastructure.Extensions;
using Listening.Server;
using Listening.Server.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Infrastructure.Utilities
{
    public class CaptchaHelper
    {
        public static string GenerateHash(string validate, int captchaTTL)
        {
            var expirationTime = DateTime.Now.AddMilliseconds(captchaTTL);
            var combinedHashWithExpTime = $"{validate.Hash()}{GlobalConstats.CAPTCHA_SPLITTER}{expirationTime}";
            var encryptedBytes = combinedHashWithExpTime.EncryptStringToBytes();
            var encryptedString = BitConverter.ToString(encryptedBytes);

            return encryptedString;
        }
    }
}
