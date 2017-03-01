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
    public class CreditUnionController : Controller
    {
        private ICreditUnion_DAL _dashboard = new CreditUnion_DAL();
        //
        // GET: /Employee/

        //public ActionResult Index()
        //{
        //    return View(_dashboard.GetAll());
        //}

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            List<CreditUnion> lstCreditUnion = _dashboard.GetAll();
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

            var CreditUnions = from s in lstCreditUnion
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                CreditUnions = CreditUnions.Where(s => s.CUName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    CreditUnions = CreditUnions.OrderByDescending(s => s.CUName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(CreditUnions.ToPagedList(pageNumber, pageSize));

            //return View(CreditUnions);
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
            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create([Bind(Include = "CUName,Address,Phone,Fax,Email")] CreditUnion creditunkion)
        {
            if (ModelState.IsValid)
            {
                _dashboard.Add(creditunkion);
                return RedirectToAction("Index");
            }

            return View(creditunkion);
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(int id)
        {
            return View(_dashboard.Find(id));
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,CUName,Address,Phone,Fax,Email")] CreditUnion creditunion, int id)
        {
            if (ModelState.IsValid)
            {
                _dashboard.Update(creditunion);
                return RedirectToAction("Index");
            }
            return View(creditunion);
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