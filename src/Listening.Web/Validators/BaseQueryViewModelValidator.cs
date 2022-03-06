using FluentValidation;
using Listening.Core.ViewModels;
using Listening.Infrastructure.Extensions;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Web.Validators
{
    public abstract class BaseQueryViewModelValidator<T> : AbstractValidator<T>
        where T : QueryViewModel
    {
        protected readonly IStringLocalizer _localizer;
        protected string[] _sortingPropertiesAvailable;
        protected string[] _filteringPropertiesAvailable;

        public BaseQueryViewModelValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
            ConstructProperties();
            SetupRules();
        }

        protected abstract void ConstructProperties();

        protected virtual void SetupRules()
        {
            RuleFor(x => x.SortingName)
                .Must(sortName => _sortingPropertiesAvailable.Contains(sortName))
                .When(x => !string.IsNullOrEmpty(x.SortingName))
                .WithMessage(x => string.Format(_localizer["sort_not_avail"], x.SortingName));

            RuleFor(x => x.FilteringProperties)
                .Must(filterProps => GetExceedsFilterProperties(filterProps).Count() == 0)
                .When(x => x.FilteringProperties != null)
                .WithMessage(x => string.Format(_localizer["search_not_avail"],
                    string.Join(", ", GetExceedsFilterProperties(x.FilteringProperties))));
        }

        private IEnumerable<string> GetExceedsFilterProperties(Dictionary<string, string> filterProps)
        {
            var filteringPrepared = _filteringPropertiesAvailable.Select(x => x.GetFirstPartBeforeSymbol(' '));
            var keysPrepared = filterProps.Keys.Select(x => x.GetFirstPartBeforeSymbol(' '));

            return keysPrepared.Except(filteringPrepared);
        }
    }
}
