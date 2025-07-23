using GameOnlineStore.Models;

namespace GameOnlineStore.Repositories.CompareProducts
{
    public interface ICompareProducts
    {
        public ProductsCollection TryGetByUserId(string userId);
        public void Add(Product product);
        public void Remove(Product product);
    }
}