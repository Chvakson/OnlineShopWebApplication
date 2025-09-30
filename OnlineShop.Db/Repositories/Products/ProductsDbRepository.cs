using GameOnlineStore.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace GameOnlineStore.Db.Repositories.Products
{
    public class ProductsDbRepository : IProductsDbRepository
    {
        private readonly ApplicationContext context;

        public ProductsDbRepository(ApplicationContext context)
        {
            this.context = context;
        }


        public List<Product> GetAll()
        {
            return context.Products.ToList();
        }

        public Product TryGetById(Guid? productId)
        {
            return context.Products.FirstOrDefault(product => product.Id == productId);
        }

        public void Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void Remove(Guid productId)
        {
            var exisingProduct = TryGetById(productId);
            if (exisingProduct == null)
            {
                return;
            }
            context.Products.Remove(exisingProduct);
            context.SaveChanges();
        }

        public void Update(Product product)
        {
            var exisingProduct = TryGetById(product.Id);
            if (exisingProduct == null)
            {
                return;
            }
            exisingProduct.Name = product.Name;
            exisingProduct.Cost = product.Cost;
            exisingProduct.Description = product.Description;
            exisingProduct.ImgFileName = product.ImgFileName;
            context.SaveChanges();
        }
    }
}
