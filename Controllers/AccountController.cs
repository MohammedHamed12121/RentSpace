using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentSpace.Data;
using RentSpace.Models;
using RentSpace.ViewModels;

namespace RentSpace.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly ApplicationDbContext _context;

        public AccountController(ILogger<AccountController> logger, ApplicationDbContext context, SignInManager<AppUser> signinManager, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _signinManager = signinManager;
            _userManager = userManager;
        }


        #region Login
            
        public IActionResult Login()
        {
            var request = new LoginViewModel();
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            // get the user
            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress!);

            // if the user exist check the password 
            // and if the password correct authourize
            if(user != null)
            {
                var checkPassword = await _userManager.CheckPasswordAsync(user, loginViewModel.Password!);
                if(checkPassword)
                {
                    var result = await _signinManager.PasswordSignInAsync(user,loginViewModel.Password!, false, false);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index","Home");
                    }
                }
                TempData["Error"] = "Wrong Password, Enter your password again";
                return View(loginViewModel);
            }
            TempData["Error"] = "Wrong Email Address, Enter your Email again";
            return View(loginViewModel);
        }

        #endregion

        #region Register
            
        public IActionResult Register()
        {
            var request = new RegisterViewModel();
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(registerViewModel);
            }
            //get the user 
            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress!);
            // check if the user exists
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }

            // if not make new user 
            var newUser = new AppUser()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress
            };

            // save it to the database
            var newUserResponse = await _userManager.CreateAsync(newUser,registerViewModel.Password!);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Logout
            
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}