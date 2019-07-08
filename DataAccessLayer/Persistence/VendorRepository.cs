using DataAccessLayer.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace DataAccessLayer.Persistence
{
    public class VendorRepository : Repository<Vendor>, IVendorRepository
    {
        public VendorRepository(PrototipoConsultaUTNContext context) : base(context)
        {
        }

        public PrototipoConsultaUTNContext PrototipoConsultaUTNContext
        {
            get
            {
                return Context as PrototipoConsultaUTNContext;
            }
        }

        public IEnumerable<Vendor> GetVendorsWithProducts()
        {
            return PrototipoConsultaUTNContext.Vendors.Include(e => e.Products).OrderBy(e => e.Name).ToList();
        }

        public Vendor GetVendorWithProducts(int id)
        {
            PrototipoConsultaUTNContext.Database.Log = message => Trace.Write(message);

            return PrototipoConsultaUTNContext.Vendors.Where(e => e.Id == id).Include(e => e.Products).FirstOrDefault();
        }
    }
}
