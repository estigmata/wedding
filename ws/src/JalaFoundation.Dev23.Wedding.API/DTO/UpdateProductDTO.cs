using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JalaFoundation.Dev23.Wedding.API.DTO
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string ImageName { get; set; }
        public int Stock { get; set; }
        public int NewStock { get; set; }
    }
}