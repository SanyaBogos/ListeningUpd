using FluentValidation;
using Listening.Core.ViewModels.DebianFAI;
using Listening.Infrastructure.Extensions;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Web.Validators
{
    public class PreseedSettingsViewModelValidator : AbstractValidator<PreseedSettingsViewModel>
    {
        public PreseedSettingsViewModelValidator(IStringLocalizer<PreseedSettingsViewModel> localizer)
        {
            var namePattern = "[a-zA-Z0-9]";
            var pwdPattern = "[a-zA-Z0-9 _-]";
            var softPattern = "[a-zA-Z0-9 _-]";
            var mirrorPattern = "[a-zA-Z0-9._-]";

            // For now it doesn't work fine, therefore implemented using another way
            // RuleFor(settings => settings.Mirror).NotEmpty()
            //     .Must(LinkMustBeAUri)
            //     .WithMessage("Link '{PropertyValue}' must be a valid URI. eg: http://www.SomeWebSite.com.au"); ;

            RuleFor(settings => settings.Mirror).NotEmpty().Matches(mirrorPattern);
            RuleFor(settings => settings.RootPassword).NotEmpty().Matches(pwdPattern);
            RuleFor(settings => settings.UserFullName).NotEmpty().Matches(namePattern);
            RuleFor(settings => settings.UserName).NotEmpty().Matches(namePattern);
            RuleFor(settings => settings.UserPassword).NotEmpty().Matches(pwdPattern);
            RuleFor(settings => settings.AdditionalSoft).NotEmpty().Matches(softPattern);
            RuleFor(settings => settings.HddSplitSettingsVM).NotEmpty();
            RuleFor(settings => settings.DeviceType).NotEmpty().IsInEnum();//.IsEnumName(typeof(DeviceType));
        }

        private static bool LinkMustBeAUri(string link)
        {
            return link.IsUri();
        }
    }
}
