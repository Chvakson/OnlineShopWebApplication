namespace OnlineShopWebApplication.Models
{
    public class Admin
    {
        public required IProductsStorage products;
        public required IOrdersStorage orders;
        public required IUsersStorage users;

    }
}
