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

        private ArticleDatabaseEntities db;
        private List<Article> articleList;
        List<ArticleViewModel> articleViewModelList;
        ArticleViewModel current_article;
        public TestController()
        {
            db = new ArticleDatabaseEntities();
            articleList = db.Articles.ToList();
            articleViewModelList = articleList.Select(x => GetArticleViewModel(x)).ToList();
            current_article = articleViewModelList[0];
        }

        // GET: Test
        public ActionResult Index(int page =1, int pageSize = 10)
        {     
            PagedList<ArticleViewModel> model = new PagedList<ArticleViewModel>(articleViewModelList, page, pageSize);
            return View(model);
        }


        public ActionResult ArticleDetail(int articleId = 1)
        {
            current_article = articleViewModelList.SingleOrDefault(x => x.Id == articleId);
            return View(current_article);
        }

        public ArticleViewModel GetArticleViewModel(Article article)
        { 
            return new ArticleViewModel { Id = article.Id, Title = article.Title, Body = article.Body, Attribution = article.Attribution };
        }



        public PartialViewResult GetPartialView()
        {
            return PartialView("_Article");
        }




    }
}