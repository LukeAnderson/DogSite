using DogSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace DogSite.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index(int page =1, int pageSize = 4)
        {
            ArticleDatabaseEntities db = new ArticleDatabaseEntities();
            List<Article> articleList = db.Articles.ToList();

            List<ArticleViewModel> articleViewModelList = new List<ArticleViewModel>();
            articleViewModelList = articleList.Select(x => new ArticleViewModel { Title = x.Title, Body = x.Body, Attribution = x.Attribution }).ToList();

            PagedList<ArticleViewModel> model = new PagedList<ArticleViewModel>(articleViewModelList, page, pageSize);

            return View(model);
        }


        public ActionResult ArticleDetail(int articleID)
        {
            ArticleDatabaseEntities db = new ArticleDatabaseEntities();
            List<Article> articleList = db.Articles.ToList();


        }
    }
}