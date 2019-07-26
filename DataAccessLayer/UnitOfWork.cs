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

        private bool _disposed = false;

        public IProductRepository ProductsRepository { get; private set; }
        public IVendorRepository VendorsRepository { get; private set; }

        public UnitOfWork(PrototipoConsultaUTNContext context)
        {
            _context = context;
            ProductsRepository = new ProductRepository(_context);
            VendorsRepository = new VendorRepository(_context);
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

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true); 
            GC.SuppressFinalize(this);
        }
    }
}
