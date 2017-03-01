using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstMVC.Models
{
    public class VehicleInfo
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
        public DateTime PayOffExpirationDate { get; set; }
    }
}