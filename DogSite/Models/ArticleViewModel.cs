using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogSite.Models
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Attribution { get; set; }
    }
}