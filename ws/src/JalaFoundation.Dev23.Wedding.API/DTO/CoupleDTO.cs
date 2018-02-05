using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JalaFoundation.Dev23.Wedding.API.DTO
{
    public class CoupleDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public int Telephone { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public string FirstNamePartner { get; set; }
        public string LastNamePartner { get; set; }
        public int IdPartner { get; set; }
        public int TelephonePartner { get; set; }
        public string MailPartner { get; set; }
        public string AddressPartner { get; set; }
        public DateTime WeddingDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string AddressDelivery { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}