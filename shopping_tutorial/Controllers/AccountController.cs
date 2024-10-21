using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopping_tutorial.Models;
using shopping_tutorial.Models.LoginViewModels;
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

		public IActionResult Login(string ReturnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = ReturnUrl});
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(UserModel user)
        {
			if (ModelState.IsValid)
			{
				var userLogin = new AppUserModel { UserName = user.Name, PasswordHash = user.Password };
				Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user.Name, user.Password, false, false);
				if (result.Succeeded)
				{
					TempData["success"] = "Login user thành công";
					return View(result);
					//return Redirect(user.ReturnUrl ?? "/");
				}
				ModelState.AddModelError("", "Username và Password không đúng");
				
			}
				return View(user);
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
					TempData["success"] = "Tạo user thành công";
					return RedirectToAction("Login");
				}

				foreach (IdentityError error in result.Errors)
				{
					TempData["error"] = $"Tạo user không thành công - {error.Description} ";
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			return View(user);
		}



			
	}
}
