using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ArsAdmin.Models
{
    public class RegisterPassenger
    {

        [Required(ErrorMessage = "Register Passenger Id is required.")]
        public int RegisterPassengerId { get; set; }

        
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        
        [Required(ErrorMessage = "Date Of Birth is required.")]
        public DateTime DateOfBirth { get; set; }
        
        
        [Required(ErrorMessage = "Mobile No is required.")]
        public string MobileNo { get; set; }
        
       
        [Required(ErrorMessage = "Email is required.")]
        public string EmailAddress { get; set; }

        
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        
        
        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
    }
}