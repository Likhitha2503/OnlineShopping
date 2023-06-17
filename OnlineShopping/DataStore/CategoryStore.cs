using OnlineShopping.Models.DTO;

namespace OnlineShopping.DataStore
{
    public class CategoryStore
    {
        public static List<CategoryDto> CategoryList = new List<CategoryDto> {
            new CategoryDto{Id=1,Name="Clothing",},
            new CategoryDto{Id=2,Name="Mobiles" },
            new CategoryDto{Id=3,Name="Groceries" },
            new CategoryDto{Id=4,Name="Electronics" },
            new CategoryDto{Id=5,Name="Stationery" }
            };


    }
}
