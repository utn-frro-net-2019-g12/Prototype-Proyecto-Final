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
            var vendors = GetVendors();

            var viewModel = new CreateProductViewModel(vendors);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateProductViewModel viewModel)
        {
            var response = GlobalApi.WebApiClient.PostAsJsonAsync("products", viewModel.Product).Result;

            // Move this to an action filter
            if (!response.IsSuccessStatusCode)
            {
                var vendors = GetVendors();

                viewModel.SetVendorsAsSelectList(vendors);

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

            var vendors = GetVendors();

            var viewModel = new EditProductViewModel(vendors, product);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // TO-DO use Bind(Include = "...") is used to avoid overposting attacks
        public ActionResult Edit(EditProductViewModel viewModel)
        {
            var response = GlobalApi.WebApiClient.PutAsJsonAsync("products/" + viewModel.Product.Id, viewModel.Product).Result;

            if (!response.IsSuccessStatusCode)
            {
                var vendors = GetVendors();

                viewModel.SetVendorsAsSelectList(vendors);

                ModelStateApi.AddErrors(response, ModelState);

                return View(viewModel);
            }

            return RedirectToAction("Index");
        }

        public IEnumerable<MvcVendorModel> GetVendors()
        {
            var response = GlobalApi.WebApiClient.GetAsync("vendors").Result;

            return response.Content.ReadAsAsync<IEnumerable<MvcVendorModel>>().Result;
        }
    }
}