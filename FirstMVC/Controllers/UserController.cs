using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstMVC.Models;
using FirstMVC.Data_Access_Layer;
using PagedList;
using System.Web.Security;

namespace FirstMVC.Controllers
{
    public class UserController : Controller
    {
        private IUser_DAL _dashboard = new User_DAL();
        //
        // GET: /Employee/

        //public ActionResult Index()
        //{
        //    return View(_dashboard.GetAll());
        //}

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            List<User> lstUser = _dashboard.GetAll();
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

            var Users = from s in lstUser
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                Users = Users.Where(s => s.UserName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    Users = Users.OrderByDescending(s => s.UserName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(Users.ToPagedList(pageNumber, pageSize));

            //return View(Users);
        }


        //
        // GET: /Employee/Details/5

        public ActionResult Details(int? id)
        {
            User _user = new User();
            _user = _dashboard.Find(id);
            ViewBag.usertype = _user.usertype;
            return View(_user);
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            ICreditUnion_DAL _cu = new CreditUnion_DAL();
            ViewBag.CU = new SelectList(_cu.GetAll(), "id", "CUName");
            IDealer_DAL _Del = new Dealer_DAL();
            ViewBag.Del = new SelectList(_Del.GetAll(), "id", "DealerName");
            ViewBag.usertype = 0;
            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create([Bind(Include = "usertype,CUId,DelId,UserName,Password,FirstName,LastName,Gender,Address,Phone,Email")] User user)
        {
            //if (ModelState.IsValid)
            //{
                //user.usertype = (int) ViewBag.usertype;
                _dashboard.Add(user);
                return RedirectToAction("Index");
            //}

            //return View(user);
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(int id)
        {
            User _user = new User();
            _user = _dashboard.Find(id);
            ViewBag.usertype = _user.usertype;
            return View(_user);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public ActionResult Edit([Bind(Include = "usertype,id,CUId,DelId,UserName,Password,FirstName,LastName,Gender,Address,Phone,Email")] User user, int id)
        {
            //if (ModelState.IsValid)
            //{
                _dashboard.Update(user);
                return RedirectToAction("Index");
            //}
            //return View(user);
        }

        //
        // GET: /Employee/Delete/5

        public ActionResult Delete(int id)
        {
            User _user = new User();
            _user = _dashboard.Find(id);
            ViewBag.usertype = _user.usertype;
            return View(_user);
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

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginUser(string UserName, string Password)
        {
            IUser_DAL dalUser = new User_DAL();
            User user = dalUser.IsValid(UserName, Password);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                if (user.CUId <= 0 & user.DelId <= 0)
                {
                    Session["UserRole"] = 0;
                    Session["userid"] = user.id;
                    Session["cuid"] = 0;
                    Session["dealerid"] = 0;
                }
                else
                {
                    if (user.CUId > 0)
                    {
                        Session["UserRole"] = 1;
                    }
                    else
                    {
                        Session["UserRole"] = 2;
                    }
                    Session["userid"] = user.id;
                    Session["cuid"] = user.CUId;
                    Session["dealerid"] = user.DelId;
                }
            }
            else
            {
                ModelState.AddModelError("", "Social Security number and last name both required information!");
            }
            return RedirectToAction("Index", "Home");
            
    }

        [HttpPost]
        public ActionResult Login(Models.User user)
        {
            if (ModelState.IsValid)
            {
                IUser_DAL dalUser = new User_DAL();
                user = dalUser.IsValid(user.UserName, user.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    if (user.CUId <= 0 & user.DelId <= 0)
                    {
                        Session["UserRole"] = 0;
                    }
                    else
                    {
                        if (user.CUId > 0)
                        {
                            Session["UserRole"] = 1;
                        }
                        else
                        {
                            Session["UserRole"] = 2;
                        }
                    }
                    Session["userid"] = user.id;
                    Session["cuid"] = user.CUId;
                    Session["dealerid"] = user.DelId;
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}