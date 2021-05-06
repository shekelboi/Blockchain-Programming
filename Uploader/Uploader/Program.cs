using System;
using System.Net;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using Blockchain_Programming.Services;

namespace Blockchain_Programming.Uploader
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Article> articles = ArticleService.DownloadArticlesFromTelex();
            List<string> jsonArticles = ArticleService.ArticlesToJson(articles);
            Token token = ModexService.ObtainToken().Result;
            Console.WriteLine($"Access token: {token.access_token}");
            Console.WriteLine($"Refresh token: {token.refresh_token}");
        }
    }
}
