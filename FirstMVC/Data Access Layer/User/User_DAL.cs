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
    public class User_DAL : IUser_DAL
    {
        //private IDbConnection _db = new SqlConnection("Data Source=DESAIGROUP; Database= cusoweb;Integrated Security=SSPI");
        private IDbConnection _db = new SqlConnection(ConfigurationManager.AppSettings["SQLConnection"]);

        public List<User> GetAll()
        {
            List<User> UserList = this._db.Query<User>("SELECT [User].id, CASE WHEN ISNULL([User].CUId, 0) = 0 THEN CASE WHEN ISNULL([User].delId, 0) = 0 THEN 0 ELSE 2 END ELSE 1 END as usertype,  ISNULL([User].CUId, 0) as CUId, ISNULL(CreditUnion.CUName, '') as CUName, ISNULL([User].DelId, 0) as DelId, ISNULL(Dealer.DealerName, '') as DealerName, [User].UserName, [User].Password, [User].FirstName, [User].LastName, [User].Gender, [User].Phone, [User].Email, [User].Active FROM [User]LEFT JOIN CreditUnion ON [User].CUId = CreditUnion.id LEFT JOIN Dealer ON [User].DelId = Dealer.id").ToList();
            return UserList;
        }

        public User Find(int? id)
        {
            string query = "SELECT [User].id, CASE WHEN ISNULL([User].CUId, 0) = 0 THEN CASE WHEN ISNULL([User].delId, 0) = 0 THEN 0 ELSE 2 END ELSE 1 END as usertype, ISNULL([User].CUId, 0) as CUId, ISNULL(CreditUnion.CUName, '') as CUName, ISNULL([User].DelId, 0) as DelId, ISNULL(Dealer.DealerName, '') as DealerName, [User].UserName, [User].Password, [User].FirstName, [User].LastName, [User].Gender, [User].Phone, [User].Email, [User].Active FROM [User] LEFT JOIN CreditUnion ON [User].CUId = CreditUnion.id LEFT JOIN Dealer ON [User].DelId = Dealer.id WHERE [User].id = " + id + "";
            return this._db.Query<User>(query).SingleOrDefault();
        }

        public User Add(User user)
        {
            var sqlQuery = "INSERT INTO [User] (CUId, DelId, UserName, Password, FirstName, LastName, Gender, Phone, Email, Active) VALUES(@CUId, @DelId, @UserName, @Password, @FirstName, @LastName, @Gender, @Phone, @Email, 1); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var UserId = this._db.Query<int>(sqlQuery, user).Single();
            user.id = UserId;
            return user;
        }

        public User Update(User user)
        {
            var sqlQuery =
            "UPDATE [User] " +
            "SET CUId = @CUId, " +
            " DelId = @DelId, " +
            " UserName = @UserName, " +
            " Password = @Password, " +
            " FirstName = @FirstName, " +
            " LastName = @LastName, " +
            " Gender = @Gender, " +
            " Phone = @Phone, " +
            " Email = @Email " +
            "WHERE id = @id";
            this._db.Execute(sqlQuery, user);
            return user;
        }

        public void Remove(int id)
        {
            var sqlQuery = ("Delete From [User] Where id = " + id + "");
            this._db.Execute(sqlQuery);
        }

        public User IsValid(string userName, string password)
        {
            string query =
                "SELECT [User].id, ISNULL([User].CUId, 0) as CUId, ISNULL(CreditUnion.CUName, '') as CUName, ISNULL([User].DelId, 0) as DelId, ISNULL(Dealer.DealerName, '') as DealerName, [User].UserName, [User].UserName, [User].Password, [User].FirstName, [User].LastName, [User].Gender, [User].Phone, [User].Email, [User].Active FROM [User] LEFT JOIN CreditUnion ON [User].CUId = CreditUnion.id LEFT JOIN Dealer ON [User].DelId = Dealer.id WHERE [User].UserName = '" +
                userName + "' AND Password = '" + password + "'";
            User user = new User();
            user = this._db.Query<User>(query).SingleOrDefault();
            return user;

        }
    }
}