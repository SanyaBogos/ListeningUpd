using Listening.Server.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Listening.Server;

namespace Integration.Extensions
{
    internal static class AuthenticationExtensions
    {
        internal static async Task SetAuthCookie(this DatabaseFixture fixture, HttpResponseMessage response, HttpClient client)
        {
            var loginResultStream = await response.Content.ReadAsStringAsync();
            dynamic signInResultDynamic = JsonConvert.DeserializeObject(loginResultStream);
            string type = signInResultDynamic.token_type;
            string token = signInResultDynamic.access_token;
            client.DefaultRequestHeaders.Add("Authorization", $"{type} {token}");
        }

        internal static void DropAuthCookie(this DatabaseFixture fixture, HttpClient client)
        {
            client.DefaultRequestHeaders.Remove("Cookie");
            client.DefaultRequestHeaders.Remove("Authorization");
        }

        internal static async Task<HttpResponseMessage> Login(
            this DatabaseFixture fixture,
            SecurityRules.User user,
            HttpClient client)
        {
            var openIdConnectionParams = new Dictionary<string, string>
            {
                { OpenIdConnectParameterNames.Username, user.Email },
                { OpenIdConnectParameterNames.Password, user.Password },
                { OpenIdConnectParameterNames.GrantType, "password" },
                { OpenIdConnectParameterNames.Scope, "openid profile email offline_access roles" },
            };
            var request = new HttpRequestMessage(HttpMethod.Post, "/connect/token")
            {
                Content = new FormUrlEncodedContent(openIdConnectionParams)
            };
            var loginResult = await client.SendAsync(request);

            loginResult.EnsureSuccessStatusCode();
            return loginResult;
        }

        internal static async Task LoginAsAdmin(this DatabaseFixture fixture)
        {
            var admins = SecurityRulesSingleton.Instance.Rules.Users
                .Where(u => u.Role == GlobalConstats.ADMIN).Take(2);

            var login = await fixture.Login(admins.First(), fixture.AdminClient);
            await fixture.SetAuthCookie(login, fixture.AdminClient);
        }

        internal static async Task LoginAsUser(this DatabaseFixture fixture)
        {
            var users = SecurityRulesSingleton.Instance.Rules.Users
                .Where(u => u.Role == GlobalConstats.USER).Take(2);

            var login = await fixture.Login(users.First(), fixture.UserClient);
            await fixture.SetAuthCookie(login, fixture.UserClient);
        }
    }
}
