using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentSpace.Extensions;
using RentSpace.Interfaces;
using RentSpace.Models;
using RentSpace.ViewModels;

namespace RentSpace.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        #region Constructor (DI)
        private readonly ILogger<UserProfileController> _logger;
        private readonly IProfileRepository _profileRepo;
        private readonly IUserRepository _userRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserProfileController(ILogger<UserProfileController> logger, IProfileRepository profileRepo, IHttpContextAccessor httpContextAccessor, IUserRepository userRepo)
        {
            _logger = logger;
            _profileRepo = profileRepo;
            _httpContextAccessor = httpContextAccessor;
            _userRepo = userRepo;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index(string id)
        {
            // get user spaces
            // var curUserId = _httpContextAccessor.HttpContext!.User.GetUserId();
            var userSpaces = await _profileRepo.GetAllUserSpaces(id);
            var curUser = await _userRepo.GetUserByIdAsync(id);
            // add them to a view model
            var profileVM = new ProfileViewModel()
            {
                State = curUser.State,
                City = curUser.City,
                Country = curUser.Country,
                Space = userSpaces
            };
            // return the view
            return View(profileVM);

        }
        #endregion

        #region GetUserById
        // public async Task<IActionResult> GetUserById(string id)
        // {
        //     var userSpaces = await _profileRepo.GetAllUserSpaces(id);
        //     var user = await _userRepo.GetUserByIdAsync(id);
        //     var profileVM = new ProfileViewModel()
        //     {
        //         State = user.State,
        //         City = user.City,
        //         Country = user.Country,
        //         Space = userSpaces
        //     };
        //     return View(profileVM);

        // }
        #endregion
        #region Edit
        public async Task<IActionResult> EditUserProfile()
        {
            // get the current user profile
            var curUserId = _httpContextAccessor.HttpContext!.User.GetUserId();
            var user = await _profileRepo.GetUserById(curUserId);

            if (user == null)
            {
                return View("Error");
            }
            // edit the user and return the  view
            var editProfileViewModel = new EditUserProfileViewModel()
            {
                Id = curUserId,
                ProfileImageUrl = user.ProfileImageUrl,
                Country = user.Country,
                City = user.City,
                State = user.State
            };
            return View(editProfileViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(string id, EditUserProfileViewModel editProfileVM)
        {
            if (ModelState.IsValid)
            {
                // get the user id if it exist add the new info and update
                var user = await _userRepo.GetUserByIdAsyncWithNoTracking(id);
                if (user is not null)
                {
                    var newInfo = new AppUser()
                    {
                        Id = id,
                        ProfileImageUrl = user.ProfileImageUrl,
                        Country = user.Country,
                        City = user.City,
                        State = user.State,
                    };
                    _profileRepo.Update(newInfo);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("EditUserProfile", editProfileVM);
                }
            }
            else
            {
                ModelState.AddModelError("", "Falied to edit profile");
                return View("EditUserProfile", editProfileVM);
            }
        }
        #endregion

        #region Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        #endregion
    }
}