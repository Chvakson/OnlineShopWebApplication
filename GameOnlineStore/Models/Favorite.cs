namespace GameOnlineStore.Models
{
    public class Favorite
    {
        public string UserId { get; set; }
        public List<Product> FavoriteProducts { get; set; }
    }
}
