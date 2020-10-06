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
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, AppDbContext context
                                    , IOptions<ApplicationSettings> appSettings, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
            this.appSettings = appSettings;
            _configuration = configuration;
        }
        [AllowAnonymous]
        [Route("chatHub")]
        public IActionResult post()
        {
            return Ok();
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
                if (loggedInUser != null && await userManager.CheckPasswordAsync(loggedInUser, model.Password))
                {
                    //var result = await signInManager.PasswordSignInAsync(loggedInUser.UserName, model.Password, model.RememberMe, false);


                    var userRoles = await userManager.GetRolesAsync(loggedInUser);

                    var authClaims = new List<Claim>
                        {
                             new Claim("name", loggedInUser.UserName),
                    new Claim(ClaimTypes.Name, loggedInUser.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim("role", userRole));
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ApplicationSettings:JWT_Secret"]));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["ApplicationSettings:ValidIssuer"],
                        audience: _configuration["ApplicationSettings:ValidAudience"],
                        expires: DateTime.Now.AddHours(3),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                    //var role = await userManager.GetRolesAsync(loggedInUser);

                    //var tokenDescriptor = new SecurityTokenDescriptor
                    //{
                    //    Subject = new ClaimsIdentity(new Claim[]
                    //     {
                    //             new Claim("UserID", loggedInUser.Id.ToString()),
                    //             new Claim("role",role.FirstOrDefault()),
                    //     }),
                    //    Expires = DateTime.UtcNow.AddHours(2),
                    //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Value.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                    //};
                    //var tokenHandler = new JwtSecurityTokenHandler();
                    //var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    //var token = tokenHandler.WriteToken(securityToken);

                    //return Ok(new {token});

                }

            }
            return BadRequest(new { message = "Username or password is incorrect." });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ForgetResetPassword")]
        public async Task<IActionResult> ForgetPasswordAsync(ForgetResetPassword model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.email);
                var token = userManager.GeneratePasswordResetTokenAsync(user).Result;
                var changePassword = await userManager.ResetPasswordAsync(user, token, model.password);

                if (changePassword.Succeeded)
                {
                    return Ok();
                }
            }
            return BadRequest(new { message = "Email Not Found!!" });
        }
    }
}
