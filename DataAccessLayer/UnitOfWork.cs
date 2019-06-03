using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Persistence;
using System.Data.Entity.Infrastructure;

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

        // Add DBConcurrencyException here
        public int Complete()
        {
            try
            {
                return _context.SaveChanges();
            }
            // Its possible that this doesn't affect any rows(that why the exception) because of some concurrrency problem or the product doesn't exist in db actually
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
