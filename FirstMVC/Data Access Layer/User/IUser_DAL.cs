using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstMVC.Models;

namespace FirstMVC.Data_Access_Layer
{
    interface IUser_DAL
    {
        List<User> GetAll();
        User Find(int? id);
        User Add(User user);
        User Update(User user);
        void Remove(int id);
        User IsValid(string userName, string password);
    }
}
