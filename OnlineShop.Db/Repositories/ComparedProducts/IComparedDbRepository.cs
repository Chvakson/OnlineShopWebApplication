using GameOnlineStore.Db.Models;

namespace GameOnlineStore.Repositories.ComparedProducts
{
    public interface IComparedDbRepository
    {
        void Add(string userId, Product product);
        void Remove(string userId, Guid productId);
        List<Product> GetAll(string userId);
        void Clear(string userId);
    }
}