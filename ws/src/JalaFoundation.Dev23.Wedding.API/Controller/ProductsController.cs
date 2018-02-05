using JalaFoundation.Dev23.Wedding.API.DTO;
using JalaFoundation.Dev23.Wedding.API.Models;
using JalaFoundation.Dev23.Wedding.BL.Interfaces;
using JalaFoundation.Dev23.Wedding.BL.Models;
using JalaFoundation.Dev23.Wedding.BL.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using JalaFoundation.Dev23.Wedding.API.Filters;

namespace JalaFoundation.Dev23.Wedding.API.Controller
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductsController : ApiController
    {
        private readonly IProductsService _productService;

        public ProductsController()
        {
            _productService = new ProductsService();    
        }

        // GET: api/Products
        public List<ProductDTO> Get()
        {
            List<ProductDTO> productsDTO = new List<ProductDTO>();

            foreach (var product in _productService.GetProducts())
            {
                productsDTO.Add(new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Category = product.Category,
                    Brand = product.Brand,
                    ImageName = product.ImageName,
                    Stock = product.Stock,
                    Image = product.Image
                });
            }
            return productsDTO;
        }

        // POST: api/Products
        [BasicAuthentication]
        public async Task<HttpResponseMessage> Post()
        {
            var category = HttpContext.Current.Request.Form.Get("category");
            string uploadRootPath = HttpContext.Current.Server.MapPath("~/App_Data");
            var uploadPath = Path.Combine(uploadRootPath, category);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            ImageStreamProvider streamProvider = new ImageStreamProvider(uploadPath);

            await Request.Content.ReadAsMultipartAsync(streamProvider);

            FileInfo fileInfo = new FileInfo(streamProvider.FileData[0].LocalFileName);

            ProductBusiness productBusiness = new ProductBusiness()
            {
                Name = streamProvider.FormData.Get("name"),
                Stock = Convert.ToInt32(streamProvider.FormData.Get("stock")),
                Brand = streamProvider.FormData.Get("brand"),
                Category = streamProvider.FormData.Get("category"),
                Description = streamProvider.FormData.Get("description"),
                Price = Convert.ToDouble(streamProvider.FormData.Get("price")),
                ImageName = fileInfo.Name
            };
     
            if (_productService.AddProduct(productBusiness))
            {
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        //PUT: api/Products
        public ProductDTO Put(UpdateProductDTO product)
        {

            ProductBusiness productBusiness = new ProductBusiness()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                Brand = product.Brand,
                ImageName = product.ImageName,
                Stock = product.Stock
            };

            var updatedProduct = _productService.UpdateStockProduct(productBusiness , product.NewStock);

           
            ProductDTO productDTO = new ProductDTO()
            {
                Id = updatedProduct.Id,
                Name = updatedProduct.Name,
                Description = updatedProduct.Description,
                Price = updatedProduct.Price,
                Category = updatedProduct.Category,
                Brand = updatedProduct.Brand,
                ImageName = updatedProduct.ImageName,
                Stock = updatedProduct.Stock
            };

            return productDTO; 
        }
    }
}
