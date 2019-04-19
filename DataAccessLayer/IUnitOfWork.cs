using System;
using DataAccessLayer.Repositories;

namespace DataAccessLayer
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IVendorRepository Vendors { get; }
        int Complete();
    }
}
