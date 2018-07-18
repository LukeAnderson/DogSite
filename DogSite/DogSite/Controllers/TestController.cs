using DogSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DogSite.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            ArticleDatabaseEntities db = new ArticleDatabaseEntities();
            Article article = db.Articles.SingleOrDefault(x => x.Id ==1);


            ArticleViewModel articleVM = new ArticleViewModel();
            articleVM.Title = article.Title;
            articleVM.Body = article.Body;
            articleVM.Attribution = article.Attribution;


            return View(articleVM);
        }
    }
}