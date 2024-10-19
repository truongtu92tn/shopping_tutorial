using Microsoft.EntityFrameworkCore;
using shopping_tutorial.Models;
using System.Linq;

namespace shopping_tutorial.Repository
{
	public class SeendData
	{
		public static void seedingData(DataContext _context)
		{
			_context.Database.Migrate();

			// Thêm danh mục nếu chưa có
			if (!_context.Categories.Any())
			{
				var Macbook = new CategoryModel { Name = "Macbook", slug = "macbook", Description = "Macbook description", status = 1 };
				var pc = new CategoryModel { Name = "PC", slug = "pc", Description = "PC description", status = 1 };
				_context.Categories.AddRange(Macbook, pc);
				_context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
			}

			// Thêm thương hiệu nếu chưa có
			if (!_context.Brands.Any())
			{
				var apple = new BrandModel { Name = "Apple", Description = "Apple description", slug = "apple", status = 1 };
				var samsung = new BrandModel { Name = "Samsung", Description = "Samsung description", slug = "samsung", status = 1 };
				_context.Brands.AddRange(apple, samsung);
				_context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
			}
			// Thêm sản phẩm chỉ khi chưa có sản phẩm nào
			if (!_context.Products.Any())
			{
				var appleBrand = _context.Brands.First(b => b.Name == "Apple");
				var macbookCategory = _context.Categories.First(c => c.Name == "Macbook");
				var samsungBrand = _context.Brands.First(b => b.Name == "Samsung");
				var pcCategory = _context.Categories.First(c => c.Name == "PC");

				Console.WriteLine($"Error adding product: {appleBrand}");

				_context.Products.AddRange(
					new ProductModel { Name = "Product1", Description = "Description of product 1", Slug = "Product1", Price = 222222, Brand = appleBrand, Category = macbookCategory },
					new ProductModel { Name = "Product2", Description = "Description of product 2", Slug = "Product2", Price = 111111, Brand = samsungBrand, Category = pcCategory }
				);

				_context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
			}
			else
			{
				Console.WriteLine("produc not null");
			}

		}
	}
}
