using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstMVC.Models;
using FirstMVC.Data_Access_Layer;
using PagedList;

namespace FirstMVC.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomer_DAL _dashboard = new Customer_DAL();
        //
        // GET: /Employee/

        //public ActionResult Index()
        //{
        //    return View(_dashboard.GetAll());
        //}

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, string dealerSearch, string searchSSNum, string searchLastName, int? page)
        {
            if  (!String.IsNullOrEmpty(dealerSearch))
            {
                string cuid;
                if ((int) Session["cuid"] > 0)
                {
                    cuid = Convert.ToString((int) Session["cuid"]);
                }
                else
                {
                    cuid = "";


                }
                List<Customer> lstCustomer = _dashboard.GetAll(cuid);
                var customers = from s in lstCustomer
                                select s;
                if (!String.IsNullOrEmpty(searchSSNum) & !String.IsNullOrEmpty(searchLastName))
                {
                    customers =
                        customers.Where(s => s.CCSocialNum.EndsWith(searchSSNum) & s.LastName.Equals(searchLastName));
                    if (customers.Count() <= 0)
                    {
                        ModelState.AddModelError("", "No customer found!");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Social Security number and last name both required information!");
                    return RedirectToAction("Index", "Home");
                }
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(customers.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                string cuid;
                try
                {
                    if ((int)Session["cuid"] > 0)
                    {
                        cuid = Convert.ToString((int)Session["cuid"]);
                    }
                    else
                    {
                        cuid = "";
                    }
                }
                catch (Exception e)
                {
                    cuid = "";
                }
                
                List<Customer> lstCustomer = _dashboard.GetAll(cuid);
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                var customers = from s in lstCustomer
                              select s;

                if (!String.IsNullOrEmpty(searchString))
                {
                    customers = customers.Where(s => s.CustomerName.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        customers = customers.OrderByDescending(s => s.CustomerName);
                        break;
                }

                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(customers.ToPagedList(pageNumber, pageSize));
            }
            //return View(customers);
        }


        //public ActionResult CustomerSearch(string searchSSNum, string searchLastName)
        //{
        //    string cuid;
        //    try
        //    {
        //        if ((int)Session["cuid"] > 0)
        //        {
        //            cuid = (string)Session["cuid"];
        //        }
        //        else
        //        {
        //            cuid = "";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        cuid = "";
        //    }
        //    List<Customer> lstCustomer = _dashboard.GetAll(cuid);
        //        var customers = from s in lstCustomer
        //                        select s;
        //        if (!String.IsNullOrEmpty(searchSSNum) & !String.IsNullOrEmpty(searchLastName))
        //        {
        //            customers =
        //                customers.Where(s => s.CCSocialNum.EndsWith(searchSSNum) & s.LastName.Equals(searchLastName));
        //            if (customers.Count() <= 0)
        //            {
        //                ModelState.AddModelError("", "No customer found!");
        //                return RedirectToAction("Index", "Home");
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Social Security number and last name both required information!");
        //            return RedirectToAction("Index", "Home");
        //        }

        //    Customer cus = customers.First();

        //    //ILoanInfo_DAL loaninfo = new LoanInfo_DAL();
        //    //List<LoanInfo> lstLoan = loaninfo.GetAll();
        //    //var loans = from l in lstLoan
        //    //            select l;
            
        //    //loans = loans.Where(l => l.CustomerId.Equals(cus.id));
        //    //LoanInfo ln = loans.First();
        //    //if (loans.Any())
        //    //{
        //    //    ViewBag.LoanId = Convert.ToString(ln.id);
        //    //    ViewBag.LoanStatus = ""; 
        //    //    ViewBag.CUName = Convert.ToString(ln.CUName);
        //    //    ViewBag.LTV = Convert.ToString(ln.LTV);
        //    //    ViewBag.Rate = Convert.ToString(ln.Rate);
        //    //    ViewBag.Term = Convert.ToString(ln.Term);
        //    //    ViewBag.ApprovalAmt = Convert.ToString(ln.ApprovalAmt);
        //    //    ViewBag.PreApprovExpirationDate = Convert.ToString(ln.PreApprovExpirationDate, new DateTimeFormatInfo());
        //    //    ViewBag.LoanDoc = Convert.ToString(ln.LoanDoc);
        //    //}
        //    //else
        //    //{
        //    //    ViewBag.LoanId = "0";
        //    //    ViewBag.LoanStatus = "Loan not Approved!";
        //    //    ViewBag.CUName = "";
        //    //    ViewBag.LTV = "";
        //    //    ViewBag.Rate = "";
        //    //    ViewBag.Term = "";
        //    //    ViewBag.ApprovalAmt = "";
        //    //    ViewBag.PreApprovExpirationDate = "";
        //    //    ViewBag.LoanDoc = "";
        //    //}


        //    //IVehicleInfo_DAL vehicleinfo = new VehicleInfo_DAL();
        //    //List<VehicleInfo> lsVehicle = vehicleinfo.GetAll();
        //    //var vehicles = from v in lsVehicle
        //    //            select v;

        //    //vehicles = vehicles.Where(v => v.CustomerId.Equals(cus.id));
        //    //VehicleInfo vl = vehicles.First();
        //    //if (vehicles.Any())
        //    //{
        //    //    ViewBag.vehicleId = vl.id;
        //    //    ViewBag.VehicleStatus = "";
        //    //    ViewBag.Year = Convert.ToString(vl.Year);
        //    //    ViewBag.Make = Convert.ToString(vl.Make);
        //    //    ViewBag.Model = Convert.ToString(vl.Model);
        //    //    ViewBag.VinNum = Convert.ToString(vl.VinNum);
        //    //    ViewBag.Mileage = Convert.ToString(vl.Mileage);
        //    //    ViewBag.PayOff = Convert.ToString(vl.PayOff);
        //    //    ViewBag.PerDiem = Convert.ToString(vl.PerDiem);
        //    //    ViewBag.PayOffExpirationDate = Convert.ToString(vl.PayOffExpirationDate);
        //    //}
        //    //else
        //    //{
        //    //    ViewBag.vehicleId = "0";
        //    //    ViewBag.VehicleStatus = "Vehicle Not found!";
        //    //    ViewBag.Year = "";
        //    //    ViewBag.Make = "";
        //    //    ViewBag.Model = "";
        //    //    ViewBag.VinNum = "";
        //    //    ViewBag.Mileage = "";
        //    //    ViewBag.PayOff = "";
        //    //    ViewBag.PerDiem = "";
        //    //    ViewBag.PayOffExpirationDate = "";
        //    //}

        //    return View(cus);
        //}

        //
        // GET: /Employee/Details/5

        public ActionResult Details(string searchSSNum, string searchLastName,int? id)
        {
            if (!String.IsNullOrEmpty(searchSSNum) & !String.IsNullOrEmpty(searchLastName))
            {
                string cuid;
                try
                {
                    if ((int)Session["cuid"] > 0)
                    {
                        cuid = (string)Session["cuid"];
                    }
                    else
                    {
                        cuid = "";
                    }
                }
                catch (Exception e)
                {
                    cuid = "";
                }
                List<Customer> lstCustomer = _dashboard.GetAll(cuid);
                var customers = from s in lstCustomer
                                select s;
                if (!String.IsNullOrEmpty(searchSSNum) & !String.IsNullOrEmpty(searchLastName))
                {
                    customers =
                        customers.Where(s => s.CCSocialNum.EndsWith(searchSSNum) & s.LastName.Equals(searchLastName));
                    if (customers.Count() <= 0)
                    {
                        ModelState.AddModelError("", "No customer found!");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Social Security number and last name both required information!");
                    return RedirectToAction("Index", "Home");
                }

                Customer cus = customers.First();
                ViewBag.CustomerSearch = "1";
                ICreditUnion_DAL _cu = new CreditUnion_DAL();
                CreditUnion Cre = new CreditUnion();
                Cre = _cu.Find(cus.CUId);
                ViewBag.CUName = Cre.CUName;
                ViewBag.address = Cre.Address;
                ViewBag.phone = Cre.Phone;
                ViewBag.Fax = Cre.Fax;
                return View(cus);

            }
            else
            {
                ViewBag.CustomerSearch = "0";
                Customer _cust = new Customer();
                _cust = _dashboard.Find(id);
                ICreditUnion_DAL _cu = new CreditUnion_DAL();
                CreditUnion Cre = new CreditUnion();
                Cre = _cu.Find(_cust.CUId);
                ViewBag.CUName = Cre.CUName;
                ViewBag.address = Cre.Address;
                ViewBag.phone = Cre.Phone;
                ViewBag.Fax = Cre.Fax;
                return View(_cust);
                
            }
            
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            ICreditUnion_DAL _cu = new CreditUnion_DAL();
            ViewBag.CU = new SelectList(_cu.GetAll(), "id", "CUName");
            ViewBag.usertype = Session["UserRole"];
            ViewBag.CUId = Session["cuid"];

            ViewBag.preapprovalid = _dashboard.getPreAprrovalID();
            if ((int) Session["UserRole"] > 0)
            {
                CreditUnion Cre = new CreditUnion();
                Cre = _cu.Find((int) ViewBag.CUId);
                ViewBag.CUName = Cre.CUName;
                ViewBag.address = Cre.Address;
                ViewBag.phone = Cre.Phone;
                ViewBag.Fax = Cre.Fax;
            }
            else
            {
                ViewBag.CUName ="";
                ViewBag.address = "";
                ViewBag.phone = "";
                ViewBag.Fax = "";
            }
            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create([Bind(Include = "CUId,CustomerName,CoBorrowerName,FirstName,LastName,CCSocialNum,DrivingLic,DOB,Address,Phone,Email,LTV, Rate, Term, ApprovalAmt, PreApprovExpirationDate, LoanDoc,Year,Make,Model,VinNum,Mileage,PayOff,PerDiem,PayOffExpirationDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if ((int)Session["UserRole"] > 0 )
                    customer.CUId = (int)Session["cuid"];
                customer.preapprovalid = _dashboard.getPreAprrovalID();
                _dashboard.Add(customer);
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(int id)
        {
            Customer _cust = new Customer();
            _cust = _dashboard.Find(id);
            ICreditUnion_DAL _cu = new CreditUnion_DAL();
            CreditUnion Cre = new CreditUnion();
            Cre = _cu.Find(_cust.CUId);
            ViewBag.CUName = Cre.CUName;
            ViewBag.address = Cre.Address;
            ViewBag.phone = Cre.Phone;
            ViewBag.Fax = Cre.Fax;
            return View(_cust);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,CUId,CustomerName,CoBorrowerName,FirstName,LastName,CCSocialNum,DrivingLic,DOB,Address,Phone,Email,LTV, Rate, Term, ApprovalAmt, PreApprovExpirationDate, LoanDoc,Year,Make,Model,VinNum,Mileage,PayOff,PerDiem,PayOffExpirationDate")] Customer customer, int id)
        {
            //if (ModelState.IsValid)
            //{
            _dashboard.Update(customer);
            return RedirectToAction("Index");
            //}
            //return View(customer);
        }

        //
        // GET: /Employee/Delete/5

        public ActionResult Delete(int id)
        {
            Customer _cust = new Customer();
            _cust = _dashboard.Find(id);
            ICreditUnion_DAL _cu = new CreditUnion_DAL();
            CreditUnion Cre = new CreditUnion();
            Cre = _cu.Find(_cust.CUId);
            ViewBag.CUName = Cre.CUName;
            ViewBag.address = Cre.Address;
            ViewBag.phone = Cre.Phone;
            ViewBag.Fax = Cre.Fax;
            return View(_cust);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _dashboard.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}