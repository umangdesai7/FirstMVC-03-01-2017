using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstMVC.Models;

namespace FirstMVC.Data_Access_Layer
{
    interface IEmplyee_DAL
    {
        List<Employee> GetAll();
        Employee Find(int? id);
        Employee Add(Employee employee);
        Employee Update(Employee employee);
        void Remove(int id);
    }
}
