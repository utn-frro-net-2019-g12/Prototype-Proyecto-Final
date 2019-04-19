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
    public class ProductController : ApiController
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new PrototipoConsultaUTNContext());


        // GET api/Product/
        public IHttpActionResult Get()
        {
            var products = _unitOfWork.Products.GetAll();

            return Ok(products);
        }


        // GET api/Product/5
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


        // Remember to include { Content-Type: application/json } in Request Body when consuming
        // POST api/Product/
        [ResponseType(typeof(Product))]
        public IHttpActionResult Post([FromBody] Product product)
        {
            try
            {
                _unitOfWork.Products.Add(product);
                _unitOfWork.Complete();

                return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                // Send the exception as parameter
                return BadRequest(ex.ToString());
            }

        }


        // DELETE api/Product/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult Delete(int id)
        {
            try
            {
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
        // PUT api/Product/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult Put(int id, [FromBody] Product sentProduct)
        {
            if(id != sentProduct.Id)
            {
                return BadRequest();
            }

            // This line modificates the content of the product that equals to sentProd(same id)
            //db.Entry(sentProduct).State = EntityState.Modified;
            _unitOfWork.Products.Update(sentProduct);

            try
            {
                _unitOfWork.Complete();
            }
            // Its possible that this doesn't affect any rows(that why the exception) because of some concurrrency problem or the product doesn't exist in db actually
            catch (DbUpdateConcurrencyException)
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

