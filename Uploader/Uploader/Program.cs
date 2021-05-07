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

            Console.WriteLine(jsonArticles[0]);

            Token token = ModexService.ObtainToken().Result;
            Console.WriteLine($"Access token: {token.access_token}");
            Console.WriteLine($"Refresh token: {token.refresh_token}");

            string schemaName = "JustAnExamleAgain";

            EntityResponse entityResponse = ModexService.CreateEntity(File.ReadAllText("schema.json"), schemaName, token.access_token).Result;

            if (entityResponse.message == null || entityResponse.message == "")
            {
                Console.WriteLine(entityResponse.transactionId);
            }
            else
            {
                Console.WriteLine(entityResponse.message);
            }

            entityResponse = ModexService.UploadRecord(jsonArticles.First(), schemaName, token.access_token).Result;

            if (entityResponse.message == null || entityResponse.message == "")
            {
                Console.WriteLine(entityResponse.transactionId);
            }
            else
            {
                Console.WriteLine(entityResponse.message);
            }

            Schema record = ModexService.ViewRecord(schemaName, entityResponse.recordId, token.access_token).Result;

            Console.WriteLine(record.Article.Title);
        }
    }
}
