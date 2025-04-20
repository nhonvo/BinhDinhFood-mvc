using System.Security.Claims;
using BinhDinhFood.Application.Interfaces;
using BinhDinhFood.Presentation.Models.Authentication;
using BinhDinhFood.Presentation.Models.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BinhDinhFood.Controllers;

[AllowAnonymous]
public class UserController(IUserRepository repo, IUserManager userManager, IMailService mailService, ITokenRepository tokenRepository) : Controller
{
    private readonly IUserRepository _repo = repo;
    private readonly IUserManager _userManager = userManager;
    private readonly IMailService _mailService = mailService;
    private readonly ITokenRepository _tokenRepository = tokenRepository;

    // Helper to check authentication and redirect to login if necessary
    private IActionResult RedirectIfNotAuthenticated()
    {
        return !User.Identity.IsAuthenticated || User.IsInRole("Admin") ? RedirectToAction("Login") : (IActionResult?)null;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = _repo.RegisterAsync(model);
        await _userManager.SignIn(HttpContext, user, false);

        return LocalRedirect("~/Home/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        if (User.Identity.IsAuthenticated)
        {
            await _userManager.SignOut(HttpContext);
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index", "User");
        }

        var user = _repo.Validate(model);
        if (user == null)
        {
            return View(model);
        }

        await _userManager.SignIn(HttpContext, user, model.RememberLogin);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Logout(string returnUrl)
    {
        if (!User.Identity.IsAuthenticated || User.IsInRole("Admin"))
        {
            return RedirectToAction("Index", "Home");
        }

        await _userManager.SignOut(HttpContext);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPasswordAsync(ForgotViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userExists = await _repo.HaveAccountAsync(model);
            if (!userExists)
            {
                ViewBag.Message = "Your username or email is incorrect!";
                return View(model);
            }

            string resetLink = _repo.CreateResetPasswordLink(model.UserName);
            await _mailService.SendEmailAsync(new MailRequest
            {
                ToEmail = model.Email,
                Subject = "Reset password",
                Body = resetLink
            });
            return RedirectToAction("ShowMessage");
        }

        ViewBag.Message = "Please fill out all information before submitting!";
        return View(model);
    }

    [HttpGet]
    public IActionResult ShowMessage()
    {
        ViewBag.Message = "Password reset link has been sent to your email. Please check your mailbox.";
        return View();
    }

    [HttpGet]
    public IActionResult ResetPassword(string user, string token)
    {
        if (_tokenRepository.CheckToken(user, token))
        {
            ViewBag.CustomerUserName = user;
            ViewBag.Token = token;
            return View();
        }

        return RedirectToAction("Login");
    }

    [HttpPost]
    public async Task<IActionResult> ResetPasswordAsync(ResetViewModel model)
    {
        if (ModelState.IsValid && _tokenRepository.CheckToken(model.UserName, model.Token))
        {
            await _repo.ResetPasswordAsync(model);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.CustomerUserName = model.UserName;
        ViewBag.Token = model.Token;
        return View(model);
    }

    [HttpGet]
    public IActionResult ChangeInfor()
    {
        var redirectResult = RedirectIfNotAuthenticated();
        if (redirectResult != null) return redirectResult;

        int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        InfoViewModel model = _repo.GetUserInfo(id);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ChangeInforAsync(InfoViewModel model)
    {
        var redirectResult = RedirectIfNotAuthenticated();
        if (redirectResult != null) return redirectResult;

        int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        IFormFileCollection files = HttpContext.Request.Form.Files;
        await _repo.ChangeInfoUserAsync(model, id, files);

        return RedirectToAction("Profile");
    }

    public async Task<IActionResult> ClearImage()
    {
        int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        await _repo.ClearImageAsync(id);
        return RedirectToAction("Profile");
    }

    [HttpGet]
    public IActionResult ChangePass()
    {
        return !User.Identity.IsAuthenticated || User.IsInRole("Admin") ? RedirectToAction("Login") : View();
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassAsync(ChangePasswordViewModel model)
    {
        if (!User.Identity.IsAuthenticated || User.IsInRole("Admin"))
        {
            return RedirectToAction("Login");
        }

        if (ModelState.IsValid)
        {
            int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string username = User.FindFirstValue(ClaimTypes.Name);

            if (await _repo.HaveAccountAsync(username, model.OldPassword))
            {
                await _repo.ChangePasswordUserAsync(model, id);
                return RedirectToAction("Profile");
            }

            ViewBag.Message = "Your password is incorrect! Please try again.";
        }

        return View(model);
    }

    public IActionResult Profile()
    {
        var redirectResult = RedirectIfNotAuthenticated();
        if (redirectResult != null) return redirectResult;

        int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        InfoViewModel model = _repo.GetUserInfo(id);
        return View(model);
    }
}
