using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstMVC.Models;

namespace FirstMVC.Data_Access_Layer
{
    interface ICustomer_DAL
    {
        List<Customer> GetAll(string cuid);
        Customer Find(int? id);
        Customer Add(Customer customer);
        Customer Update(Customer customer);
        void Remove(int id);

        string getPreAprrovalID();
    }
}
