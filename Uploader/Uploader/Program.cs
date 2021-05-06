using System;
using System.Net;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using Blockchain_Programming.Services;
using System.Text.Json;
using System.IO;

namespace Blockchain_Programming.Uploader
{
    class Program
    {
        public static EntityResponse EntityResponse { get; private set; }

        static void Main(string[] args)
        {
            List<Article> articles = ArticleService.DownloadArticlesFromTelex();
            List<string> jsonArticles = ArticleService.ArticlesToJson(articles);
            ModexService modexService = ModexService.Create().Result;
            Console.WriteLine($"Access token: {modexService.Token.access_token}");
            Console.WriteLine($"Refresh token: {modexService.Token.refresh_token}");
            Console.WriteLine(modexService.Token.token_type);

            EntityResponse entityResponse = modexService.CreateEntity(File.ReadAllText("schema.json"), "ENTER UNIQUE ID HERE").Result;

            if (entityResponse.message == null || entityResponse.message == "")
            {
                Console.WriteLine(entityResponse.transactionId);
            }
            else
            {
                Console.WriteLine(entityResponse.message);
            }
        }
    }
}
