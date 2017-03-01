using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using FirstMVC.Models;
using Dapper;
using System.Configuration;
using System.Web.UI.WebControls;

namespace FirstMVC.Data_Access_Layer
{
    public class Customer_DAL : ICustomer_DAL
    {
        //private IDbConnection _db = new SqlConnection("Data Source=DESAIGROUP; Database= cusoweb;Integrated Security=SSPI");
        private IDbConnection _db = new SqlConnection(ConfigurationManager.AppSettings["SQLConnection"]);

        public List<Customer> GetAll(string cuid)
        {
            string strQ;
            strQ ="SELECT Customer.id, Customer.CUId, CreditUnion.CUName, Customer.CustomerName, Customer.CoBorrowerName, Customer.FirstName, Customer.LastName, Customer.CCSocialNum, Customer.DrivingLic, Customer.DOB, Customer.Address, Customer.Phone, Customer.Email, Customer.Active " +
                            ",LoanInfo.LTV, LoanInfo.Rate, LoanInfo.Term, LoanInfo.ApprovalAmt, LoanInfo.PreApprovExpirationDate, LoanInfo.LoanDoc, LoanInfo.preapprovalid " +
                            ",VehicleInfo.Year, VehicleInfo.Make, VehicleInfo.Model, VehicleInfo.VinNum, VehicleInfo.Mileage, VehicleInfo.PayOff, VehicleInfo.PerDiem, VehicleInfo.PayOffExpirationDate " +
                        "FROM Customer" +
                            " LEFT OUTER JOIN LoanInfo ON Customer.id = LoanInfo.customerid " +
                            " LEFT OUTER JOIN VehicleInfo ON Customer.id = VehicleInfo.customerid " +
                        ", CreditUnion " +
                        "WHERE Customer.CUId = CreditUnion.id ";
            if (!string.IsNullOrEmpty(cuid))
            {
                strQ += " AND Customer.CUId = " + cuid + "";
            }
            
            List<Customer> CustomerList = this._db.Query<Customer>(strQ).ToList();
            return CustomerList;
        }

        public Customer Find(int? id)
        {
            string query = "SELECT Customer.id, Customer.CUId, CreditUnion.CUName, Customer.CustomerName, Customer.CoBorrowerName, Customer.FirstName, Customer.LastName, Customer.CCSocialNum, Customer.DrivingLic, Customer.DOB, Customer.Address, Customer.Phone, Customer.Email, Customer.Active " +
                                    ",LoanInfo.LTV, LoanInfo.Rate, LoanInfo.Term, LoanInfo.ApprovalAmt, LoanInfo.PreApprovExpirationDate, LoanInfo.LoanDoc, LoanInfo.preapprovalid " +
                                    ",VehicleInfo.Year, VehicleInfo.Make, VehicleInfo.Model, VehicleInfo.VinNum, VehicleInfo.Mileage, VehicleInfo.PayOff, VehicleInfo.PerDiem, VehicleInfo.PayOffExpirationDate " +
                                "FROM Customer" +
                                    " LEFT OUTER JOIN LoanInfo ON Customer.id = LoanInfo.customerid " +
                                    " LEFT OUTER JOIN VehicleInfo ON Customer.id = VehicleInfo.customerid " +
                                    ", CreditUnion " +
                                "WHERE Customer.CUId = CreditUnion.id " + 
                                "AND Customer.id = " + id + "";
            return this._db.Query<Customer>(query).SingleOrDefault();
        }

        public Customer Add(Customer customer)
        {
            var sqlQuery = "INSERT INTO Customer (CUId, CustomerName, CoBorrowerName, FirstName, LastName, CCSocialNum, DrivingLic, DOB, Address, Phone, Email, Active) VALUES(@CUId, @CustomerName, @CoBorrowerName, @FirstName, @LastName, @CCSocialNum, @DrivingLic, @DOB, @Address, @Phone, @Email, 1); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var CustomerId = this._db.Query<int>(sqlQuery, customer).Single();
            customer.id = CustomerId;

            sqlQuery = "INSERT INTO LoanInfo (CUId, CustomerId, LTV, Rate, Term, ApprovalAmt, PreApprovExpirationDate, LoanDoc, Active, preapprovalid) VALUES(@CUId, " + CustomerId + ", @LTV, @Rate, @Term, @ApprovalAmt, @PreApprovExpirationDate, @LoanDoc, 1, @preapprovalid); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var LoanId = this._db.Query<int>(sqlQuery, customer).Single();

            sqlQuery = "INSERT INTO VehicleInfo (CUId, CustomerId, Year, Make, Model, VinNum, Mileage, PayOff, PerDiem, PayOffExpirationDate, Active) VALUES(@CUId, " + CustomerId + ", @Year, @Make, @Model, @VinNum, @Mileage, @PayOff, @PerDiem, @PayOffExpirationDate, 1); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var VehicleInfoId = this._db.Query<int>(sqlQuery, customer).Single();

            return customer;
        }

        public Customer Update(Customer customer)
        {
            var sqlQuery =
            "UPDATE Customer " +
            "SET CUId = @CUId, " +
            " CustomerName = @CustomerName, " +
            " CoBorrowerName = @CoBorrowerName, " +
            " FirstName = @FirstName, " +
            " LastName = @LastName, " +
            " CCSocialNum = @CCSocialNum, " +
            " DrivingLic = @DrivingLic, " +
            " DOB = @DOB, " +
            " Address = @Address, " +
            " Phone = @Phone, " +
            " Email = @Email " +
            "WHERE id = @id";
            this._db.Execute(sqlQuery, customer);

            sqlQuery =
            "UPDATE LoanInfo " +
            "SET CUId = @CUId, " +
            " CustomerId = @id, " +
            " LTV = @LTV, " +
            " Rate = @Rate, " +
            " Term = @Term, " +
            " ApprovalAmt = @ApprovalAmt, " +
            " PreApprovExpirationDate = @PreApprovExpirationDate, " +
            " LoanDoc = @LoanDoc " +
            "WHERE customerid = @id";
            this._db.Execute(sqlQuery, customer);

            sqlQuery =
            "UPDATE VehicleInfo " +
            "SET CUId = @CUId, " +
            " CustomerId = @id, " +
            " Year = @Year, " +
            " Make = @Make, " +
            " Model = @Model, " +
            " VinNum = @VinNum, " +
            " Mileage = @Mileage, " +
            " PayOff = @PayOff, " +
            " PerDiem = @PerDiem, " +
            " PayOffExpirationDate = @PayOffExpirationDate " +
            "WHERE customerid = @id";
            this._db.Execute(sqlQuery, customer);

            return customer;
        }

        public void Remove(int id)
        {
            var sqlQuery = ("Delete From VehicleInfo Where customerid = " + id + "");
            this._db.Execute(sqlQuery);

            sqlQuery = ("Delete From LoanInfo Where customerid = " + id + "");
            this._db.Execute(sqlQuery);

            sqlQuery = ("Delete From Customer Where id = " + id + "");
            this._db.Execute(sqlQuery);
        }

        public string getPreAprrovalID()
        {
            string query = "SELECT dbo.GetPreApprovalId() as id";
            return this._db.Query<string>(query).SingleOrDefault();
        }
    }
}