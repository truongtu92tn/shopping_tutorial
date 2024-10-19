using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopping_tutorial.Models;
using shopping_tutorial.Repository;

namespace shopping_tutorial.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
		private readonly DataContext _dataContext;
		public BrandController(DataContext dataContext)
		{
			_dataContext = dataContext;

		}
		public async Task<IActionResult> Index()
		{

            List<BrandModel> brandModels = await _dataContext.Brands.OrderByDescending(p => p.Id).ToListAsync();
            return View(brandModels);
		}
        [HttpGet]
        public async Task<IActionResult> Create()
        {

         //   List<BrandModel> brandModels = await _dataContext.Brands.OrderByDescending(p => p.Id).ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BrandModel brandModel)
        {
            var slug = brandModel.Name.Replace(" ", "-");
            brandModel.slug = slug;
            Console.WriteLine($" name: {slug}");
            var checkSlug = await _dataContext.Brands.FirstOrDefaultAsync(b=> b.slug == slug);
            if (checkSlug != null) {
                ModelState.AddModelError(" ", "Đã có Barnd với slug này.");
                TempData["error"] = "Barnd chưa được lưu. Đã có Barnd";
                Console.WriteLine($" name: {checkSlug}");
                return View();
            }
            if (ModelState.IsValid) { 
                _dataContext.Brands.Add(brandModel);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Đã thêm thành công";
                return RedirectToAction("Index");
            }
            return View(brandModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var brandModel = await _dataContext.Brands.FirstOrDefaultAsync(b => b.Id == Id);
            return View(brandModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int Id, BrandModel brandModel)
        {
            var slug = brandModel.Name.Replace(" ", "-");
            brandModel.slug = slug;
            Console.WriteLine($" name: {slug}");
            var checkSlug = await _dataContext.Brands.FirstOrDefaultAsync(b => b.slug == slug && b.Id != Id);
            if (checkSlug != null)
            {
                ModelState.AddModelError(" ", "Đã có Barnd với slug này.");
                TempData["error"] = "Đã có Barnd với slug này. Barnd chưa được lưu.";
                //Console.WriteLine($" name: {checkSlug}");
                return View();
            }
            if (ModelState.IsValid)
            {
                _dataContext.Brands.Update(brandModel);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Đã thêm thành công";
                return RedirectToAction("Index");
            }
            return View(brandModel);
        }
		public async Task<IActionResult> Delete(int Id)
		{
            BrandModel brand = await _dataContext.Brands.FirstOrDefaultAsync(b => b.Id == Id);
			if (brand != null)
            {
				_dataContext.Brands.Remove(brand);
				await _dataContext.SaveChangesAsync();
				TempData["success"] = "Đã xóa thành công";
			}
            return RedirectToAction("Index");
		}

	}
}
