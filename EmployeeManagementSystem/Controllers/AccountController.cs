using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManagementSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EmployeeManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "employees");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (signInManager.IsSignedIn(User))
                    {
                        return RedirectToAction("index", "employees");
                    }
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "employees");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = await userManager.FindByEmailAsync(model.Email);
                if(loggedInUser != null)
                {
                    var result = await signInManager.PasswordSignInAsync(loggedInUser.UserName, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl))
                            return Redirect(returnUrl);
                        else
                            return RedirectToAction("index", "employees");
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }
                
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult FindEmail()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> FindEmail(FindEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = await userManager.FindByEmailAsync(model.Email);
                if (loggedInUser != null)
                    return RedirectToAction("ForgetPassword", new RouteValueDictionary(
                                                new { controller = "Account", action = "ForgetPassword", loggedInUser.Id }));
                ViewBag.Message = "User Not Found";
            }
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ForgetPassword(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var token = userManager.GeneratePasswordResetTokenAsync(user).Result;
            var forgetresetpassword = new ForgetResetPassword
            {
                id = user.Id,
                Token = token
            };
            return View(forgetresetpassword);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgetPasswordAsync(ForgetResetPassword model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.id);
                var changePassword = await userManager.ResetPasswordAsync(user,model.Token,model.Password);

                if (changePassword.Succeeded)
                {
                    ViewBag.Message = "Password Changed Successfully";
                    return RedirectToAction("Login", "Account");
                }
                ViewBag.Message = "Error while resetting the password!";
                foreach (var item in changePassword.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }
    }
}
