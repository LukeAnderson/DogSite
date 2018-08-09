using DogSite.Models;
using System.Linq;
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
        public ActionResult Authorize(UserViewModel userModel)
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
                Session["Username"] = userDetails.Username;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");

        }


        public ActionResult AddOrEdit(int id = 1)
        {
            UserViewModel uvm = new UserViewModel();
            return View(uvm);
        }

        [HttpPost]
        public ActionResult AddOrEdit(UserViewModel uvm)
        {
            if(uvm.Username == null || uvm.Password == null)
            {
                ViewBag.RegistrationMsg = "Enter in username and password.";
            }
            else if(!uvm.Password.Equals(uvm.ConfirmPassword))
            {
                ViewBag.RegistrationMsg = "Password and confirm password do not match.";
            }
            else
            {
                db = new ArticleDatabaseEntities();
                User newUser = new User { Username = uvm.Username, Password = uvm.Password };
                db.Users.Add(newUser);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.RegistrationMsg = "Registration successful!";
            }
            


            return View("AddOrEdit", new UserViewModel());
        }



    }
}