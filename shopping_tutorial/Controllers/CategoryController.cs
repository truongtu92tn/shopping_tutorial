using Microsoft.AspNetCore.Mvc;
using shopping_tutorial.Models;
using shopping_tutorial.Repository;
using Microsoft.EntityFrameworkCore;

namespace shopping_tutorial.Controllers
{
	public class CategoryController : Controller
	{
		private DataContext _dataContext;
		public CategoryController(DataContext dataContext) { 
			_dataContext = dataContext;
		}
        public async Task<IActionResult> Index(string slug = "")
        {
            CategoryModel category = await _dataContext.Categories
                .FirstOrDefaultAsync(c => c.slug == slug);

            if (category == null)
            {
              
                return RedirectToAction("Index", new { slug = "" });
            }

            var productsByCat = await _dataContext.Products
                .Where(p => p.CategoryId == category.Id)
                .OrderByDescending(p => p.Id)
                .ToListAsync();

            return View(productsByCat);
        }
    }
}
