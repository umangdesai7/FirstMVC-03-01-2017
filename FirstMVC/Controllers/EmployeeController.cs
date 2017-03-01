using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstMVC.Models;
using FirstMVC.Data_Access_Layer;

namespace FirstMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmplyee_DAL _dashboard = new Emplyee_DAL(); 
        //
        // GET: /Employee/

        public ActionResult Index()
        {
            return View(_dashboard.GetAll());
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
        public ActionResult Create([Bind(Include = "name,address,phone,email")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _dashboard.Add(employee);
                return RedirectToAction("Index");
            }

            return View(employee);
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
        public ActionResult Edit([Bind(Include = "employeeunkid,name,address,phone,email")] Employee employee, int id)
        {
            if (ModelState.IsValid)
            {
                _dashboard.Update(employee);
            }
            return View(employee);
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