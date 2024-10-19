using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopping_tutorial.Models;
using shopping_tutorial.Models.CartViewModels;
using shopping_tutorial.Repository;
namespace shopping_tutorial.Controllers
{
	public class CartController: Controller
	{
		private readonly DataContext _dataContext;
		public CartController(DataContext dataContext)
		{
			_dataContext = dataContext;

		}
		public IActionResult Index()
		{
			
			List<CartModel> carts = HttpContext.Session.GetJson<List<CartModel>>("Cart") ?? new List<CartModel>();
			CartViewModel cartVM = new CartViewModel
			{
				CartItem = carts,
				GrandTotal = carts.Sum(item => item.Quantity * item.Price)
			};
			//CartViewModel CartVM = new();
				return View(cartVM);
		}
		public async Task<IActionResult> Add(int id)
		{
			ProductModel productCart = await _dataContext.Products.FindAsync(id);
			List<CartModel> cart = HttpContext.Session.GetJson<List<CartModel>>("Cart") ?? new List<CartModel>();
			CartModel cartModel = cart.Where(c=>c.ProductId == id).FirstOrDefault();
			if (cartModel == null) { 
				cart.Add(new CartModel(productCart));
			}
			else
			{
				cartModel.Quantity += 1;
			}
			HttpContext.Session.SetJson("Cart", cart);
			TempData["success"] = "Add item cart success ";

            return Redirect(Request.Headers["Referer"].ToString());
		}
		public async Task<IActionResult> QuantityUp(int id)
		{
			List<CartModel> carts = HttpContext.Session.GetJson<List<CartModel>>("Cart") ?? new List<CartModel>();
			CartModel cartItem = carts.Where(c => c.ProductId == id).FirstOrDefault();
			if (cartItem != null)
			{
				if (cartItem.Quantity >= 0)
				{
					cartItem.Quantity++;
				}
				HttpContext.Session.SetJson("Cart", carts);
				TempData["success"] = "Up Quantity  item cart success ";
			}
			
			return RedirectToAction("Index");

		}
		public async Task<IActionResult> QuantityDown(int id)
		{
			List<CartModel> carts = HttpContext.Session.GetJson<List<CartModel>>("Cart") ?? new List<CartModel>();
			CartModel cartItem = carts.FirstOrDefault(c => c.ProductId == id);
			if (cartItem.Quantity > 1)
			{
				cartItem.Quantity--;
			}
			else
			{
				cartItem.Quantity = 0;
			}
			if (carts.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", carts);
				TempData["success"] = " Quantity down  item cart success ";
			}
			return RedirectToAction("Index");

		}
		public async Task<IActionResult> QuantityDelete(int id)
		{
			List<CartModel> carts = HttpContext.Session.GetJson<List<CartModel>>("Cart") ?? new List<CartModel>();
			CartModel cartItem = carts.Where(c => c.ProductId == id).FirstOrDefault();
			if (cartItem != null)
			{
				carts.Remove(cartItem);
				HttpContext.Session.SetJson("Cart", carts);
				TempData["success"] = "Delete  item cart success ";
			}

			return RedirectToAction("Index");
		}

	}
}
