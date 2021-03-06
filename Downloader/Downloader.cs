using Blockchain_Programming.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace Blockchain_Programming.Downloader
{
    class Downloader
    {
        static void Main()
        {
            Token token = JsonSerializer.Deserialize<Token>(File.ReadAllText("token.dat"));
            EntityContainer entityContainer = JsonSerializer.Deserialize<EntityContainer>(File.ReadAllText("entities.dat"));

            List<Article> articles = new List<Article>();

            foreach (var item in entityContainer.EntityResponses)
            {
                Schema schema = ModexService.ViewRecord(entityContainer.SchemaName, item.recordId, token).Result;
                articles.Add(schema.Article);

                Console.WriteLine(schema.Article.Title);
                Console.WriteLine(schema.Article.Summary);
                Console.WriteLine(schema.Article.PublicationDate);
                Console.WriteLine();
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Article>));
            StreamWriter streamWriter = new StreamWriter("out.xml");
            xmlSerializer.Serialize(streamWriter, articles);
            streamWriter.Close();

            Console.WriteLine("Process finished");
            Console.ReadKey();
        }
    }
}
