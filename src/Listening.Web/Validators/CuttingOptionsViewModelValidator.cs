using FluentValidation;
using Listening.Core.ViewModels.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Validators
{
    public class CuttingOptionsViewModelValidator : AbstractValidator<CuttingOptionsViewModel>
    {
        private const int MAX_VIDEO_DURATION = 150;

        public CuttingOptionsViewModelValidator()
        {
            RuleFor(x => x.From == x.To).Must(x => x == false)
                .WithMessage("from_is_not_to");

            RuleFor(x => x.To - x.From).Must(x => x <= MAX_VIDEO_DURATION)
                .WithMessage("max_duration_exceeds");
        }
    }
}
