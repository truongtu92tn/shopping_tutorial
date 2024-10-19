using Microsoft.AspNetCore.Mvc;
using shopping_tutorial.Repository;

namespace shopping_tutorial.Controllers
{
	
	public class ProductController : Controller
	{
		private DataContext _dataContext;
		public ProductController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
		public IActionResult Index()
		{
			return View();


		}
		public IActionResult Details(int id)
		{	
			//int id = (float)id ?? string.Empty;
			var product = _dataContext.Products.FirstOrDefault(x => x.Id == id);
			return View(product);
		}
	}
}
