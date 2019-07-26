using System;
using DataAccessLayer.Repositories;

namespace DataAccessLayer
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductsRepository { get; }
        IVendorRepository VendorsRepository { get; }
        int Complete();
    }
}
