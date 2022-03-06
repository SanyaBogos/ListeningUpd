using Listening.Core.ViewModels;
using Listening.Core.ViewModels.ConstraintsInterface.AccountViewModels;
using Listening.Infrastructure.Exceptions;
using Listening.Infrastructure.Extensions;
using Listening.Infrastructure.Utilities;
using Listening.Server;
using Listening.Server.Security;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Web.Filters
{
    public class CaptchaFilter : ActionFilterAttribute
    {
        private const int MAX_CAPTCHA_VALUE_LENGTH = 20;

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            CaptchaCheckDto captcha;
            var receivedTime = DateTime.Now.AddSeconds(3);

            if (context.ActionArguments.ContainsKey("captcha"))
            {
                var captchaObject = context.ActionArguments.First(x => x.Key == "captcha");

                if (captchaObject.Value is ICaptchaViewModel)
                    captcha = (CaptchaCheckDto)captchaObject.Value;
                else if (captchaObject.Value is string)
                {
                    var hashObj = context.ActionArguments.First(x => x.Key == "hash");
                    var hashStr = hashObj.Value as string;

                    var captchaStr = captchaObject.Value as string;
                    captcha = new CaptchaCheckDto
                    {
                        Captcha = captchaStr,
                        Hash = hashStr
                    };
                }
                else
                    throw new ConfirmException("cptch_wrng_param");
            }
            else
            {
                var arg = context.ActionArguments.First(x => x.Value is ICaptchaViewModel);
                var objectWithCaptcha = arg.Value as ICaptchaViewModel;
                captcha = objectWithCaptcha.Captcha;
            }


            if (string.IsNullOrEmpty(captcha.Captcha))
                throw new ConfirmException("captcha_is_not_filled");

            if (captcha.Captcha.Length > MAX_CAPTCHA_VALUE_LENGTH)
                throw new ConfirmException("captcha_too_long");

            var calculatedHash = captcha.Captcha.Hash();
            var hashFromUser = captcha.Hash;
            var encodedBytes = hashFromUser.GetByteArrayFromBitConverter();
            var decoded = encodedBytes.DecryptStringFromBytes();
            var splitted = decoded.Split(GlobalConstats.CAPTCHA_SPLITTER);
            var captchaHash = Convert.ToInt64(splitted.First());
            var expirationTime = Convert.ToDateTime(splitted.Last());

            if (expirationTime < receivedTime)
                throw new ConfirmException("captcha_expired");

            if (calculatedHash != captchaHash)
                throw new ConfirmException("wrong_captcha_value");

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
