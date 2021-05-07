using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blockchain_Programming.Services
{
    public class Article
    {
        string title;
        string summary;
        DateTime publicationDate;

        public long ID { get => publicationDate.Ticks; }
        public string Title { get => title; set => title = value; }
        public string Summary { get => summary; set => summary = value; }
        public DateTime PublicationDate { get => publicationDate; set => publicationDate = value; }

    }
}
