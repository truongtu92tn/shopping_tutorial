using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using shopping_tutorial.Models;

namespace shopping_tutorial.Repository
{
	public class DataContext: IdentityDbContext
	{
		public DataContext(DbContextOptions<DataContext> options):base(options) {
		}
		public DbSet<BrandModel> Brands { get; set; }
		public DbSet<ProductModel> Products { get; set; }
		public DbSet<CategoryModel> Categories { get; set; }

	}
}
