using DataAccessLayer.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace DataAccessLayer.Persistence
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(PrototipoConsultaUTNContext context) : base(context)
        {
        }

        public PrototipoConsultaUTNContext PrototipoConsultaUTNContext
        {
            get
            {
                return Context as PrototipoConsultaUTNContext;
            }
        }

        public IEnumerable<Product> GetProductsWithMoreStock(int count)
        {
            return PrototipoConsultaUTNContext.Products.OrderByDescending(p => p.Quantity).Take(count).ToList();

        }

        public IEnumerable<Product> GetProductsWithVendor()
        {
            return PrototipoConsultaUTNContext.Products.Include(e => e.Vendor);
        }

        public Product GetProductWithVendor(int id)
        {
            // 10-16 ms uses a more sofisticated join method
            /*var product = from p in PrototipoConsultaUTNContext.Products
                          join v in PrototipoConsultaUTNContext.Vendors on p.VendorId equals v.Id
                          where p.Id == id
                          select new
                          {
                              p.Id,
                              p.ProductName,
                              p.Quantity,
                              p.Price,
                              p.VendorId,
                              p.Vendor
                          };*/

            PrototipoConsultaUTNContext.Database.Log = message => Trace.Write(message);
            
            // 15-20 ms uses composed select
            var product = PrototipoConsultaUTNContext.Products.Include(p => p.Vendor).Where(e => e.Id == id);

            return product.FirstOrDefault();
        }
    }
}
