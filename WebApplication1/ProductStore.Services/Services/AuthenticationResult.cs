using System;
using System.Collections.Generic;
using System.Text;

namespace ProductStore.Services.Services
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> ErrorMessage { get; set; }
    }
}
