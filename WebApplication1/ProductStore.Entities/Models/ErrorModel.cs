using System;
using System.Collections.Generic;
using System.Text;

namespace ProductStore.Entities.Models
{
    public class ErrorModel
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Response { get; set; }
        public DateTime When { get; set; }
    }
}
