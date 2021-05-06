﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Blockchain_Programming.Services
{
    public class Token
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
    }
}
