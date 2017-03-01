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
    public class CreditUnion_DAL : ICreditUnion_DAL
    {
        //private IDbConnection _db = new SqlConnection("Data Source=DESAIGROUP; Database= cusoweb;Integrated Security=SSPI");
        private IDbConnection _db = new SqlConnection(ConfigurationManager.AppSettings["SQLConnection"]);

        public List<CreditUnion> GetAll()
        {
            List<CreditUnion> empList = this._db.Query<CreditUnion>("SELECT id, CUName, Address, Phone, Fax, Email, Active FROM CreditUnion").ToList();
            return empList;
        }

        public CreditUnion Find(int? id)
        {
            string query = "SELECT id, CUName, Address, Phone, Fax, Email, Active FROM CreditUnion WHERE id = " + id + "";
            return this._db.Query<CreditUnion>(query).SingleOrDefault();
        }

        public CreditUnion Add(CreditUnion creditunion)
        {
            var sqlQuery = "INSERT INTO CreditUnion (CUName, Address, Phone, Fax, Email, Active) VALUES(@CUName, @Address, @Phone, @Fax, @Email, 1); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var CreditUnionId = this._db.Query<int>(sqlQuery, creditunion).Single();
            creditunion.id = CreditUnionId;
            return creditunion;
        }

        public CreditUnion Update(CreditUnion creditunion)
        {
            var sqlQuery =
            "UPDATE CreditUnion " +
            "SET CUName = @CUName, " +
            " Address = @Address, " +
            " Phone = @Phone, " +
            " Fax = @Fax, " +
            " Email = @Email " +
            "WHERE id = @id";
            this._db.Execute(sqlQuery, creditunion);
            return creditunion;
        }

        public void Remove(int id)
        {
            var sqlQuery = ("Delete From CreditUnion Where id = " + id + "");
            this._db.Execute(sqlQuery);
        }
    }
}