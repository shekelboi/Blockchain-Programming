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
    class Uploader
    {
        static void Main()
        {
            List<Article> articles = ArticleService.DownloadArticlesFromTelex();
            List<string> jsonArticles = ArticleService.ArticlesToJson(articles);

            Token token = ModexService.ObtainToken().Result;
            File.WriteAllText("token.dat", JsonSerializer.Serialize(token));
            Console.WriteLine("Token obtained!");

            List<EntityResponse> entityResponses = UploadData(jsonArticles, "thisisatest34", token);
            File.WriteAllText("entities.dat", JsonSerializer.Serialize(entityResponses));
        }

        static List<EntityResponse> UploadData(List<string> jsonArticles, string schemaName, Token token)
        {
            EntityResponse entityResponse = ModexService.CreateEntity(File.ReadAllText("schema.json"), schemaName, token.access_token).Result;
            Console.WriteLine("Schema created!");

            List <EntityResponse> entityResponses = new List<EntityResponse>();

            foreach (var article in jsonArticles)
            {
                entityResponse = ModexService.UploadRecord(article, schemaName, token.access_token).Result;
                entityResponses.Add(entityResponse);
                Console.WriteLine("Record added!");
            }

            return entityResponses;
        }
    }
}
