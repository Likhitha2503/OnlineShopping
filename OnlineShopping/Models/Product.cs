using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopping.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
           

        public  string Description { get; set; }

        public Category Category { get; set; }
       
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [Required]
        public  float Price { get; set; }
        [Required]
        public int AvailableQuantity { get; set;}

        public  string  ImageUrl { get; set; }


    }

}
