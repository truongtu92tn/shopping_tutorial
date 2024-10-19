using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using shopping_tutorial.Models;
using shopping_tutorial.Repository;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace shopping_tutorial.Areas.Admin.Cotrollers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private DataContext _dataContext;
        public ProductController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IActionResult> Index()
        {

            var Product = await _dataContext.Products.OrderByDescending(p => p.Id).Include(p => p.Category).Include(p => p.Brand).ToListAsync();
            return View(Product);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Category = new SelectList(_dataContext.Categories, "Id", "Name");
            ViewBag.Brand = new SelectList(_dataContext.Brands, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel product)
        {
            // Tạo danh sách cho SelectList
            ViewBag.Category = new SelectList(_dataContext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brand = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandId);

            string v = product.Name.Replace(" ", "-");
            product.Slug = v;
            var slug = await _dataContext.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);
            if (slug != null)
            {
                ModelState.AddModelError("Đã", "Đã có Sản phẩm .");
                ViewBag.error = "Không thể thêm Product. ";
            }
            // Kiểm tra xem BrandId và CategoryId có tồn tại không
            if (product.BrandId == 0 || product.CategoryId == 0)
            {
                ModelState.AddModelError("", "BrandId and CategoryId are required.");
                ViewBag.error = "Không thể thêm Product";
                return View(product);
            }
            // Xử lý tệp hình ảnh
            if (product.ImagerUpload != null && product.ImagerUpload.Length > 0)
            {
                var fileName = Path.GetFileName(product.ImagerUpload.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ImagerUpload.CopyToAsync(stream);
                }

                product.Imager = $"images/{fileName}"; // Lưu đường dẫn hình ảnh
            }
            if (ModelState.IsValid)
            {

                _dataContext.Products.Add(product);
                await _dataContext.SaveChangesAsync();

                TempData["success"] = "Đã thêm thành công";
                return RedirectToAction("Index");
            }
            // Nếu ModelState không hợp lệ, thêm lỗi vào ModelState
            foreach (var entry in ModelState)
            {
                var errors = entry.Value.Errors; // Lấy danh sách lỗi từ ModelStateEntry
                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
            }
            ViewBag.error = "Không thể thêm Product";
            return View(product);
        }
        public async Task<IActionResult> Edit(int id)
        {
            ProductModel product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.Category = new SelectList(_dataContext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brand = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandId);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, ProductModel product)
        {
            // Tạo danh sách cho SelectList
            ViewBag.Category = new SelectList(_dataContext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brand = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandId);

            string v = product.Name.Replace(" ", "-");
            product.Slug = v;
            var productEntity = await _dataContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Slug == product.Slug);
            if (productEntity != null && productEntity.Id != product.Id)
            {
                ModelState.AddModelError("", "Đã có Sản phẩm .");
                ViewBag.error = "Không thể cập nhật Product. ";
            }
            // Kiểm tra xem BrandId và CategoryId có tồn tại không
            if (product.BrandId == 0 || product.CategoryId == 0)
            {
                ModelState.AddModelError("", "BrandId and CategoryId are required.");
                ViewBag.error = "Không thể Cập nhật Product";
                return View(product);
            }
            // Xử lý tệp hình ảnh
            if (product.ImagerUpload != null && product.ImagerUpload.Length > 0)
            {
                var fileName = Path.GetFileName(product.ImagerUpload.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ImagerUpload.CopyToAsync(stream);
                }

                product.Imager = $"images/{fileName}"; // Lưu đường dẫn hình ảnh
            }
            if (ModelState.IsValid)
            {

                _dataContext.Products.Update(product);
                await _dataContext.SaveChangesAsync();

                TempData["success"] = "Cập nhật sản phẩm thành công";
                return RedirectToAction("Edit");
            }
            // Nếu ModelState không hợp lệ, thêm lỗi vào ModelState
            foreach (var entry in ModelState)
            {
                var errors = entry.Value.Errors; // Lấy danh sách lỗi từ ModelStateEntry
                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
            }
            ViewBag.error = "Không thể Cập nhật Product";
            return View(product);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            ProductModel product = await _dataContext.Products.FindAsync(Id);
            if (product == null)
            {
                ViewBag.error = "Không tìm thấy sản phẩm Product";
            }
            else
            {
                _dataContext.Products.Remove(product);
                await _dataContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

    }
}
