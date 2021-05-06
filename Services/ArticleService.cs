using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blockchain_Programming.Services
{
    public static class ArticleService
    {
        public static List<Article> DownloadArticlesFromTelex()
        {
            string URL = "https://telex.hu/rss/archivum?filters=%7B%22flags%22%3A%5B%22english%22%5D%2C%22parentId%22%3A%5B%22null%22%5D%7D";
            WebClient wbc = new WebClient();
            string source = wbc.DownloadString(URL);
            XDocument xDoc = XDocument.Parse(source);

            List<Article> articles = new List<Article>();

            foreach (var item in xDoc.Root.Element("channel").Elements("item"))
            {
                string title = item.Element("title").Value;
                string description = item.Element("description").Value;
                string pubDate = item.Element("pubDate").Value;
                articles.Add(new Article(title, description, TelexDateTimeConverter(pubDate)));
            }

            return articles;
        }

        private static DateTime TelexDateTimeConverter(string pubDate)
        {
            Dictionary<string, int> months = new Dictionary<string, int>
            {
                ["Jan"] = 1,
                ["Feb"] = 2,
                ["Mar"] = 3,
                ["Apr"] = 4,
                ["May"] = 5,
                ["Jun"] = 6,
                ["Jul"] = 7,
                ["Aug"] = 8,
                ["Sep"] = 9,
                ["Oct"] = 10,
                ["Nov"] = 11,
                ["Dec"] = 12,
            };

            string[] parts = pubDate.Split(' ');
            int day = int.Parse(parts[1]);
            int month = months[parts[2]];
            int year = int.Parse(parts[3]);

            string[] times = parts[4].Split(':');
            int hour = int.Parse(times[0]);
            int minute = int.Parse(times[1]);
            int second = int.Parse(times[2]);

            return new DateTime(year, month, day, hour, minute, second);
        }

        public static List<string> ArticlesToJson(List<Article> articles)
        {
            List<string> jsonArticles = new List<string>();

            foreach (var article in articles)
            {
                jsonArticles.Add(JsonSerializer.Serialize(article));
            }

            return jsonArticles;
        }
    }
}
