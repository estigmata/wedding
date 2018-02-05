using JalaFoundation.Dev23.Wedding.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JalaFoundation.Dev23.Wedding.DAL.Models;

namespace JalaFoundation.Dev23.Wedding.DAL.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        readonly WeddingContext weddingContext;

        public ProductsRepository()
        {
            weddingContext = new WeddingContext();
        }

        public bool Add(Product product)
        {
            weddingContext.Products.Add(product);
            return weddingContext.SaveChanges() > 0;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return weddingContext.Products.ToList();
        }

        public Product GetProduct(int ProductID)
        {
            using (WeddingContext WeddingContext = new WeddingContext())
            {
                return WeddingContext.Products.Find(ProductID);
            }
        }
        public Product UpdateProduct(Product product)
        {
            using (WeddingContext WeddingContext = new WeddingContext())
            {
                var productFound = WeddingContext.Products.Find(product.Id);

                productFound.Stock = product.Stock;
                productFound.Brand = product.Brand;
                productFound.Category = product.Category;
                productFound.Description = product.Description;
                productFound.Id = product.Id;
                productFound.ImageName = product.ImageName;
                productFound.Name = product.Name;
                productFound.Price = product.Price;

                WeddingContext.SaveChanges();
                
                return productFound;
            }
        }
        public Product UpdateProduct(Product product, int quantity)
        {
            using (WeddingContext WeddingContext = new WeddingContext())
            {
                var productFound = WeddingContext.Products.Find(product.Id);

                productFound.Stock = quantity;

                WeddingContext.SaveChanges();

                return productFound;
            }
        }
    }
}
