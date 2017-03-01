using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstMVC.Models
{
    public class CreditUnion
    {
        public int id { get; set; }
        public string CUName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
    }
}