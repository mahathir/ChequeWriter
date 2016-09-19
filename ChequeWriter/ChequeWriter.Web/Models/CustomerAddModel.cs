using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChequeWriter.Web.Models
{
    public class CustomerAddModel
    {
        [Required, MinLength(15), MaxLength(15)]
        public string CustomerNo { get; set; }
        [Required, MinLength(6), MaxLength(15)]
        public string Password { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required, MaxLength(500)]
        public string Address { get; set; }
        [Required, MaxLength(50)]
        public string ContactNo { get; set; }
        public string Status { get; set; }

        public DTO.Models.Customer ToDTO()
        {
            return new DTO.Models.Customer
            {
                CustomerNo = this.CustomerNo,
                Password = this.Password,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Address = this.Address,
                ContactNo = this.ContactNo,
                Status = this.Status
            };
        }
    }
}