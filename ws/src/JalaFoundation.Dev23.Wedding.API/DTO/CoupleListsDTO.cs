using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JalaFoundation.Dev23.Wedding.API.DTO
{
    public class CoupleListsDTO
    {
        public int? PresentListId { get; set; }

        public PersonDTO Husband { get; set; }

        public PersonDTO Wife { get; set; }

        public DateTime WeddingDate { get; set; }

        public string Status { get; set; }
    }
}