using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopping_tutorial.Models;
using System.ComponentModel.DataAnnotations;

namespace shopping_tutorial.Controllers
{
    
    public class AccountController : Controller
    {
        private UserManager<AppUserModel> _userManager;
        private SignInManager<AppUserModel> _signInManager;

		public AccountController(UserManager<AppUserModel> userManager, SignInManager<AppUserModel> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Login(UserModel user)
        {
            return RedirectToAction("Index");
        }
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(UserModel user)
		{
			if (user == null)
			{
				return BadRequest("UserModel is null.");
			}

			if (ModelState.IsValid)
			{
				var newUser = new AppUserModel { UserName = user.Name, Email = user.Email };
				IdentityResult result = await _userManager.CreateAsync(newUser);

				if (result.Succeeded)
				{
					return RedirectToAction("Login");
				}

				foreach (IdentityError error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			return View(user);
		}

	}
}
