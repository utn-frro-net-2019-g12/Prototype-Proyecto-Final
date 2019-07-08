using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPresentationMVC.Models;

namespace WebPresentationMVC.ViewModels
{
    public class EditProductViewModel
    {
        public EditProductViewModel() { }

        public EditProductViewModel(IEnumerable<MvcVendorModel> vendors, MvcProductModel product)
        {
            this.SetVendorsAsSelectList(vendors);

            this.Product = product;
        }

        public MvcProductModel Product { get; set; }
        public int? VendorId { get; set; }
        public IEnumerable<SelectListItem> VendorsList { get; set; }
        public void SetVendorsAsSelectList(IEnumerable<MvcVendorModel> vendors)
        {
            VendorsList = vendors.Select(e => new SelectListItem()
            {
                Value = e.Id.ToString(),
                Text = e.Name
            }) as IEnumerable<SelectListItem>;
        }
    }
}