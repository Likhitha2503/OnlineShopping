using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models.DTO
{
    public class ProductDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }


        public string Description { get; set; }

        //public Category Category { get; set; }
        //public  int CategoryId { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int AvailableQuantity { get; set; }

        public string ImageUrl { get; set; }
    }
}
