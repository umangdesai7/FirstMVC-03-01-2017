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
    public class LoanInfo_DAL : ILoanInfo_DAL
    {
        //private IDbConnection _db = new SqlConnection("Data Source=DESAIGROUP; Database= cusoweb;Integrated Security=SSPI");
        private IDbConnection _db = new SqlConnection(ConfigurationManager.AppSettings["SQLConnection"]);

        public List<LoanInfo> GetAll()
        {
            List<LoanInfo> LoanInfoList = this._db.Query<LoanInfo>("SELECT LoanInfo.id, LoanInfo.CUId, CreditUnion.CUName, LoanInfo.CustomerId, Customer.CustomerName, LoanInfo.LTV, LoanInfo.Rate, LoanInfo.Term, LoanInfo.ApprovalAmt, LoanInfo.PreApprovExpirationDate, LoanInfo.LoanDoc FROM LoanInfo, CreditUnion, Customer WHERE LoanInfo.CUId = CreditUnion.id AND LoanInfo.CustomerId = Customer.id").ToList();
            return LoanInfoList;
        }

        public LoanInfo Find(int? id)
        {
            string query = "SELECT LoanInfo.id, LoanInfo.CUId, CreditUnion.CUName, LoanInfo.CustomerId, Customer.CustomerName, LoanInfo.LTV, LoanInfo.Rate, LoanInfo.Term, LoanInfo.ApprovalAmt, LoanInfo.PreApprovExpirationDate, LoanInfo.LoanDoc FROM LoanInfo, CreditUnion, Customer WHERE LoanInfo.CUId = CreditUnion.id AND LoanInfo.CustomerId = Customer.id AND LoanInfo.id = " + id + "";
            return this._db.Query<LoanInfo>(query).SingleOrDefault();
        }

        public LoanInfo Add(LoanInfo loaninfo)
        {
            var sqlQuery = "INSERT INTO LoanInfo (CUId, CustomerId, LTV, Rate, Term, ApprovalAmt, PreApprovExpirationDate, LoanDoc, Active) VALUES(@CUId, @CustomerId, @LTV, @Rate, @Term, @ApprovalAmt, @PreApprovExpirationDate, @LoanDoc, 1); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var LoanInfoId = this._db.Query<int>(sqlQuery, loaninfo).Single();
            loaninfo.id = LoanInfoId;
            return loaninfo;
        }

        public LoanInfo Update(LoanInfo loaninfo)
        {
            var sqlQuery =
            "UPDATE LoanInfo " +
            "SET CUId = @CUId, " +
            " CustomerId = @CustomerId, " +
            " LTV = @LTV, " +
            " Rate = @Rate, " +
            " Term = @Term, " +
            " ApprovalAmt = @ApprovalAmt, " +
            " PreApprovExpirationDate = @PreApprovExpirationDate, " +
            " LoanDoc = @LoanDoc " +
            "WHERE id = @id";
            this._db.Execute(sqlQuery, loaninfo);
            return loaninfo;
        }

        public void Remove(int id)
        {
            var sqlQuery = ("Delete From LoanInfo Where id = " + id + "");
            this._db.Execute(sqlQuery);
        }
    }
}