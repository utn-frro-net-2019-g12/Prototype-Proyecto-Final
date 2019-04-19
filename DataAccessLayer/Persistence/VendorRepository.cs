using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IEnumerable<Vendor> GetVendorsWithProducts(int id)
        {
            throw new NotImplementedException();
            //return PrototipoConsultaUTNContext.Vendors.Where(e => e.Products != null).OrderBy(e => e.Name).ToList();
        }
    }
}
