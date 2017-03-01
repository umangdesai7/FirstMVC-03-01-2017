using FirstMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstMVC.Data_Access_Layer
{
    interface IVehicleInfo_DAL
    {
        List<VehicleInfo> GetAll();
        VehicleInfo Find(int? id);
        VehicleInfo Add(VehicleInfo vehicleInfo);
        VehicleInfo Update(VehicleInfo vehicleInfo);
        void Remove(int id);
    }
}
