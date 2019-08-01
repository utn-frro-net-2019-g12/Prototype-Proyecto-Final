using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebPresentationMVC.Models
{
    public class MvcProductModel
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }
        public int? Quantity { get; set; }
        [Required]
        public int? Price { get; set; }
        [Display(Name = "Vendor")]
        public int? VendorId { get; set; }

        public virtual MvcVendorModel Vendor { get; set; }
    }
}