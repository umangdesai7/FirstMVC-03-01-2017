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
    public class VehicleInfo_DAL : IVehicleInfo_DAL
    {
        //private IDbConnection _db = new SqlConnection("Data Source=DESAIGROUP; Database= cusoweb;Integrated Security=SSPI");
        private IDbConnection _db = new SqlConnection(ConfigurationManager.AppSettings["SQLConnection"]);

        public List<VehicleInfo> GetAll()
        {
            List<VehicleInfo> VehicleInfoList = this._db.Query<VehicleInfo>("SELECT VehicleInfo.id, VehicleInfo.CUId, CreditUnion.CUName, VehicleInfo.CustomerId, Customer.CustomerName, VehicleInfo.Year, VehicleInfo.Make, VehicleInfo.Model, VehicleInfo.VinNum, VehicleInfo.Mileage, VehicleInfo.PayOff, VehicleInfo.PerDiem, VehicleInfo.PayOffExpirationDate, VehicleInfo.Active FROM VehicleInfo, CreditUnion, Customer WHERE VehicleInfo.CUId = CreditUnion.id AND VehicleInfo.CustomerId = Customer.id").ToList();
            return VehicleInfoList;
        }

        public VehicleInfo Find(int? id)
        {
            string query = "SELECT VehicleInfo.id, VehicleInfo.CUId, CreditUnion.CUName, VehicleInfo.CustomerId, Customer.CustomerName, VehicleInfo.Year, VehicleInfo.Make, VehicleInfo.Model, VehicleInfo.VinNum, VehicleInfo.Mileage, VehicleInfo.PayOff, VehicleInfo.PerDiem, VehicleInfo.PayOffExpirationDate, VehicleInfo.Active FROM VehicleInfo, CreditUnion, Customer WHERE VehicleInfo.CUId = CreditUnion.id AND VehicleInfo.CustomerId = Customer.id AND VehicleInfo.id = " + id + "";
            return this._db.Query<VehicleInfo>(query).SingleOrDefault();
        }

        public VehicleInfo Add(VehicleInfo vehicleInfo)
        {
            var sqlQuery = "INSERT INTO VehicleInfo (CUId, CustomerId, Year, Make, Model, VinNum, Mileage, PayOff, PerDiem, PayOffExpirationDate, Active) VALUES(@CUId, @CustomerId, @Year, @Make, @Model, @VinNum, @Mileage, @PayOff, @PerDiem, @PayOffExpirationDate, 1); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var VehicleInfoId = this._db.Query<int>(sqlQuery, vehicleInfo).Single();
            vehicleInfo.id = VehicleInfoId;
            return vehicleInfo;
        }

        public VehicleInfo Update(VehicleInfo vehicleInfo)
        {
            var sqlQuery =
            "UPDATE VehicleInfo " +
            "SET CUId = @CUId, " +
            " CustomerId = @CustomerId, " +
            " Year = @Year, " +
            " Make = @Make, " +
            " Model = @Model, " +
            " VinNum = @VinNum, " +
            " Mileage = @Mileage, " +
            " PayOff = @PayOff, " +
            " PerDiem = @PerDiem, " +
            " PayOffExpirationDate = @PayOffExpirationDate " +
            "WHERE id = @id";
            this._db.Execute(sqlQuery, vehicleInfo);
            return vehicleInfo;
        }

        public void Remove(int id)
        {
            var sqlQuery = ("Delete From VehicleInfo Where id = " + id + "");
            this._db.Execute(sqlQuery);
        }
    }
}