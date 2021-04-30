using System;
using System.Net;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace Downloader
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Article> articles = ArticleService.DownloadArticlesFromTelex();
            List<string> jsonArticles = ArticleService.ArticlesToJson(articles);
        }
    }
}
