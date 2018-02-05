using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using JalaFoundation.Dev23.Wedding.API.DTO;
using JalaFoundation.Dev23.Wedding.BL.Interfaces;
using JalaFoundation.Dev23.Wedding.BL.Models;
using JalaFoundation.Dev23.Wedding.BL.Services;
using JalaFoundation.Dev23.Wedding.DAL.Interfaces;

namespace JalaFoundation.Dev23.Wedding.API.Controller
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountsController : ApiController
    {
        private readonly IAccountsService accountsService;

        public AccountsController()
        {
            accountsService = new AccountsService();
        }

        [HttpPost]
        [Route("api/accounts/authenticate")]
        public HttpResponseMessage Post(AccountDTO accountDto)
        {
            if (accountDto.Account == "inventoryManager" && accountDto.Password == "inventoryManager")
            {
                accountDto.Password = "";
                accountDto.Role = "inventory manager";
                accountDto.Token = "aW52ZW50b3J5TWFuYWdlcjppbnZlbnRvcnlNYW5hZ2Vy";
                return Request.CreateResponse(HttpStatusCode.OK, accountDto);
            }

            AccountBusiness accountBusiness = accountsService.Authenticate(accountDto.Account, accountDto.Password);
            
            if (accountBusiness != null)
            {
                if (accountBusiness.Password == "expired")
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);
                }

                accountDto.Password = "";
                accountDto.Role = "couple";
                accountDto.Token = accountBusiness.Token;
                accountDto.PresentListId = accountBusiness.ProductListId;
                accountDto.FirstName = accountBusiness.HusbandName;
                accountDto.FirstNamePartner = accountBusiness.WifeName;
                accountDto.WeddingDate = accountBusiness.WeddingDate;

                return Request.CreateResponse(HttpStatusCode.OK, accountDto);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
