using FluentValidation;
using Listening.Core.ViewModels.Text;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Validators
{
    public class TextDtoValidator : AbstractValidator<TextDto>
    {
        private const int MaxLength = 4000;

        public TextDtoValidator(IStringLocalizer<TextDto> localizer)
        {
            // Perhaps... once upon a time it will be usefull to separate validation logic from dto...
            // use it http://cecilphillip.com/fluent-validation-rules-with-asp-net-core/
            RuleFor(text => text.Title).NotEmpty();
            RuleFor(text => text.Text).NotEmpty();
            RuleFor(text => text.Country).NotEmpty();
            RuleFor(text => text.Text).Must(text => text.Count() <= MaxLength)
                .WithMessage(localizer["unsupportable_text_length_4000"]);


            //RuleFor(m => m.AudioName).NotEmpty().When(m => string.IsNullOrEmpty(m.VideoName));
            //RuleFor(m => m.AudioName).Empty().When(m => !string.IsNullOrEmpty(m.VideoName));
            //RuleFor(m => m.VideoName).NotEmpty().When(m => string.IsNullOrEmpty(m.AudioName));
            //RuleFor(m => m.VideoName).Empty().When(m => !string.IsNullOrEmpty(m.AudioName));
        }
    }
}
