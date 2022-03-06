using FluentValidation;
using Listening.Core.ViewModels.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Web.Validators
{
    public class PostWriteDtoValidator: AbstractValidator<PostWriteDto>
    {
        public PostWriteDtoValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Message).NotEmpty();
            RuleFor(x => x.Header).NotEmpty();
        }
    }
}
