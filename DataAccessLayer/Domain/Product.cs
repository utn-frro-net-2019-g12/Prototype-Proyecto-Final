using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        [MinLength(3)]
        public string ProductName { get; set; }
        public int? Quantity { get; set; }
        [Required]
        public int? Price { get; set; }

        // Use another type of LINQ query to return this product and its Vendor
        [ForeignKey("Vendor")]
        public int? VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
