using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;
using WebPresentationMVC.Models;

namespace WebPresentationMVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var response = GlobalVariables.WebApiClient.GetAsync("products/vendor").Result;

            IEnumerable<MvcProductModel> productList = response.Content.ReadAsAsync<IEnumerable<MvcProductModel>>().Result;

            return View(productList);
        }


        public ActionResult Details(int id)
        {
            var response = GlobalVariables.WebApiClient.GetAsync("products/" + id.ToString() + "/vendor").Result;

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
            var response = GlobalVariables.WebApiClient.DeleteAsync("products/" + id.ToString()).Result;

            // Search what is TempData!
            TempData["SuccessMessage"] = "Deleted Sucessfully";

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MvcProductModel product)
        {
            var response = GlobalVariables.WebApiClient.PostAsJsonAsync("products", product).Result;

            if (!response.IsSuccessStatusCode)
            {
                var modelState = response.Content.ReadAsAsync<ModelState>().Result;

                

                return View(product);
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

            var response = GlobalVariables.WebApiClient.GetAsync("products/" + id).Result;

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
            var response = GlobalVariables.WebApiClient.PutAsJsonAsync("products/" + product.Id, product).Result;

            if (!response.IsSuccessStatusCode)
            {
                var modelState = response.Content.ReadAsAsync<ModelState>().Result;

                return View(product);
            }

            return RedirectToAction("Index");
        }
    }
}