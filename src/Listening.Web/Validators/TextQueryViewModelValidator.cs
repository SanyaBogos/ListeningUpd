using FluentValidation;
using FluentValidation.Results;
using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels.Text;
using Listening.Server.Entities.Specialized.Text;
using Listening.Web.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Validators
{
    public class TextQueryViewModelValidator : BaseQueryViewModelValidator<TextQueryViewModel>
    {
        public TextQueryViewModelValidator(
            IStringLocalizer<TextQueryViewModel> localizer) : base(localizer) { }

        protected override void ConstructProperties()
        {
            _sortingPropertiesAvailable = new string[] { nameof(Text.Title), nameof(Text.SubTitle) };
            _filteringPropertiesAvailable = new string[] {
                nameof(Text.Title), nameof(Text.SubTitle), nameof(Text.Country), nameof(Text.Complexity),
                //nameof(Text.Assignee)
            };

            //if (IsAdminOrSuper().GetAwaiter().GetResult())
            //{
            //    var additions = new List<string> { nameof(Text.Assignee) };
            //    _filteringPropertiesAvailable.AddRange(additions);
            //}

            //_filteringPropertiesAvailable = filteringPropertiesAvailable.ToArray();
        }

        //private async Task<bool> IsAdminOrSuper()
        //{
        //    var user = await _userManager.GetUserAsync(_context.HttpContext.User);
        //    var userRoles = await _userManager.GetRolesAsync(user);
        //    var roles = new string[] { GlobalConstats.ADMIN, GlobalConstats.SUPER };
        //    return (await _userManager.GetRolesAsync(user)).Intersect(roles).Count() >= 1;
        //}
    }
}
