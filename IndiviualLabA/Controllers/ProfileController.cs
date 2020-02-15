using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndiviualLabA.Data;
using IndiviualLabA.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IndiviualLabA.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Shows the form to users for updating their profile.
        [Authorize]
        [HttpGet]
        public IActionResult UpdateProfile(bool updated)
        {
            CustomUserRepo customUserVMRepo = new CustomUserRepo(this._context);
            var userName = User.Identity.Name;
            CustomUser customUser = customUserVMRepo.GetUser(userName);
            ViewBag.Status = ""; // Clears the ViewBag when the form is loaded.
            return View(customUser);
        }

        // Receives form input to update the user's profile.
        [Authorize]
        [HttpPost]
        public IActionResult UpdateProfile(CustomUser customUser)
        {

            // Update the profile but only if the contents are valid.
            // ModelState.IsValid enforces server-side validation which is critical.
            if (this.ModelState.IsValid)
            {
                // Get current user name.
                var userName = User.Identity.Name;

                // Update the profile for the current user only.
                if (userName.ToLower() == customUser.UserName.ToLower())
                {
                    CustomUserRepo customUserVMRepo = new CustomUserRepo(this._context);
                    string result = customUserVMRepo.UpdateProfile(customUser);

                    // Show the form again with error message if update failed.
                    if (result != "OK")
                    {
                        ViewBag.Status = result;
                    }
                    else
                    {
                        ViewBag.Status = "Your profile has been updated.";
                    }
                    return View(customUser);
                }
            }
            ViewBag.Status = "The update failed due to invalid content. Please try again";
            return View(customUser);
        }
    }

}