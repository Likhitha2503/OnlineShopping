using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models.DTO
{
    public class CategoryDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
