using System;
using System.Collections.Generic;
using System.Text;

namespace ProductStore.Services.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
