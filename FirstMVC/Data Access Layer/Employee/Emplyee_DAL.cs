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
    public class Emplyee_DAL : IEmplyee_DAL 
    {
        //private IDbConnection _db = new SqlConnection("Data Source=(Local)\\EZEENEXTGEN; Database= test;User Id=sa;Password = ;");
        //private IDbConnection _db = new SqlConnection("Data Source=DESAIGROUP; Database= cusoweb;Integrated Security=SSPI");
        private IDbConnection _db = new SqlConnection(ConfigurationManager.AppSettings["SQLConnection"]);

        public List<Employee> GetAll()
        {
            List<Employee> empList = this._db.Query<Employee>("SELECT * FROM employee").ToList();
            return empList;
        }

        public Employee Find(int? id)
        {
            string query = "SELECT * FROM employee WHERE employeeunkid = " + id + "";
            return this._db.Query<Employee>(query).SingleOrDefault();
        }

        public Employee Add(Employee employee)
        {
            var sqlQuery = "INSERT INTO employee (name, address, phone, email) VALUES(@name, @address, @phone, @email); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var employeeId = this._db.Query<int>(sqlQuery, employee).Single();
            employee.employeeunkid = employeeId;
            return employee;
        }

        public Employee Update(Employee employee)
        {
            var sqlQuery =
            "UPDATE employee " +
            "SET name = @name, " +
            " address = @address, " +
            " phone = @phone, " +
            " email = @email " +
            "WHERE employeeunkid = @employeeunkid";
            this._db.Execute(sqlQuery, employee);
            return employee;
        }

        public void Remove(int id)
        {
            var sqlQuery = ("Delete From employee Where employeeunkid = " + id + "");
            this._db.Execute(sqlQuery);
        }
    }
}