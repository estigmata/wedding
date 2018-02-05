using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using JalaFoundation.Dev23.Wedding.API.DTO;
using JalaFoundation.Dev23.Wedding.BL.Interfaces;
using JalaFoundation.Dev23.Wedding.BL.Models;
using JalaFoundation.Dev23.Wedding.BL.Services;

namespace JalaFoundation.Dev23.Wedding.API.Controller
{
    public class CouplesController : ApiController
    {
        private readonly IAccountsService _accountService;
        private readonly ICouplesService _coupleService;
        private readonly IPersonsService _personService;
        private readonly IPresentListService _presentListService;

        public CouplesController()
        {
            _accountService = new AccountsService();
            _coupleService = new CouplesService();
            _personService = new PersonsService();
            _presentListService = new PresentListService();
        }

        // POST: api/couples
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage Post(CoupleDTO coupleDto)
        {
            var personPartner = new PersonBusiness()
            {
                FirstName = coupleDto.FirstNamePartner,
                LastName = coupleDto.LastNamePartner,
                CI = coupleDto.IdPartner,
                Telephone = coupleDto.TelephonePartner,
                Direction = coupleDto.AddressPartner,
                Mail = coupleDto.MailPartner
                
            };
            var person = new PersonBusiness()
            {
                FirstName = coupleDto.FirstName,
                LastName = coupleDto.LastName,
                CI = coupleDto.Id,
                Telephone = coupleDto.Telephone,
                Direction = coupleDto.Address,
                Mail = coupleDto.Mail
            };

            var presentList = new PresentListBusiness()
            {
                Quantity = 25
            };

            int IDPersonPartner = _personService.AddPerson(personPartner);
            int IDPerson = _personService.AddPerson(person);
            int IDPresentList = _presentListService.Add(presentList);


            var couple = new CoupleBusiness()
            {
                WeddingDate = coupleDto.WeddingDate,
                DeliveryDate = coupleDto.DeliveryDate,
                Address = coupleDto.AddressDelivery,
                HusbandID = IDPersonPartner,
                WifeID = IDPerson,
                PresentListID = IDPresentList,
                Latitude = coupleDto.Latitude,
                Longitude = coupleDto.Longitude
            };

            int IDCouple = _coupleService.AddCouple(couple);

            var account = new AccountBusiness(person, personPartner, IDCouple);
            _accountService.AddAccount(account);

            AccountDTO accountDTO = new AccountDTO()
            {
                Account = account.Name,
                Password = account.Password,
                FirstName = person.FirstName,
                FirstNamePartner = personPartner.FirstName
            };

            return Request.CreateResponse(HttpStatusCode.OK, accountDTO);
        }
    }
}