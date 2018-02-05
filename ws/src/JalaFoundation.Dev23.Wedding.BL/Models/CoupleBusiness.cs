using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JalaFoundation.Dev23.Wedding.BL.Models
{
    public class CoupleBusiness
    {
        public DateTime WeddingDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Address { get; set; }
        public int? WifeID { get; set; }
        public int? HusbandID { get; set; }
        public int? PresentListID { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
