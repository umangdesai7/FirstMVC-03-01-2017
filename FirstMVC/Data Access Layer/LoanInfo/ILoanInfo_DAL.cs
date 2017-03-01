using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstMVC.Models;

namespace FirstMVC.Data_Access_Layer
{
    interface ILoanInfo_DAL
    {
        List<LoanInfo> GetAll();
        LoanInfo Find(int? id);
        LoanInfo Add(LoanInfo user);
        LoanInfo Update(LoanInfo loaninfo);
        void Remove(int id);
    }
}
