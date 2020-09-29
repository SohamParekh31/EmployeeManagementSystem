using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagementSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly AppDbContext context;
        private readonly IOptions<ApplicationSettings> appSettings;

        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager,AppDbContext context
                                    , IOptions<ApplicationSettings> appSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
            this.appSettings = appSettings;
        }
        [HttpGet]
        [Route("Logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
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
        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult Login()
        //{
        //    return View();
        //}
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = await userManager.FindByEmailAsync(model.Email);
                if(loggedInUser != null)
                {
                    var result = await signInManager.PasswordSignInAsync(loggedInUser.UserName, model.Password, model.RememberMe, false);
                    
                    if (result.Succeeded)
                    {
                        IdentityOptions _options = new IdentityOptions();
                        var role = await userManager.GetRolesAsync(loggedInUser);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                             {
                                     new Claim("UserID", loggedInUser.Id.ToString()),
                                     new Claim(_options.ClaimsIdentity.RoleClaimType,role.FirstOrDefault())
                             }),
                            Expires = DateTime.UtcNow.AddDays(1),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Value.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                        var token = tokenHandler.WriteToken(securityToken);

                        return Ok(new {token});
                        
                    }
                }
                
            }
            return BadRequest(new { message = "Username or password is incorrect." });
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
