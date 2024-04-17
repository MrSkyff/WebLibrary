using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using WebProject.Areas.Account.Data.Interfaces;
using WebProject.Areas.Account.Models;
using WebProject.Areas.Account.Models.ViewModels;
using WebProject.Data.Interfaces;
using WebProject.Models.Account;

namespace WebProject.Areas.Account.Controllers
{
    [Area("Account")]
    public class IdentityController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IInvite _inviteRepository;
        private readonly IEmailService _emailService;

        public IdentityController(SignInManager<User> signInManager, UserManager<User> userManager, IInvite inviteRepository, IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _inviteRepository = inviteRepository;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Register(string? value = "")
        {
            var model = _inviteRepository.GetInvtieByInviteCode(value);

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "Identity" });
            }

            if (value.Length == 0)
            {
                ViewBag.InviteStatus = InviteStatus.Empty;
                return PartialView();
            }

            if (model.InviteCode is not null)
            {
                if (model.IsUsed)
                {
                    ViewBag.InviteStatus = InviteStatus.Used;
                    return PartialView();
                }
                if (model.ValidTill < DateTime.Now)
                {
                    ViewBag.InviteStatus = InviteStatus.Expired;
                    return PartialView();
                }

                ViewBag.InviteStatus = InviteStatus.Valid;
                RegisterViewModel registerViewModel = new()
                {
                    Email = model.Email,
                    FullName = model.FullName,
                    InviteCode = model.InviteCode
                };

                return PartialView(registerViewModel);
            }

            ViewBag.InviteStatus = InviteStatus.InValid;
            return PartialView();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var inviteModel = _inviteRepository.GetInvtieByInviteCode(viewModel.InviteCode);

                if (inviteModel is not null && inviteModel.ValidTill > DateTime.Now && !inviteModel.IsUsed)
                {
                    var user = new User
                    {
                        UserName = viewModel.UserName,
                        Email = viewModel.Email,
                        FullName= viewModel.FullName,
                    };

                    var result = await _userManager.CreateAsync(user, viewModel.Password);

                    if (result.Succeeded)
                    {
                        inviteModel.UseDate = DateTime.Now.ToString();
                        inviteModel.IsUsed = true;
                        await _inviteRepository.UpdateInviteAsync(inviteModel);

                        // Generate email confirmation token
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                        // Generate confirmation link
                        var callbackUrl = Url.Action(
                            action: "ConfirmEmail",
                            controller: "Identity",
                            values: new { userId = user.Id, token },
                            protocol: Request.Scheme);

                        // Send the confirmation email
                        await _emailService.SendEmailAsync(
                            user.Email,
                            "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home", new { area = "Account" });
                    }
                    else
                    {
                        return PartialView();
                    }
                }
            }

            return PartialView();
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "Account" });
            }
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, viewModel.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "Account" });
                }
                else if (result.IsLockedOut)
                {
                    return RedirectToAction("Lockout", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                    return PartialView();
                }
            }
            return PartialView();
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? "ConfirmEmail" : "ConfirmEmailError");
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToAction("Index", "Home", new { area = "Account" });
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> SendConfirmationEmail()
        {
            User user = await _userManager.GetUserAsync(User);

            // Generate email confirmation token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // Generate confirmation link
            var callbackUrl = Url.Action(
                action: "ConfirmEmail",
                controller: "Identity",
                values: new { userId = user.Id, token },
                protocol: Request.Scheme);

            // Send the confirmation email
            await _emailService.SendEmailAsync(
                user.Email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            return View();
        }
    }
}
