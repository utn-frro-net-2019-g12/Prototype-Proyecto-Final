using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Object GetProductWithVendor(int id)
        {
            // Maybe a join can solve this
            var product = from p in PrototipoConsultaUTNContext.Products
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
                          };

            return product.FirstOrDefault();
        }
    }
}
