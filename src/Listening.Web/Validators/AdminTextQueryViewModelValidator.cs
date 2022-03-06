using Listening.Core.ViewModels.Text;
using Listening.Server;
using Listening.Server.Entities.Specialized.Text;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Web.Validators
{
    public class AdminTextQueryViewModelValidator : BaseQueryViewModelValidator<AdminTextQueryViewModel>
    {
        public AdminTextQueryViewModelValidator(
            IStringLocalizer<TextQueryViewModel> localizer
            ) : base(localizer) { }

        protected override void ConstructProperties()
        {
            _sortingPropertiesAvailable = new string[] { nameof(Text.Title), nameof(Text.SubTitle) };
            _filteringPropertiesAvailable = new string[] {
                nameof(Text.Title), nameof(Text.SubTitle), nameof(Text.Country), nameof(Text.Complexity),
                nameof(Text.Assignee), GlobalConstats.CREATED_NAME, GlobalConstats.UPDATED_NAME
            };
        }
    }
}
