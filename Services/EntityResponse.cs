using System;
using System.Collections.Generic;
using System.Text;

namespace Blockchain_Programming.Services
{
    public class EntityResponse
    {
        public bool Success { get; set; }
        public string message { get; set; }
        public string transactionId { get; set; }
        public string recordId { get; set; }
    }
}
