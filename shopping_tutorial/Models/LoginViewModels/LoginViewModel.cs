using System.ComponentModel.DataAnnotations;

namespace shopping_tutorial.Models.LoginViewModels
{
	public class LoginViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Nhập UserName")]
		public string Name { get; set; }

		[DataType(DataType.Password), Required(ErrorMessage = "Nhập PassWord")]
		public string Password { get; set; }
		public string ReturnUrl { get; set; }
	}
}
