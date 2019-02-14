using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Employee.Models;
using System.Data.Entity;
using System.Web.Security;

namespace Employee.Controllers
{
    public class HomeController : Controller
    {
        E_DbContext dbContext = new E_DbContext();
        Employeelist em = new Employeelist();
        Credential cred = new Credential();
        public ActionResult EmployeeManager(string Searchby, string search)
        {
            if (Session["Login"] != null && Session["Login"].ToString() != "")
            {

                if (Searchby == "FirstName")
                {
                    var model = dbContext.employees.Where(emp => emp.FirstName == search || search == null).ToList();
                    return View(model);

                }
                else
                {
                    var model = dbContext.employees.Where(emp => emp.Designation == search || search == null).ToList();
                    return View(model);
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }

        public ActionResult AddNewEmp()
        {
            if (Session["Login"] != null && Session["Login"].ToString() != "")
            {
                return View();
            }

            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddNewEmp(FormCollection form)
        {

            em.EmployeeID = Convert.ToInt32(form["EmployeeID"]);
            em.Prefix = form["Prefix"];
            em.FirstName = form["FirstName"];
            em.MiddleName = form["MiddleName"];
            em.LastName = form["LastName"];
            if (form["Gender"] == "Male")
            {
                em.Gender = form["Gender"];
            }
            else if (form["Gender"] == "Female")
            {
                em.Gender = form["Gender"];
            }
            em.Dob = Convert.ToDateTime(form["Dob"]);
            em.Designation = form["Designation"];
            dbContext.employees.Add(em);
            dbContext.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("EmployeeManager");

        }

        public ActionResult Edit(int Id)
        {
            if (Session["Login"] != null && Session["Login"].ToString() != "")
            {
                Employeelist employeelist = dbContext.employees.Single(m => m.Id == Id);
                return View(employeelist);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form, int Id)
        {
            Employeelist em = dbContext.employees.Single(m => m.Id == Id);
            em.EmployeeID = Convert.ToInt32(form["EmployeeID"]);
            em.Prefix = form["Prefix"];
            em.FirstName = form["FirstName"];
            em.MiddleName = form["MiddleName"];
            em.LastName = form["LastName"];
            if (form["Gender"] == "Male")
            {
                em.Gender = form["Gender"];
            }
            else if (form["Gender"] == "Female")
            {
                em.Gender = form["Gender"];
            }

            em.Dob = Convert.ToDateTime(form["Dob"]);
            em.Designation = form["Designation"];
            dbContext.employees.Attach(em);
            var prf = dbContext.Entry(em);
            prf.Property(m => m.EmployeeID).IsModified = true;
            prf.Property(m => m.Prefix).IsModified = true;
            prf.Property(m => m.FirstName).IsModified = true;
            prf.Property(m => m.LastName).IsModified = true;
            prf.Property(m => m.MiddleName).IsModified = true;
            prf.Property(m => m.Gender).IsModified = true;
            prf.Property(m => m.Dob).IsModified = true;
            prf.Property(m => m.Designation).IsModified = true;
            dbContext.SaveChanges();
            return RedirectToAction("EmployeeManager");
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Credential cred)
        {
            //int? userId = dbContext.credentials(cred.UserName, cred.Password).FirstOrDefault();
            bool isExist = dbContext.credentials.Where(x => x.UserName.Trim().ToLower() == cred.UserName.Trim().ToLower() && x.Password.Trim().ToLower() == cred.Password.Trim().ToLower()).Any();

            string message = string.Empty;
            if (isExist)
            {
                bool IsActive = dbContext.credentials.Where(x => x.UserName.Trim().ToLower() == cred.UserName.Trim().ToLower() && x.Isactive.Trim().ToLower() == "Yes".ToLower()).Any();
                if (IsActive)
                {
                    Session["Login"] = "Login";
                    return RedirectToAction("EmployeeManager", "Home");
                }
                else
                {
                    ModelState.Clear();
                    ViewBag.message = "User not Active";
                    return View();
                }

            }
            else
            {
                ModelState.Clear();
                ViewBag.message = "Either Username or Password is incorrect";
                return View();
            }
        }
        public ActionResult AddCredential(int id)
        {
            if (Session["Login"] != null && Session["Login"].ToString() != "")
            {
                Employeelist employeelist = dbContext.employees.Single(m => m.Id == id);
                Credential cred = new Credential();
                cred.EmployeeID = employeelist.EmployeeID;
                cred.UserName = employeelist.FirstName;
                return View(cred);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddCredential(FormCollection form)
        {
            cred.EmployeeID = Convert.ToInt32(form["EmployeeID"]);
            cred.UserName = form["UserName"];
            cred.CredentailID = Convert.ToInt32(form["CredentailID"]);
            cred.Password = form["Password"];
            cred.Isactive = form["Isactive"];
            dbContext.credentials.Add(cred);
            dbContext.SaveChanges();
            ModelState.Clear();
            return View();
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
    }
}