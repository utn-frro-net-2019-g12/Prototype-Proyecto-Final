using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;
using WebPresentationMVC.Models;
using WebPresentationMVC.ViewModels;

namespace WebPresentationMVC.Controllers
{
    public class ProductController : Controller
    {
        private IEnumerable<MvcVendorModel> vendors;

        public ProductController()
        {
            var response = GlobalApi.WebApiClient.GetAsync("vendors").Result;

            vendors = response.Content.ReadAsAsync<IEnumerable<MvcVendorModel>>().Result;
        }
  

        // GET: Product
        public ActionResult Index()
        {
            var response = GlobalApi.WebApiClient.GetAsync("products/vendor").Result;

            IEnumerable<MvcProductModel> productList = response.Content.ReadAsAsync<IEnumerable<MvcProductModel>>().Result;

            return View(productList);
        }


        public ActionResult Details(int id)
        {
            var response = GlobalApi.WebApiClient.GetAsync("products/" + id.ToString() + "/vendor").Result;

            if (!response.IsSuccessStatusCode)
            {
                return HttpNotFound();
            }

            var product = response.Content.ReadAsAsync<MvcProductModel>().Result;
    
            return View(product);
        }


        // DELETE Product/5
        public ActionResult Delete(int id)
        {
            var response = GlobalApi.WebApiClient.DeleteAsync("products/" + id.ToString()).Result;

            // Search what is TempData!
            TempData["SuccessMessage"] = "Deleted Sucessfully";

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new CreateProductViewModel(vendors, null);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(MvcProductModel product)
        {
            var response = GlobalApi.WebApiClient.PostAsJsonAsync("products", product).Result;

            var viewModel = new CreateProductViewModel(vendors, product);

            // Move this to an action filter
            if (!response.IsSuccessStatusCode)
            {
                ModelStateApi.AddErrors(response, ModelState);

                return View(viewModel);
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var response = GlobalApi.WebApiClient.GetAsync("products/" + id).Result;

            if (!response.IsSuccessStatusCode)
            {
                return HttpNotFound();
            }

            MvcProductModel product = response.Content.ReadAsAsync<MvcProductModel>().Result;

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Bind(Include = "...") is used to avoid overposting attacks
        public ActionResult Edit([Bind(Include = "Id, ProductName, Quantity, Price, VendorId")]MvcProductModel product)
        {
            var response = GlobalApi.WebApiClient.PutAsJsonAsync("products/" + product.Id, product).Result;

            if (!response.IsSuccessStatusCode)
            {
                ModelStateApi.AddErrors(response, ModelState);

                return View(product);
            }

            return RedirectToAction("Index");
        }
    }
}