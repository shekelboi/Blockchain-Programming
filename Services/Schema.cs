using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Blockchain_Programming.Services
{
    public class Schema
    {
        public string ID { get; set; }
        public Article Article { get; set; }
    }
}
