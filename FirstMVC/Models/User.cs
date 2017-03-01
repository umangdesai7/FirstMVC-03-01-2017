using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FirstMVC.Models
{
    public class User
    {

        public int id { get; set; }

        public int usertype { get; set; }

        [Display(Name = "Credit Union")] //Give your display name here
        public int CUId { get; set; }

        [Display(Name = "Credit Union")] //Give your display name here
        public string CUName { get; set; }

        [Display(Name = "Dealer")] //Give your display name here
        public int DelId { get; set; }

        [Display(Name = "Dealer")] //Give your display name here
        public string DealerName { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [NotMapped] // Does not effect with your database
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
    }
}