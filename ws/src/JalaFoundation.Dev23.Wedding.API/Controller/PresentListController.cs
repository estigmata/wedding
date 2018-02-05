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
using JalaFoundation.Dev23.Wedding.BL.Models;

namespace JalaFoundation.Dev23.Wedding.API.Controller
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PresentListController : ApiController
    {
        private readonly IPresentListService presentListService;

        public PresentListController()
        {
            this.presentListService = new PresentListService();
        }

        public HttpResponseMessage Get(string name, string lastName)
        {
            if (name == null && lastName == null)
            {
                throw new WeddingException();
            }
            else
            {
                var foundedPresentsLists = this.presentListService.SearchPresentsList(name, lastName);
                var result = foundedPresentsLists.Select(x => new CoupleListsDTO()
                {
                    PresentListId = x.PresentlistID,
                    Husband = new PersonDTO() { Name = x.Husband.FirstName, LastName = x.Husband.LastName },
                    Wife = new PersonDTO() { Name = x.Wife.FirstName, LastName = x.Wife.LastName },
                    Status = x.Status,
                    WeddingDate = x.WeddingDate
                }).ToList();
                if (result.Count < 1)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No results");
                }

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        [HttpGet]
        [Route("api/presentlist/{id}")]
        public HttpResponseMessage GetList(int id)
        {
            var presentListBusiness = presentListService.GetPresentList(id);

            var presentListDTO = new PresentListDTO()
            {
                PresentList = presentListBusiness.Presents.Select(p => p.PresentID).ToList(),
                ListId = (int) presentListBusiness.PresentlistID,
                Size = presentListBusiness.Quantity,
                Presents = presentListBusiness.Presents.Select(p => new PresentDTO()
                {
                    Id = p.PresentID,
                    Product = new ProductDTO()
                    {
                        Id = p.Product.Id,
                        Name = p.Product.Name,
                        Brand = p.Product.Brand,
                        Category = p.Product.Category,
                        Description = p.Product.Description,
                        Price = p.Product.Price,
                        Stock = p.Product.Stock
                    }
                }).ToList()
            };

            return Request.CreateResponse(HttpStatusCode.OK, presentListDTO);
        }

        [HttpGet]
        [Route("api/presentlist/{id}/presents")]
        public HttpResponseMessage Get(int id)
        {
            var foundedPresentsListProducts = this.presentListService.GetPresents(id);
            var result = foundedPresentsListProducts.Select(x => new PresentDTO()
            {
                Id = x.PresentID,
                Product = new ProductDTO ()
                {
                    Id = x.Product.Id,
                    Brand = x.Product.Brand,
                    Category = x.Product.Category,
                    Description = x.Product.Description,
                    ImageName = x.Product.ImageName,
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    Stock = x.Product.Stock,
                    Image = x.Product.Image
                },
                Message = x.Status.Item2,
                Status = x.Status.Item1,
            }).ToList();
            if (result.Count < 1)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, false);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        public IHttpActionResult Post(PresentListDTO presentList)
        {
            if(presentListService.AddProducts(presentList.PresentList, presentList.ListId))
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        [Route("api/presentlist/{id}/presents")]
        public IHttpActionResult Put(PresentListDTO products)
        {
            if (presentListService.UpdatePresentList(products.PresentList, products.ListId))
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int listID, int productID)
        {
            if(presentListService.DeletePresent(listID, productID))
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("api/presentList/buy")]
        public IHttpActionResult Buy(PresentDTO presentDTO)
        {
            var presentBL = new PresentBusiness();
            presentBL.PresentID = presentDTO.Id;
            presentBL.Product = new ProductBusiness()
            {
                Brand = presentDTO.Product.Brand,
                Category = presentDTO.Product.Category,
                Description = presentDTO.Product.Description,
                Id = presentDTO.Product.Id,
                ImageName = presentDTO.Product.ImageName,
                Name = presentDTO.Product.Name,
                Price = presentDTO.Product.Price,
                Stock = presentDTO.Product.Stock,
            };
            presentBL.Status = new Tuple<bool, string>(presentDTO.Status, presentDTO.Message);
            if (this.presentListService.UpdatePresent(presentBL))
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