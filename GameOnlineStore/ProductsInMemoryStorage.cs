using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public class ProductsInMemoryStorage : IProductsStorage
    {
        public List<Product> Products = new List<Product>
        {
            new Product("Atomic Heart", 
                10, 
                "Описание 1", 
                "~/img/games/atomic_heart.jpg"),
            new Product("Cyberpunk 2077", 
                20, 
                "Описание 2", 
                "~/img/games/cyberpunk_2077.jpg"),
            new Product("Death Stranding", 
                45, 
                "Описание 3", 
                "~/img/games/death_stranding.jpg"),
            new Product("Detroit: Become Human", 
                10, 
                "Описание 1", 
                "~/img/games/detroit_become_human.jpg"),
            new Product("Divinity: Original Sin 2", 
                20, 
                "Описание 2", 
                "~/img/games/divinity_original_sin_2.jpg"),
            new Product("Elden Ring", 
                115, 
                "Описание 3", 
                "~/img/games/elden_ring.jpeg"),
            new Product("Forza Horizon 4", 
                1000, 
                "Описание 1", 
                "~/img/games/forza_horizon_4.jpg"),
            new Product("God Of War", 
                500, 
                "Описание 2", 
                "~/img/games/god_of_war.jpeg"),
            new Product("Northgard", 
                8000, 
                "Описание 3", 
                "~/img/games/northgard.jpg"),
            new Product("Red Dead Redemption 2", 
                16, 
                "Описание 3", 
                "~/img/games/red_dead_redemption_2.jpg")
        };

        public List<Product> GetAll() {
            return Products;
        }

        public Product TryGetById(int? productId)
        {
            return Products.FirstOrDefault(product => product.Id == productId);
        }
    }
}
