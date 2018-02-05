using System;
using System.Collections.Generic;

namespace JalaFoundation.Dev23.Wedding.BL.Models
{
    //Model Class
    public class PresentListBusiness
    {
        public int? PresentlistID { get; set; }
        public PersonBusiness Husband { get; set; }
        public PersonBusiness Wife { get; set; }
        public DateTime WeddingDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public List<PresentBusiness> Presents { get; set; }
    }
}
