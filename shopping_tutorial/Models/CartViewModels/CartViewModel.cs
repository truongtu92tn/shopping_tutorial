namespace shopping_tutorial.Models.CartViewModels
{
	public class CartViewModel
	{
		public List<CartModel> CartItem { get; set; } = new List<CartModel>();
		//public List<CartModel> CartItem { get; set; } = new List<CartModel>(); // Khởi tạo với danh sách trống
		public decimal GrandTotal {  get; set; }

	}
}
