using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentSpace.Enums;
using RentSpace.Extensions;
using RentSpace.Helpers;
using RentSpace.Interfaces;
using RentSpace.Models;
using RentSpace.ViewModels;
using Newtonsoft.Json;

namespace RentSpace.Controllers
{
    [Authorize]
    public class SpaceController : Controller
    {
        #region Constructor
        private readonly ILogger<SpaceController> _logger;
        private readonly ISpaceRepository _spaceRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SpaceController(ILogger<SpaceController> logger, ISpaceRepository spaceRepository, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _spaceRepo = spaceRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index(decimal? minPrice, decimal? maxPrice, int pg = 1, string search = "")
        {
            // get all spaces
            IEnumerable<Space> allSpaces = await _spaceRepo.GetAllSpaceAsync(search, minPrice, maxPrice);
            // apply paginations
            // TODO: Move the pageination into its own service and injected in the spaces repository
            // make sure page always begin from 1 not smaller 
            const int pageSize = 3;
            if (pg < 1)
            {
                pg = 1;
            }

            int recsCount = allSpaces.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var spaces = allSpaces.Skip(recSkip).Take(pager.PageSize).ToList();
            ViewBag.pager = pager;
            // make a view model for spaces 
            var spacesVM = new List<SpaceCardViewModel>() { };
            foreach (var space in spaces)
            {
                var spaceVM = new SpaceCardViewModel()
                {
                    space = space,
                    PostUserName = _spaceRepo.GetSpaceUserNameAsync(space.AppUserId).Result
                };
                spacesVM.Add(spaceVM);
            }
            // return them to the view
            return View(spacesVM);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int id)
        {
            // TODO: the null possible value
            // get the space using the id
            Space space = await _spaceRepo.GetSpaceByIdAsync(id);
            // return the space
            return View(space);
        }
        #endregion

        #region CreateSpace
        public IActionResult Create()
        {
            // get the current user id
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            // return it to the view
            var createSpaceVM = new CreateAndEditSpaceViewModel
            {
                AppUserId = currentUserId,
            };
            return View(createSpaceVM);
        }

        [HttpPost]
        public IActionResult Create(CreateAndEditSpaceViewModel space)
        {
            if (!ModelState.IsValid)
            {
                return View(space);
            }
            // get the new space 
            var newSpace = new Space
            {
                AppUserId = space.AppUserId,
                Title = space.Title,
                ShortDescription = space.ShortDescription,
                Description = space.Description,
                Image = space.Image,
                InitialPrice = space.InitialPrice,
                SpaceCategory = space.SpaceCategory,
                Country = space.Country,
                City = space.City,
                State = space.State,
                CreateAt = new DateTime()
            };
            // add it the database
            _spaceRepo.Add(newSpace);
            // redirect the index
            return RedirectToAction("Index");
        }
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int id)
        {
            // get the space from the id
            var space = await _spaceRepo.GetSpaceByIdAsync(id);
            // check that the space exist
            if (space is null)
            {
                return View("Error");
            }
            // get the old space data
            var spaceVM = new CreateAndEditSpaceViewModel
            {
                Id = id,
                AppUserId = space.AppUserId,
                Title = space.Title,
                ShortDescription = space.ShortDescription,
                Description = space.Description,
                Image = space.Image,
                SpaceCategory = space.SpaceCategory,
                Country = space.Country,
                City = space.City,
                State = space.State,
            };


            // return it with the view
            return View(spaceVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateAndEditSpaceViewModel spaceViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit space");
                return View("Edit", spaceViewModel);
            }
            // get the space
            var userSpace = await _spaceRepo.GetSpaceByIdAsyncWithNoTracking(id);
            // if it's exist update and save else return the view the false data
            if (userSpace is not null)
            {
                // TODO: problem with edit
                var space = new Space
                {
                    Id = spaceViewModel.Id,
                    AppUserId = spaceViewModel.AppUserId,
                    Title = spaceViewModel.Title,
                    ShortDescription = spaceViewModel.ShortDescription,
                    Description = spaceViewModel.Description,
                    Image = spaceViewModel.Image,
                    InitialPrice = spaceViewModel.InitialPrice,
                    SpaceCategory = spaceViewModel.SpaceCategory,
                    Country = spaceViewModel.Country,
                    City = spaceViewModel.City,
                    State = spaceViewModel.State,
                    CreateAt = new DateTime()
                };

                _spaceRepo.Update(space);

                var toastrMessage = new ToastrMessage
                {
                    Message = "Your space have been edited successfully",
                    Type = ToastrMessageType.Success
                };
                TempData["ToastrMessage"] = JsonConvert.SerializeObject(toastrMessage);

                return RedirectToAction("Index");
            }
            else
            {
                return View(spaceViewModel);

            }
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            // get the space 
            var spaceDetails = await _spaceRepo.GetSpaceByIdAsync(id);
            // check if it's exist
            if (spaceDetails == null)
            {
                return View("Error");
            }
            // return view
            return View(spaceDetails);
        }

        [HttpPost, ActionName("DeleteSpace")]
        public async Task<IActionResult> DeleteSpace(int id)
        {
            // get the space
            var spaceDetails = await _spaceRepo.GetSpaceByIdAsync(id);
            //check if its exist
            if (spaceDetails == null)
            {
                return View("Error");
            }
            // delete it
            _spaceRepo.Delete(spaceDetails);
            // redirct to the index
            return RedirectToAction("Index");
        }
        #endregion

        #region  Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        #endregion
    }
}