using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Persistence;

namespace DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PrototipoConsultaUTNContext _context;

        public IProductRepository Products { get; private set; }
        public IVendorRepository Vendors { get; private set; }

        public UnitOfWork(PrototipoConsultaUTNContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
            Vendors = new VendorRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
