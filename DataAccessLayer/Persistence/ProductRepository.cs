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
    }
}
