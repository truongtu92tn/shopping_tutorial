namespace shopping_tutorial.Models
{
	public class CartModel
	{
		public long ProductId { get; set; }
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public decimal TotalPrice
		{
			get { return Quantity * Price; }
		}
		public string? Imager { get; set; }
		public CartModel()
		{

		}
		public CartModel(ProductModel product)
		{
			ProductId = product.Id;
			ProductName = product.Name;
			Price = (decimal)product.Price;
			Imager = product.Imager;
			Quantity = 1;


		}


	}
}
