using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPresentationMVC.Models
{
    public class MvcVendorModel
    {
        public MvcVendorModel()
        {
            //Products = new HashSet<MvcProductModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public string Adress { get; set; }

        // TO-DO: Fix circular reference
        //public virtual ICollection<MvcProductModel> Products { get; set; }
    }
}