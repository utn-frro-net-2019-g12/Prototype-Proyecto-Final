using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

            var product = response.Content.ReadAsAsync<MvcProductModel>().Result;
    
            return View(product);
        }


        // DELETE Product/5
        public ActionResult Delete(int id)
        {
            var response = GlobalVariables.WebApiClient.DeleteAsync("Products/" + id.ToString()).Result;

            // Search what is TempData!
            TempData["SuccessMessage"] = "Deleted Sucessfully";

            return RedirectToAction("Index");
        }
    }
}