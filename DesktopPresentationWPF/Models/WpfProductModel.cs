using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesktopPresentationWPF.Models {
    public class WpfProductModel {
        public int Id { get; set; }

        public string ProductName { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
        public int? VendorId { get; set; }
    }
}
