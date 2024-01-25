using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public class ProductsStorage : IProductsStorage
    {
        public List<Product> Products = new List<Product>
        {
            new Product("Game_1", 10, "Описание 1"),
            new Product("Game_2", 20, "Описание 2"),
            new Product("Game_3", 45, "Описание 3"),
            new Product("Game_4", 10, "Описание 1"),
            new Product("Game_5", 20, "Описание 2"),
            new Product("Game_6", 115, "Описание 3"),
            new Product("Game_7", 1000, "Описание 1"),
            new Product("Game_8", 500, "Описание 2"),
            new Product("Game_9", 8000, "Описание 3"),
            new Product("Game_10", 16, "Описание 3")
        };

        public List<Product> GetAll() {
            return Products;
        }

        public Product GetByProductId(int? productId)
        {
            return Products.FirstOrDefault(product => product.Id == productId);
        }
    }
}
