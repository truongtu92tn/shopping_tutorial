using Microsoft.AspNetCore.Mvc;
using shopping_tutorial.Models;
using shopping_tutorial.Repository;
using Microsoft.EntityFrameworkCore;

namespace shopping_tutorial.Controllers
{
	public class BrandController : Controller
	{
		private DataContext _dataContext;
		public BrandController(DataContext dataContext) { 
			_dataContext = dataContext;
		}
        public async Task<IActionResult> Index(string slug = "")
        {
            BrandModel brand = await _dataContext.Brands
                .FirstOrDefaultAsync(c => c.slug == slug);

            if (brand == null)
            {
              
                return RedirectToAction("Index", new { slug = "" });
            }

            var productsByBrand = await _dataContext.Products
                .Where(p => p.BrandId == brand.Id)
                .OrderByDescending(p => p.Id)
                .ToListAsync();

            return View(productsByBrand);
        }
    }
}
