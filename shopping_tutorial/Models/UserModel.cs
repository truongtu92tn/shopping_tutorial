using System.ComponentModel.DataAnnotations;

namespace shopping_tutorial.Models
{

	public class UserModel
	{
//		private const object dataType;

		public int Id { get; set; }
		[Required(ErrorMessage = "Nhập UserName")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Nhập Email"), EmailAddress]
		public string Email { get; set; }
		[DataType(DataType.Password), Required(ErrorMessage = "Nhập PassWord")]
		public string Password { get; set; }
		public string ReturnUrl { get; internal set; }
	}
}
