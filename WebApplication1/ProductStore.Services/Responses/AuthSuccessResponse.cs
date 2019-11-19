using System;
using System.Collections.Generic;
using System.Text;

namespace ProductStore.Services.Responses
{
    public class AuthSuccessResponse
    {
        public string Tocken { get; set; }
        public string Password { get; set; }
    }
}
