using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Web.Http.Cors;
using JalaFoundation.Dev23.Wedding.API.DTO;
using JalaFoundation.Dev23.Wedding.API.Exceptions;
using JalaFoundation.Dev23.Wedding.BL.Interfaces;
using JalaFoundation.Dev23.Wedding.BL.Services;

namespace JalaFoundation.Dev23.Wedding.API.Controller
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DedicatoryController : ApiController
    {
        private readonly IPresentListService presentListService;

        public DedicatoryController()
        {
            this.presentListService = new PresentListService();
        }
        public IHttpActionResult Post(DedicatoryDTO dedicatory)
        {
            if (presentListService.AddDedicatory(dedicatory.Name, dedicatory.Message, dedicatory.PresentListProductID))
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}