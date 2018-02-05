using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JalaFoundation.Dev23.Wedding.API.DTO
{
    public class PresentListDTO
    {
        public List<int> PresentList { get; set; }
        public List<PresentDTO> Presents { get; set; }
        public int ListId { get; set; }
        public int Size { get; set; }
    }
}