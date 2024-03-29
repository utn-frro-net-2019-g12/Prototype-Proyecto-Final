﻿using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using DataAccessLayer;


namespace WebApi.Controllers
{
    [RoutePrefix("api/vendors")]
    public class VendorApiController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VendorApiController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        /// <summary>
        /// Retrives all product intances
        /// </summary>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var vendors = _unitOfWork.VendorsRepository.Get();

            return Ok(vendors);
        }

        [HttpGet]
        [Route("products")]
        public IHttpActionResult GetProductsWithVendor()
        {
            var vendors = _unitOfWork.VendorsRepository.GetVendorsWithProducts();

            return Ok(vendors);
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
            var vendor = _unitOfWork.VendorsRepository.GetById(id);

            if (vendor == null)
            {
                return NotFound();
            }

            return Ok(vendor);
        }

        [HttpGet]
        [Route("{id:int}/products")]
        [ResponseType(typeof(Vendor))]
        public IHttpActionResult GetProductWithVendor(int id)
        {
            var vendor = _unitOfWork.VendorsRepository.GetVendorWithProducts(id);

            if (vendor == null)
            {
                return NotFound();
            }

            return Ok(vendor);
        }


        // Remember to include { Content-Type: application/json } in Request Body when consuming
        [HttpPost]
        [Route("", Name = "postVendor")]
        [ResponseType(typeof(Vendor))]
        public IHttpActionResult Post([FromBody] Vendor vendor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _unitOfWork.VendorsRepository.Insert(vendor);
                _unitOfWork.Complete();

                return CreatedAtRoute("postVendor", new { id = vendor.Id }, vendor);
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
                var vendor = _unitOfWork.VendorsRepository.GetById(id);
                if (vendor == null)
                {
                    return NotFound();
                }

                _unitOfWork.VendorsRepository.Delete(vendor);
                _unitOfWork.Complete();

                return Ok(vendor);
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
        public IHttpActionResult Put(int id, [FromBody] Vendor sentVendor)
        {
            if (id != sentVendor.Id)
            {
                return BadRequest();
            }

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _unitOfWork.VendorsRepository.Update(sentVendor);
                _unitOfWork.Complete();
            }
            catch (Exception)
            {
                if (_unitOfWork.VendorsRepository.GetById(id) == null)
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
