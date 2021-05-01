using System;
using System.Collections.Generic;
using System.Text;

namespace Downloader
{
    class Article
    {
        string title;
        string summary;
        DateTime publicationDate;

        public string Title { get => title; set => title = value; }
        public string Summary { get => summary; set => summary = value; }
        public DateTime PublicationDate { get => publicationDate; set => publicationDate = value; }

        public Article(string title, string summary, DateTime publicationDate)
        {
            this.Title = title;
            this.Summary = summary;
            this.PublicationDate = publicationDate;
        }

    }
}
