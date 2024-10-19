using shopping_tutorial.Repository.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shopping_tutorial.Models
{
	public class ProductModel
	{
		[Key]
		public int Id { get; set; }
		[Required, MinLength(4, ErrorMessage = "Nhập tên sản phẩm")]
		public string Name { get; set; }

        [Required, MinLength(4, ErrorMessage = "Nhập slug sản phẩm")]
        public string Slug { get; set; }
		public string? Description { get; set; }
		public string? Imager { get; set; }
		public decimal? Price { get; set; } = 0;
		public int BrandId { get; set; }
		public int CategoryId { get; set; }

		public CategoryModel? Category { get; set; }
		public BrandModel? Brand { get; set; }
			
		[NotMapped]
		[FileExtension]
        public IFormFile? ImagerUpload { get; set; }

	}
}
