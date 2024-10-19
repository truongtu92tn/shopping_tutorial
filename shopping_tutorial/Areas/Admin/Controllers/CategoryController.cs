using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopping_tutorial.Models;
using shopping_tutorial.Repository;
using System.Text.Json;

namespace shopping_tutorial.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly DataContext _dataContext;
        public CategoryController(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        public async Task<IActionResult> Index()
        {

            var categoryModels = await _dataContext.Categories.OrderByDescending(p => p.Id).ToListAsync();
            return View(categoryModels);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

           
            return View();
        }
        [HttpPost]
       
        public async Task<IActionResult> Create(CategoryModel categoryModel)
        {
            string v = categoryModel.Name.Replace(" ", "-");
            categoryModel.slug = v;
            // In ra slug
            Console.WriteLine($"Category slug: {v}");
            
            // Kiểm tra xem slug đã tồn tại chưa
            var existingCategory = await _dataContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.slug == categoryModel.slug);
            Console.WriteLine($"Category slug: {existingCategory}");
            // Nếu tồn tại, thêm lỗi vào ModelState
            if (existingCategory != null)
            {
                ModelState.AddModelError("", "Đã có Category với slug này.");
                ViewBag.error = "Không thể thêm Category.";
            }

            // Kiểm tra ModelState trước khi thêm vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                _dataContext.Categories.Add(categoryModel);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // Trả lại view nếu có lỗi
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
          //  CategoryModel category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
           CategoryModel category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id, CategoryModel categoryModel)
        {
            if (Id != categoryModel.Id)
            {
                return BadRequest();
            }

            // Tạo slug từ tên
            string slug = categoryModel.Name.Replace(" ", "-");
            categoryModel.slug = slug;

            // In ra slug
            Console.WriteLine($"Category slug: {slug}");

            // Kiểm tra xem slug đã tồn tại chưa, ngoại trừ chính thực thể hiện tại
            var existingCategory = await _dataContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.slug == categoryModel.slug && p.Id != categoryModel.Id);

            if (existingCategory != null)
            {
                ModelState.AddModelError("", "Đã có Category với slug này.");
                ViewBag.error = "Không thể chỉnh sửa Category.";
            }

            // Kiểm tra ModelState trước khi cập nhật cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(categoryModel);
                    await _dataContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dataContext.Categories.Any(e => e.Id == categoryModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Trả lại view nếu có lỗi
            return View(categoryModel);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            CategoryModel category = await _dataContext.Categories.FirstOrDefaultAsync(c => c.Id == Id);
            if (category != null)
            {
                _dataContext.Categories.Remove(category);
                await _dataContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }


    }
}
