using Listening.Core.ViewModels.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Helpers;
using Listening.Infrastructure.Utilities;

namespace Integration.Extensions
{
    internal static class DatabaseFixtureExtensions
    {
        private const int DefaultMaxItemsCount = 50;
        private const int CaptchaTTL = 30000;

        internal static async Task Initialize(this DatabaseFixture fixture)
        {
            var captchaValue = RandomHelper.String(0, 8);
            fixture.NewUserRegisterVM = new RegisterViewModel
            {
                Username = "Barada",
                Firstname = "Name",
                Lastname = "Surname",
                Email = "dasistfantastisch125@gmail.com",
                Password = "123qweASD!@#",
                Captcha = new Listening.Core.ViewModels.CaptchaCheckDto {
                    Captcha = captchaValue,
                    Hash = CaptchaHelper.GenerateHash(captchaValue, CaptchaTTL)
                } 
                //ConfirmPassword = "123qweASD!@#"
            };

            await fixture.LoginAsAdmin();
            await fixture.LoginAsUser();

            fixture.NewUser = await fixture.GetNewUser();
            if (fixture.NewUser == null)
            {
                await fixture.CreateNewUser();
                fixture.NewUser = await fixture.GetNewUser();
            }

            fixture.MaxResultId = await fixture.ResultRepository.GetMaxId();
            fixture.MaxFeedbackId = await fixture.FeedbackRepository.GetMaxId();

            fixture.CleanFiles();
            await fixture.CleanupTexts();
            await fixture.DeleteResultsFromId(DefaultMaxItemsCount);
            await fixture.DeleteFeedbacksFromId(DefaultMaxItemsCount);

            await fixture.InsertUserResults();
            await fixture.InsertTexts();
            await fixture.InsertFeedbacks();

            fixture.PrepareExistedMediaFiles();
            fixture.OcrImageName = fixture.PrepareOCRFile();
        }
    }
}
