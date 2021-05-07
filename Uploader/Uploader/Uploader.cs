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

            EntityContainer entityContainer = UploadData(jsonArticles, "ashui", token);
            File.WriteAllText("entities.dat", JsonSerializer.Serialize(entityContainer));
        }

        static EntityContainer UploadData(List<string> jsonArticles, string schemaName, Token token)
        {
            EntityResponse entityResponse = ModexService.CreateEntity(File.ReadAllText("schema.json"), schemaName, token.access_token).Result;
            Console.WriteLine("Schema created!");

            EntityContainer entityContainer = new EntityContainer()
            {
                SchemaName = schemaName,
                EntityResponses = new List<EntityResponse>()
            };

            foreach (var article in jsonArticles)
            {
                entityResponse = ModexService.UploadRecord(article, schemaName, token.access_token).Result;
                entityContainer.EntityResponses.Add(entityResponse);
                Console.WriteLine("Record added!");
            }

            return entityContainer;
        }
    }
}
