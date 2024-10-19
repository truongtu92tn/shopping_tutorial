using System.ComponentModel.DataAnnotations;

namespace shopping_tutorial.Models
{
	public class CategoryModel
	{
		[Key]
		public int Id { get; set; }
		[Required, MinLength (4,ErrorMessage ="Nhập tên danh mục")]
		public string Name { get; set; }
		public string? Description { get; set; }
		[Required,  MinLength(4, ErrorMessage = "Nhập tên danh mục")]
		public string slug { get; set; }
		public int status { get; set; }
	}
}
