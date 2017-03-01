using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstMVC.Models;

namespace FirstMVC.Data_Access_Layer
{
    interface IDealer_DAL
    {
        List<Dealer> GetAll();
        Dealer Find(int? id);
        Dealer Add(Dealer dealer);
        Dealer Update(Dealer dealer);
        void Remove(int id);
    }
}
