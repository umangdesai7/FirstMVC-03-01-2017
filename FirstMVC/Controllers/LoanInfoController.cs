using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstMVC.Models;
using FirstMVC.Data_Access_Layer;
using PagedList;

namespace FirstMVC.Controllers
{
    public class LoanInfoController : Controller
    {
        private ILoanInfo_DAL _dashboard = new LoanInfo_DAL();
        //
        // GET: /Employee/

        //public ActionResult Index()
        //{
        //    return View(_dashboard.GetAll());
        //}

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            List<LoanInfo> lstLoanInfo = _dashboard.GetAll();
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

            var loans = from s in lstLoanInfo
                        select s;

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    loans = loans.Where(s => s.CustomerName.Contains(searchString)
            //                           || s.CustomerName.Contains(searchString));
            //}

            //switch (sortOrder)
            //{
            //    case "name_desc":
            //        loans = loans.OrderByDescending(s => s.CustomerName);
            //        break;
            //}

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(loans.ToPagedList(pageNumber, pageSize));

            //return View(dealers);
        }

        //
        // GET: /Employee/Details/5

        public ActionResult Details(int? id)
        {

            return View(_dashboard.Find(id));
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            ICreditUnion_DAL _cu = new CreditUnion_DAL();
            ViewBag.CU = new SelectList(_cu.GetAll() , "id", "CUName");
            ICustomer_DAL _Cust = new Customer_DAL();
            ViewBag.Cust = new SelectList(_Cust.GetAll(""), "id", "CustomerName");
            return View();
          
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create([Bind(Include = "CUId, CustomerId, LTV, Rate, Term, ApprovalAmt, PreApprovExpirationDate, LoanDoc")] LoanInfo loaninfo)
        {
            if (ModelState.IsValid)
            {
                _dashboard.Add(loaninfo);
                return RedirectToAction("Index");
            }

            return View(loaninfo);
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(int id)
        {
            ICreditUnion_DAL _cu = new CreditUnion_DAL();
            ViewBag.CU = new SelectList(_cu.GetAll(), "id", "CUName");
            ICustomer_DAL _Cust = new Customer_DAL();
            ViewBag.Cust = new SelectList(_Cust.GetAll(""), "id", "CustomerName");
            return View(_dashboard.Find(id));
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,CUId, CustomerId, LTV, Rate, Term, ApprovalAmt, PreApprovExpirationDate, LoanDoc")] LoanInfo loaninfo, int id)
        {
            if (ModelState.IsValid)
            {
                _dashboard.Update(loaninfo);
                return RedirectToAction("Index");
            }
            return View(loaninfo);
        }

        //
        // GET: /Employee/Delete/5

        public ActionResult Delete(int id)
        {
            return View(_dashboard.Find(id));
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