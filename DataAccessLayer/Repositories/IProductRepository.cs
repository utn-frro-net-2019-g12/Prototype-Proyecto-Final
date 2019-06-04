using System.Collections.Generic;

namespace DataAccessLayer.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductsWithMoreStock(int count);
        IEnumerable<Product> GetProductsWithVendor();
        object GetProductWithVendor(int id);
    }
}
