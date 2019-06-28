using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPresentationMVC.Models
{
    public class MvcProductModel
    {
        public MvcProductModel()
        {
            Vendors = new HashSet<MvcVendorModel>();
        }
        public int Id { get; set; }

        public string ProductName { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
        public int? VendorId { get; set; }

        public virtual ICollection<MvcVendorModel> Vendors { get; set; }
    }
}