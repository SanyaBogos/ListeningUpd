using Listening.Core.Entities.Specialized.Feedback;
using Listening.Core.ViewModels.Feedback;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace Listening.Web.Validators
{
    public class FeedbackQueryViewModelValidator : BaseQueryViewModelValidator<FeedbackQueryViewModel>
    {
        public FeedbackQueryViewModelValidator(IStringLocalizer<FeedbackQueryViewModel> localizer) : base(localizer) { }

        protected override void ConstructProperties()
        {
            _sortingPropertiesAvailable = new string[] { nameof(Feedback.CreatedTime) };
            _filteringPropertiesAvailable = new string[] {
                nameof(Feedback.Topic), nameof(Feedback.Details), nameof(Feedback.UserId)
            };
        }
    }
}
