using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using FirstMVC.Models;
using Dapper;
using System.Configuration;

namespace FirstMVC.Data_Access_Layer
{
    public class Dealer_DAL : IDealer_DAL
    {
        //private IDbConnection _db = new SqlConnection("Data Source=DESAIGROUP; Database= cusoweb;Integrated Security=SSPI");
        private IDbConnection _db = new SqlConnection(ConfigurationManager.AppSettings["SQLConnection"]);

        public List<Dealer> GetAll()
        {
            List<Dealer> dealerList = this._db.Query<Dealer>("SELECT Dealer.id, Dealer.CUId, CreditUnion.CUName, Dealer.DealerName, Dealer.DealerInfo, Dealer.Address, Dealer.Phone, Dealer.Fax, Dealer.Email, Dealer.Active FROM Dealer, CreditUnion WHERE Dealer.CUId = CreditUnion.id").ToList();
            return dealerList;
        }

        public Dealer Find(int? id)
        {
            string query = "SELECT  Dealer.id, Dealer.CUId, CreditUnion.CUName, Dealer.DealerName, Dealer.DealerInfo, Dealer.Address, Dealer.Phone, Dealer.Fax, Dealer.Email, Dealer.Active FROM Dealer, CreditUnion WHERE Dealer.CUId = CreditUnion.id AND Dealer.id = " + id + "";
            return this._db.Query<Dealer>(query).SingleOrDefault();
        }

        public Dealer Add(Dealer dealer)
        {
            var sqlQuery = "INSERT INTO Dealer (CUId, DealerName, DealerInfo, Address, Phone, Fax, Email, Active) VALUES(1, @DealerName, @DealerInfo, @Address, @Phone, @Fax, @Email, 1); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var DealerId = this._db.Query<int>(sqlQuery, dealer).Single();
            dealer.id = DealerId;
            return dealer;
        }

        public Dealer Update(Dealer dealer)
        {
            var sqlQuery =
            "UPDATE Dealer " +
            "SET DealerName = @DealerName, " +
            " DealerInfo = @DealerInfo, " +
            " Address = @Address, " +
            " Phone = @Phone, " +
            " Fax = @Fax, " +
            " Email = @Email " +
            "WHERE id = @id";
            this._db.Execute(sqlQuery, dealer);
            return dealer;
        }

        public void Remove(int id)
        {
            var sqlQuery = ("Delete From Dealer Where id = " + id + "");
            this._db.Execute(sqlQuery);
        }
    }
}