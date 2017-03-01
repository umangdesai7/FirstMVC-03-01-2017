using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstMVC.Models
{
    public class Customer
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Credit Union is required data.")] //give your validation message here
        [Display(Name = "Credit Union")] //Give your display name here
        public int CUId { get; set; }

        [Display(Name = "Credit Union")] //Give your display name here
        public string CUName { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "CO Brrower Name")]
        public string CoBorrowerName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Social Number should not be blank.")]
        //[MinLength(4, ErrorMessage = "Minimum of 4 digit")]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Social Number must be numeric.")]
        [Display(Name = "Social Number")]
        public string CCSocialNum { get; set; }

        [Display(Name = "Driving License")]
        public string DrivingLic { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }


        //Loan Info
        [Display(Name = "Loan To Value")]
        public int LTV { get; set; }

        [Display(Name = "Rate")]
        [DataType(DataType.Currency)]
        public decimal Rate { get; set; }

        [Display(Name = "Term")]
        public string Term { get; set; }

        [Display(Name = "Approval Amount")]
        [DataType(DataType.Currency)]
        public int ApprovalAmt { get; set; }

        [Display(Name = "Pre Approval Expiration Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PreApprovExpirationDate { get; set; }

        [Display(Name = "Loan Documents")]
        public string LoanDoc { get; set; }

        [Display(Name = "Pre-Approval ID")]
        public string preapprovalid { get; set; }


        //Vehicle Info
        [Display(Name = "Year")]
        public int Year { get; set; }

        [Display(Name = "Make")]
        public string Make { get; set; }

        [Display(Name = "Model")]
        public string Model { get; set; }

        [Display(Name = "Vin Number")]
        public string VinNum { get; set; }

        [Display(Name = "Mileage")]
        public int Mileage { get; set; }

        [Display(Name = "PayOff")]
        public string PayOff { get; set; }

        [Display(Name = "PerDiem")]
        public string PerDiem { get; set; }

        [Display(Name = "PayOffExpirationDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PayOffExpirationDate { get; set; }
    }
}