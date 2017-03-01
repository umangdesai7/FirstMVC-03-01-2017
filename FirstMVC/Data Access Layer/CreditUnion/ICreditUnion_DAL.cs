using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstMVC.Models;

namespace FirstMVC.Data_Access_Layer
{
    interface ICreditUnion_DAL
    {
        List<CreditUnion> GetAll();
        CreditUnion Find(int? id);
        CreditUnion Add(CreditUnion creditunion);
        CreditUnion Update(CreditUnion creditunion);
        void Remove(int id);
    }
}
