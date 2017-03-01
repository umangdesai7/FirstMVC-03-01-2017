
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstMVC.Models
{
    public class Dealer
    {
        public int id { get; set; }
        public SelectList CU { get; set; }

        [Required(ErrorMessage = "Credit Union is required data.")] //give your validation message here
        [Display(Name = "Credit Union")] //Give your display name here
        public int CUId { get; set; }

        [Display(Name = "Credit Union")]
        public string CUName { get; set; }

        [Display(Name = "Dealer Name")]
        public string DealerName { get; set; }

        [Display(Name = "Dealer Info")]
        public string DealerInfo { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
    }
}