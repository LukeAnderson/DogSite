using DogSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DogSite.Controllers
{
    public class LoginController : Controller
    {
        private ArticleDatabaseEntities db;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(User userModel)
        {
            db = new ArticleDatabaseEntities();
            var userDetails = db.Users.Where(x => x.Username == userModel.Username && x.Password == userModel.Password).FirstOrDefault();
            if(userDetails == null)
            {
                userModel.LoginErrorMessage = "Wrong username or password";
                return View("Index", userModel);
            }
            else
            {
                Session["userId"] = userDetails.UserId;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");

        }



    }
}