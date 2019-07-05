using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using DataAccessLayer;


namespace WebApi.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductApiController : ApiController
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new PrototipoConsultaUTNContext());


        /// <summary>
        /// Retrives all product intances
        /// </summary>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var products = _unitOfWork.Products.GetAll();

            return Ok(products);
        }

        [HttpGet]
        [Route("vendor")]
        public IHttpActionResult GetProductsWithVendor()
        {
            var products = _unitOfWork.Products.GetProductsWithVendor();

            return Ok(products);
        }


        // GET api/Product/5
        /// <summary>
        /// Retrives an specific product
        /// </summary>
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult Get(int id)
        {
            var product = _unitOfWork.Products.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet]
        [Route("{id:int}/vendor")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProductWithVendor(int id)
        {
            var product = _unitOfWork.Products.GetProductWithVendor(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }


        // Remember to include { Content-Type: application/json } in Request Body when consuming
        [HttpPost]
        [Route("", Name = "name")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult Post([FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _unitOfWork.Products.Add(product);
                _unitOfWork.Complete();

                return CreatedAtRoute("name", new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                // Send the exception as parameter
                return BadRequest(ex.ToString());
            }

        }


        [Authorize]
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                // TO-DO: return errors in http format
                var product = _unitOfWork.Products.Get(id);
                if (product == null)
                {
                    return NotFound();
                }

                _unitOfWork.Products.Remove(product);
                _unitOfWork.Complete();

                return Ok(product);
            }
            catch (Exception ex)
            {
                // Send the exception as parameter
                return BadRequest(ex.ToString());
            }
        }

        // Remember to include { Content-Type: application/json } and state the ProductId in in Request Body when consuming
        // The Query String id (api/product/{id}) has to match ProductId from Request Body
        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult Put(int id, [FromBody] Product sentProduct)
        {
            if (id != sentProduct.Id)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.Products.Update(sentProduct);

                _unitOfWork.Complete();
            }
            catch(Exception)
            {
                if (_unitOfWork.Products.Get(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                } 
            }
            

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
