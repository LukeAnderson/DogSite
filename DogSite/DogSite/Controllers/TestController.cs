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
        public TestController()
        {
            db = new ArticleDatabaseEntities();
            articleList = db.Articles.ToList();
            articleViewModelList = articleList.Select(x => GetArticleViewModel(x)).ToList();
        }

        public ActionResult Index(int page =1, int pageSize = 10)
        {     
            PagedList<ArticleViewModel> model = new PagedList<ArticleViewModel>(articleViewModelList, page, pageSize);
            return View(model);
        }



        //index of articles a title is clicked.
        public ActionResult ArticleDetail(int articleId = 1)
        {
            return View(articleViewModelList.SingleOrDefault(x => x.Id == articleId));
        }

        //partial view for ArticleDetail page
        public PartialViewResult GetPartialView(int Id)
        {

            if ( Id+1 < articleViewModelList.Count())
            {
                return PartialView("_Article", articleViewModelList.SingleOrDefault(x => x.Id == Id + 1));
            }

            return PartialView("_Article", articleViewModelList.SingleOrDefault(x => x.Id == 1));
        }




        public PartialViewResult GetArticle(int Id)
        {
            if (Id > articleViewModelList.Count)//get the first article
                Id = 1;
            if (Id < 1)//get the last article
                Id = articleViewModelList.Count;
            return PartialView("_Article", articleViewModelList.SingleOrDefault(x => x.Id == Id));
        }


        public ArticleViewModel GetArticleViewModel(Article article)
        {
            return new ArticleViewModel { Id = article.Id, Title = article.Title, Body = article.Body, Attribution = article.Attribution };
        }

        public string GetComments()
        {
            return "got the comments";
        }

    }
}