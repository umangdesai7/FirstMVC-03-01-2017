using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FirstMVC.Models
{
    public class LoanInfo
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Credit Union is required data.")] //give your validation message here
        [Display(Name = "Credit Union")] //Give your display name here
        public int CUId { get; set; }

        [Display(Name = "Credit Union")] //Give your display name here
        public string CUName { get; set; }

        [Required(ErrorMessage = "Customer is required data.")] //give your validation message here
        [Display(Name = "Customer")] //Give your display name here
        public int CustomerId { get; set; }

        [Display(Name = "Customer")] //Give your display name here
        public string CustomerName { get; set; }

        [Display(Name = "Loan To Value")]
        public int LTV { get; set; }

        [Display(Name = "Rate")]
        public decimal Rate { get; set; }

        [Display(Name = "Term")]
        public string Term { get; set; }

        [Display(Name = "Approval Amount")]
        public int ApprovalAmt { get; set; }

        [Display(Name = "Pre Approval Expiration Date")]
        public DateTime PreApprovExpirationDate { get; set; }

        [Display(Name = "Loan Documents")]
        public string LoanDoc { get; set; }


    }
}