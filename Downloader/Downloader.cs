using Blockchain_Programming.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Blockchain_Programming.Downloader
{
    class Downloader
    {
        static void Main()
        {
            Token token = JsonSerializer.Deserialize<Token>(File.ReadAllText("token.dat"));
            EntityContainer entityContainer = JsonSerializer.Deserialize<EntityContainer>(File.ReadAllText("entities.dat"));

            foreach (var item in entityContainer.EntityResponses)
            {
                Schema schema = ModexService.ViewRecord(entityContainer.SchemaName, item.recordId, token.access_token).Result;

                Console.WriteLine(schema.Article.Title);
                Console.WriteLine(schema.Article.Summary);
                Console.WriteLine(schema.Article.PublicationDate);
                Console.WriteLine();
            }

        }
    }
}
