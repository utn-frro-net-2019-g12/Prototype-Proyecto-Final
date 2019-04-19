using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PrototipoConsultaUTNInitializer : System.Data.Entity.DropCreateDatabaseAlways<PrototipoConsultaUTNContext>
    {
        protected override void Seed(PrototipoConsultaUTNContext context)
        {
            var unitOfWork = new UnitOfWork(new PrototipoConsultaUTNContext());

            var products = new List<Product>
            {
                new Product{ ProductName = "Queso", Quantity = 2, Price = 160},
                new Product { ProductName = "Leche", Quantity = 5, Price = 50},
                new Product{ ProductName = "Dulce de Leche", Quantity = 1, Price = 35}
            };

            unitOfWork.Products.AddRange(products);

            var vendors = new List<Vendor>
            {
                new Vendor{Name = "Serenisima", Adress = "Adroge 1545, Santa Teresita"}
            };

            unitOfWork.Vendors.AddRange(vendors);

            unitOfWork.Complete();
        }
    }
}
