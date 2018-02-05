using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JalaFoundation.Dev23.Wedding.DAL.Interfaces;
using JalaFoundation.Dev23.Wedding.DAL.Models;

namespace JalaFoundation.Dev23.Wedding.DAL.Repositories
{
    public class PresentRepository : IPresentRepository
    {
 
        public bool AddPresentList(List<Present> presents)
        {
            using (WeddingContext WeddingContext = new WeddingContext())
            {
                int changes = 0;

                foreach (var present in presents)
                {
                    WeddingContext.Presents.Add(present);

                    changes += WeddingContext.SaveChanges();
                }
          
                return changes > 0;
            };         
        }

        public bool DeletePresent(int presentListID, int productID)
        {
            using (WeddingContext WeddingContext = new WeddingContext())
            {
                var productDelete = WeddingContext.Presents.Where(present => present.PresentListID == presentListID && present.ProductID == productID).First();
                WeddingContext.Presents.Remove(productDelete);

                return WeddingContext.SaveChanges() > 0;
            };
        }

        public List<Present> GetPresents(int PresentListID)
        {
            List<Present> result = new List<Present>();
            
            using (WeddingContext WeddingContext = new WeddingContext())
            {
                result = WeddingContext.Presents
                    .Where(x => x.PresentListID == PresentListID)
                    .ToList().Select(x=> new Present() {
                        ProductID = x.ProductID,
                        PresentListID = x.PresentListID,
                        PresentID = x.PresentID,
                        Status = x.Status,
                        Product = new Product()
                        {
                            Id = x.Product.Id,
                            Brand = x.Product.Brand,
                            Category = x.Product.Category,
                            Description = x.Product.Description,
                            ImageName = x.Product.ImageName,
                            Name = x.Product.Name,
                            Price = x.Product.Price,
                            Stock = x.Product.Stock
                        }
                    }).ToList();
                return result;
            }
        }


        public Present GetPresent(int PresentID)
        {
            using (WeddingContext WeddingContext = new WeddingContext())
            {
                var result = WeddingContext.Presents
                    .Where(x => x.PresentID == PresentID)
                    .ToList().Select(x => new Present()
                    {
                        ProductID = x.ProductID,
                        PresentListID = x.PresentListID,
                        PresentID = x.PresentID,
                        Status = x.Status,
                        Product = new Product()
                        {
                            Id = x.Product.Id,
                            Brand = x.Product.Brand,
                            Category = x.Product.Category,
                            Description = x.Product.Description,
                            ImageName = x.Product.ImageName,
                            Name = x.Product.Name,
                            Price = x.Product.Price,
                            Stock = x.Product.Stock
                        }
                    }).ToList().FirstOrDefault();
                return result;
            }
        }

        public Present UpdatePresent(Present present)
        {
            using (WeddingContext WeddingContext = new WeddingContext())
            {
                var updatePresentProductList = WeddingContext.Presents.Find(present.PresentID);
                updatePresentProductList.Status = present.Status;
                WeddingContext.SaveChanges();
                return updatePresentProductList;
            }
        }
    }
}
