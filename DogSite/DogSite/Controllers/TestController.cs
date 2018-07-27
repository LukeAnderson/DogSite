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
        List<ArticleViewModel> articleViewModelList;
        public TestController()
        {
            db = new ArticleDatabaseEntities();
            articleViewModelList = db.Articles.ToList().Select(x => MakeArticleViewModel(x)).ToList();
        }

        //MakeArticleViewModel => (article -> articleViewModel)
        public ArticleViewModel MakeArticleViewModel(Article article)
        {
            return new ArticleViewModel { Id = article.ArticleId, Title = article.Title, Body = article.Body, Attribution = article.Attribution };
        }

        //get PagedList of all the articles.
        public ActionResult Index(int page =1, int pageSize = 10)
        {     
            return View(new PagedList<ArticleViewModel>(articleViewModelList, page, pageSize));
        }

        //get a specific article
        public ActionResult ArticleDetail(int articleId = 1)
        {
            return View(articleViewModelList.SingleOrDefault(x => x.Id == articleId));
        }

        public PartialViewResult GetArticle(int Id)
        {
            if (Id > articleViewModelList.Count)//get the first article
                Id = 1;
            if (Id < 1)//get the last article
                Id = articleViewModelList.Count;
            return PartialView("_Article", articleViewModelList.SingleOrDefault(x => x.Id == Id));
        }



        public string GetComments(int articleId)
        {
            List<Comment> commentList = db.Comments.Where(x => x.ArticleId == articleId).ToList();
            string commentText = "";
            foreach (Comment c in commentList)
            {
                commentText += c.Text + "\n\n\n";
            }
            return commentText;
        }


        public string AddComment(string comment, int articleId)
        {
            Comment newcomment = new Comment();
            newcomment.ArticleId = articleId;
            newcomment.Text = comment;

            db.Comments.Add(newcomment);
            db.SaveChanges();



            var username = "anonymous";
            if (Session["userID"] != null)
                username = getUser((int)Session["userId"]).Username;
            return username + " added comment: " + comment + " to article: " + articleId;
        }

        public int GetMaxCount()
        {
            return articleViewModelList.Count;
        }


        public User getUser(int userId)
        {
            return db.Users.SingleOrDefault(x => x.UserId == userId);
        }

    }
}