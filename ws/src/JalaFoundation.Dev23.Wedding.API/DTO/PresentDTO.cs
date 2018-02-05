using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JalaFoundation.Dev23.Wedding.API.DTO
{
    public class PresentDTO
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}