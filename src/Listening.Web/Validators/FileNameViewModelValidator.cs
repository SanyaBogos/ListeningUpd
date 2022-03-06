using FluentValidation;
using Listening.Core.ViewModels.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Validators
{
    public class FileNameViewModelValidator: AbstractValidator<FileNameViewModel>
    {
        public FileNameViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
