using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JalaFoundation.Dev23.Wedding.API.DTO
{
    public class AccountDTO
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string FirstNamePartner { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public int PresentListId { get; set; }
        public DateTime WeddingDate { get; set; }
    }
}