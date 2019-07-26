using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PrototipoConsultaUTNContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }


        public PrototipoConsultaUTNContext() : base("name=PrototipoConsultaUTNContext")
        {
            Database.SetInitializer<PrototipoConsultaUTNContext>(new PrototipoConsultaUTNInitializer());
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
    }
}
