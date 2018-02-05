using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JalaFoundation.Dev23.Wedding.BL.Interfaces;
using JalaFoundation.Dev23.Wedding.BL.Models;
using JalaFoundation.Dev23.Wedding.DAL.Interfaces;
using JalaFoundation.Dev23.Wedding.DAL.Models;
using JalaFoundation.Dev23.Wedding.DAL.Repositories;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Web;

namespace JalaFoundation.Dev23.Wedding.BL.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productRepository;

        public ProductsService()
        {
            _productRepository = new ProductsRepository();
        }

        public bool AddProduct(ProductBusiness productBusiness)
        {
            var product = new Product()
            {
                Name = productBusiness.Name,
                Description = productBusiness.Description,
                Brand = productBusiness.Brand,
                ImageName = productBusiness.ImageName,
                Category = productBusiness.Category,
                Price = productBusiness.Price,
                Stock = productBusiness.Stock
            };

            return _productRepository.Add(product);
        }


        public List<ProductBusiness> GetProducts()
        {
            string imagesPath = HttpContext.Current.Server.MapPath("~/App_Data");
            

            List<ProductBusiness> products = new List<ProductBusiness>();
            foreach(var product in _productRepository.GetAllProducts())
            {
                if (product.ImageName == "")
                {
                    product.ImageName = "not-found.png";
                }

                var imagePath = Path.Combine(imagesPath, product.Category, product.ImageName);
                string imageBase64;

                using (Image image = Image.FromFile(imagePath))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        imageBase64 = Convert.ToBase64String(imageBytes);
                    }
                }

                products.Add(new ProductBusiness
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Category = product.Category,
                    Brand = product.Brand,
                    ImageName = product.ImageName,
                    Stock = product.Stock,
                    Image = imageBase64
                });
            }
            return products;
        }
        public ProductBusiness UpdateStockProduct(ProductBusiness productBusiness)
        {
            Product product = new Product()
            {
                Brand = productBusiness.Brand,
                Category = productBusiness.Category,
                Description = productBusiness.Description,
                Id = productBusiness.Id,
                ImageName = productBusiness.ImageName,
                Name = productBusiness.Name,
                Price = productBusiness.Price,
                Stock = productBusiness.Stock - 1
            };

            var productModified = _productRepository.UpdateProduct(product);

            productBusiness.Stock = productModified.Stock;

            return productBusiness;
        }
        public ProductBusiness UpdateStockProduct(ProductBusiness productBusiness , int quantity)
        {
            Product product = new Product()
            {
                Brand = productBusiness.Brand,
                Category = productBusiness.Category,
                Description = productBusiness.Description,
                Id = productBusiness.Id,
                ImageName = productBusiness.ImageName,
                Name = productBusiness.Name,
                Price = productBusiness.Price,
                Stock = productBusiness.Stock
            };

            var currentProduct = _productRepository.GetProduct(product.Id);

            int newStock = currentProduct.Stock + quantity;

            var productModified = _productRepository.UpdateProduct(product, newStock);

            productBusiness.Stock = productModified.Stock;
            
            return productBusiness;
        }
    }
}
