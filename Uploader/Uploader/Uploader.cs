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

            Console.Write("Enter a name for the schema: ");
            string schemaName = Console.ReadLine();

            EntityContainer entityContainer = UploadData(jsonArticles, schemaName, token);
            File.WriteAllText("entities.dat", JsonSerializer.Serialize(entityContainer));

            Console.WriteLine("Process finished");
            Console.ReadKey();
        }

        static EntityContainer UploadData(List<string> jsonArticles, string schemaName, Token token)
        {
            EntityResponse entityResponse = ModexService.CreateEntity(File.ReadAllText("schema.json"), schemaName, token).Result;
            Console.WriteLine("Schema created!");

            EntityContainer entityContainer = new EntityContainer()
            {
                SchemaName = schemaName,
                EntityResponses = new List<EntityResponse>()
            };

            foreach (var article in jsonArticles)
            {
                entityResponse = ModexService.UploadRecord(article, schemaName, token).Result;
                entityContainer.EntityResponses.Add(entityResponse);
                Console.WriteLine("Record added!");
            }

            return entityContainer;
        }
    }
}
