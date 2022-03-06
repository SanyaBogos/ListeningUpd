using Listening.Core.Entities.Custom;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.Extensions
{
    internal static class UserExtensions
    {
        internal static async Task<ApplicationUser> GetNewUser(this DatabaseFixture fixture)
        {
            var userResponse = await fixture.AdminClient.PostAsync(
                $"api/Manage/getUsersByEmails",
                (new string[] { fixture.NewUserRegisterVM.Email }).Serialize().AsContent());

            userResponse.EnsureSuccessStatusCode();

            var user = (await userResponse.Deserialize<ApplicationUser[]>()).FirstOrDefault();
            return user;
        }

        internal static async Task CreateNewUser(this DatabaseFixture fixture)
        {
            var content = fixture.NewUserRegisterVM.Serialize().AsContent();
            var response = await fixture.AnonymousClient.PostAsync("api/Account/hiddenRegister", content);
            response.EnsureSuccessStatusCode();
        }

        internal static async Task DeleteNewUser(this DatabaseFixture fixture)
        {
            var deleteResult = await fixture.AdminClient.DeleteAsync(
                $"api/Manage/delete?email={fixture.NewUserRegisterVM.Email}");
            deleteResult.EnsureSuccessStatusCode();
        }
    }
}
