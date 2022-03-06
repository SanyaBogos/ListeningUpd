using System.Linq;
using System.Threading.Tasks;
using Listening.Core.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Listening.Infrastructure.Services;
using Microsoft.Extensions.Options;
using Listening.Web.Filters;
using Listening.Core.Entities;
using System.Security.Claims;
using AspNet.Security.OpenIdConnect.Primitives;
using Listening.Core.Entities.Custom;
using Listening.Infrastructure.Services.Custom;
using System.Collections.Generic;
using Listening.Infrastructure.Exceptions;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Listening.Infrastructure.Utilities;
using System;
//using System.IO;
using Listening.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Listening.Server.Services.Contracts;

namespace Listening.Web.Controllers.api.Custom
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        //private readonly IConfiguration _configuration;
        //private readonly string _captchaPath;
        private readonly IOptions<IdentityOptions> _identityOptions;
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IFileService _fileService;

        public AccountController(
            //IConfiguration configuration,
            //IHostingEnvironment env,
            UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> identityOptions,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IFileService fileService,
            ILogger<AccountController> logger) : base(userManager)
        {
            //_configuration = configuration;
            //_captchaPath = $"{env.WebRootPath}{configuration["Data:FileStorage:CaptchaPath"]}";
            _identityOptions = identityOptions;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _fileService = fileService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("whoAmI/")]
        public async Task<string> WhoAmI()
        {
            var user = await GetCurrentUserAsync();
            return user.Email;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (!user.IsEnabled)
            {
                _logger.LogWarning(2, "Disabled user tries to login");
                return BadRequest(new ApiError("Issue with login"));
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                //var user = await _userManager.FindByEmailAsync(model.Email);
                var roles = await _userManager.GetRolesAsync(user);
                _logger.LogInformation(1, "User logged in.");
                return AppUtils.SignIn(user, roles);
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToAction(nameof(SendCode), new { RememberMe = model.RememberMe });
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning(2, "User account locked out.");
                return BadRequest(new ApiError("Lockout"));
            }
            else
            {
                return BadRequest(new ApiError("Invalid login attempt."));
            }
        }

        [HttpPost("rgstr")]
        [AllowAnonymous]
        [CaptchaFilter]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            return await RegisterNewUser(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }



        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel model)
        {
            var currentUser = await _userManager.FindByNameAsync(model.Email);
            if (currentUser == null || !(await _userManager.IsEmailConfirmedAsync(currentUser)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return NoContent();
            }
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            // Send an email with this link
            var code = await _userManager.GeneratePasswordResetTokenAsync(currentUser);

            var host = Request.Scheme + "://" + Request.Host;
            var callbackUrl = host + "?userId=" + currentUser.Id + "&passwordResetCode=" + code;
            var confirmationLink = "<a class='btn-primary' href=\"" + callbackUrl + "\">Reset your password</a>";
            await _emailSender.SendEmailAsync(model.Email, "Forgotten password email", confirmationLink);
            return NoContent(); // sends 204
        }

        [HttpPost("resetpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user == null)
            {
                // Don't reveal that the user does not exist
                return Ok("Reset confirmed");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return Ok("Reset confirmed");
            }
            AddErrors(result);
            return BadRequest(new ApiError(ModelState));
        }

        [HttpGet("SendCode")]
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return BadRequest(new ApiError("Error"));
            }
            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(user);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        [HttpPost("SendCode")]
        [AllowAnonymous]
        public async Task<IActionResult> SendCode([FromBody] SendCodeViewModel model)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return BadRequest(new ApiError("Error"));
            }

            // Generate the token and send it
            var code = await _userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);
            if (string.IsNullOrWhiteSpace(code))
            {
                return BadRequest(new ApiError("Error"));
            }

            var message = "Your security code is: " + code;
            if (model.SelectedProvider == "Email")
            {
                await _emailSender.SendEmailAsync(user.Email, "Security Code", message);
            }
            // else if (model.SelectedProvider == "Phone")
            // {
            //     await _smsSender.SendSmsAsync(await _userManager.GetPhoneNumberAsync(user), message);
            // }

            return RedirectToAction(nameof(VerifyCode), new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        [HttpGet("VerifyCode")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyCode(string provider, bool rememberMe, string returnUrl = null)
        {
            // Require that the user has already logged in via username/password or external login
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return BadRequest(new ApiError("Error"));
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        [HttpPost("VerifyCode")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe, model.RememberBrowser);
            if (result.Succeeded)
            {
                // return RedirectToLocal(model.ReturnUrl);
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning(7, "User account locked out.");
                return View("Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid code.");
                return View(model);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("captchaImg")]
        public Listening.Core.ViewModels.CaptchaDto GetCaptchaImage()
        {
            var result = _fileService.GenerateCaptcha();
            return result;
        }

        // for test purpose (need to avoid capture)
#if DEBUG
        [HttpPost("hiddenRegister")]
        [AllowAnonymous]
        public async Task<IActionResult> HiddenRegister([FromBody] RegisterViewModel model)
        {
            return await RegisterNewUser(model);
        }
#endif

        private async Task<IActionResult> RegisterNewUser(RegisterViewModel model)
        {
            var currentUser = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                //FirstName = model.Firstname,
                //LastName = model.Lastname,
                Mobile = model.Mobile,
                IsEnabled = true
            };

            var result = await _userManager.CreateAsync(currentUser, model.Password);
            if (result.Succeeded)
            {
                // Add custom claim
                // ASP.NET Identity does not remember claim value types. So, if it was important that the office claim be an integer(rather than a string)
                var officeClaim = new Claim(OpenIdConnectConstants.Claims.Email,
                    currentUser.Email.ToString(), ClaimValueTypes.Integer);
                await _userManager.AddClaimAsync(currentUser, officeClaim);

                // Add to roles
                var roleAddResult = await _userManager.AddToRoleAsync(currentUser, "User");
                if (roleAddResult.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(currentUser);

                    var host = Request.Scheme + "://" + Request.Host;
                    var callbackUrl = host + "?userId=" + currentUser.Id + "&emailConfirmCode=" + code;
                    var confirmationLink = "<a class='btn-primary' href=\"" + callbackUrl + "\">Confirm email address</a>";
                    _logger.LogInformation(3, "User created a new account with password.");
                    await _emailSender.SendEmailAsync(model.Email, "Registration confirmation email", confirmationLink);
                    return NoContent();
                }
            }

            AddErrors(result);

            // If we got this far, something failed, redisplay form
            return BadRequest(new ApiError(ModelState));
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        #endregion
    }
}
