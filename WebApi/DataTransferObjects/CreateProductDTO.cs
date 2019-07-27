using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.DataTransferObjects
{
    public class CreateProductDTO
    {
        [Required]
        [StringLength(15)]
        [MinLength(3)]
        public string ProductName { get; set; }
        public int? Quantity { get; set; }
        [Required]
        public int? Price { get; set; }
        public int? VendorId { get; set; }
    }
}