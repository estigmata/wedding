using JalaFoundation.Dev23.Wedding.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using JalaFoundation.Dev23.Wedding.BL.Models;
using JalaFoundation.Dev23.Wedding.BL.Services;
using JalaFoundation.Dev23.Wedding.DAL.Interfaces;
using JalaFoundation.Dev23.Wedding.DAL.Repositories;
using JalaFoundation.Dev23.Wedding.DAL.Models;

namespace JalaFoundation.Dev23.Wedding.BL.Services
{
    public class PresentListService : IPresentListService
    {
        private readonly IPresentListRepository presentListRepository;
        private readonly IPresentRepository presentRepository;
        private readonly IProductsRepository productsRepository;
        private readonly IDedicatoryRepository dedicatoryRepository;
        private readonly IProductsService productsService;

        public PresentListService()
        {
            this.presentListRepository = new PresentListRepository();
            this.presentRepository = new PresentRepository();
            this.productsRepository = new ProductsRepository();
            this.dedicatoryRepository = new DedicatoryRepository();
            this.productsService = new ProductsService();
        }

        public int Add(PresentListBusiness presentList)
        {
            var newPresentlist = new PresentList()
            {
                Quantity = presentList.Quantity
            };

            return this.presentListRepository.Add(newPresentlist);
        }

        public List<PresentListBusiness> SearchPresentsList(string firstName, string lastName)
        {
            var result = this.presentListRepository.SearchPresentsList(firstName, lastName);
            return result.Select(x => new PresentListBusiness()
            {
                PresentlistID = x.PresentListID,
                Husband = new PersonBusiness()
                {
                    FirstName = x.Husband.FirstName,
                    LastName = x.Husband.LastName
                }, 
                Wife = new PersonBusiness()
                {
                    FirstName = x.Wife.FirstName,
                    LastName = x.Wife.LastName
                },
                WeddingDate = x.WeddingDate,
                DeliveryDate = x.DeliveryDate,
                Status = this.VerifyStatus(x.WeddingDate, x.DeliveryDate)
            }).ToList();
        }

        public List<PresentBusiness> GetPresents(int id)
        {
            string imagesPath = HttpContext.Current.Server.MapPath("~/App_Data");
            List<Present> presents = presentRepository.GetPresents(id);
            List<PresentBusiness> presentsBusinesses = new List<PresentBusiness>();

            foreach (Present present in presents)
            {
                if (present.Product.ImageName == "")
                {
                    present.Product.ImageName = "not-found.png";
                }

                var imagePath = Path.Combine(imagesPath, present.Product.Category, present.Product.ImageName);

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

                presentsBusinesses.Add(new PresentBusiness()
                {
                    PresentList = null,
                    PresentID = present.PresentID,
                    Product = new ProductBusiness()
                    {
                        Id = present.ProductID,
                        Name = present.Product.Name,
                        Price = present.Product.Price,
                        Brand = present.Product.Brand,
                        Category = present.Product.Category,
                        Description = present.Product.Description,
                        ImageName = present.Product.ImageName,
                        Stock = present.Product.Stock,
                        Image = imageBase64
                    },
                    Status = GetProductStatus(present.ProductID, present.Status)
                });
            }

            return presentsBusinesses;
        }

        private string VerifyStatus(DateTime weddingDate, DateTime deliveryDate)
        {
            if (deliveryDate <= DateTime.Today.AddDays(1))
            {
                return "Not Available: The Presents are already on Delivering Proccess";
            }

            if (weddingDate <= DateTime.Today.AddDays(-7))
            {
                return "Not Available: Already Passed a Week From the Wedding Day";
            }

            return "Available: You can access to the Present List";            
        }

        private Tuple<bool, string> GetProductStatus(int productID, bool status)
        {
            var product = productsRepository.GetProduct(productID);
            if (status)
            {
                return new Tuple<bool, string>(false, "Already Bought");
            }

            if (product.Stock == 0)
            {
                return new Tuple<bool, string>(false, "Out Of Stock");
            }

            return new Tuple<bool, string>(true, "Available");
        }

        public bool AddProducts(List<int> products, int idList)
        {
            List<Present> productsList = new List<Present>();

            foreach (var product in products)
            {
                var presentListProduct = new Present()
                {
                    PresentListID = idList,
                    ProductID = product,
                    Status = false
                };

                productsList.Add(presentListProduct);
            }
            return presentRepository.AddPresentList(productsList);
        }

        public bool UpdatePresent(PresentBusiness presentBusiness)
        {
            productsService.UpdateStockProduct(presentBusiness.Product);
            var presentDAL = presentRepository.GetPresent(presentBusiness.PresentID);
            presentDAL.Status = true;
            var updatePresentListProduct = presentRepository.UpdatePresent(presentDAL);
            return updatePresentListProduct.Status;
        }

        public bool DeletePresent(int presentListID, int productID)
        {
            return presentRepository.DeletePresent(presentListID, productID);
        }

        public bool AddDedicatory(string name, string message, int PresentID)
        {
            return this.dedicatoryRepository.Add(new Dedicatory()
            {
                Name = name,
                Message = message,
                PresentID = PresentID
            });
        }

        public PresentListBusiness GetPresentList(int id)
        {
            var presentList = presentListRepository.GetPresents(id);

            return new PresentListBusiness()
            {
                PresentlistID = presentList.PresentListID,
                Quantity = presentList.Quantity,
                Presents = presentList.Presents.Select(p => new PresentBusiness()
                {
                    PresentID = p.PresentID,
                    Product = new ProductBusiness()
                    {
                        Id = p.Product.Id,
                        Name = p.Product.Name,
                        Description = p.Product.Description,
                        Brand = p.Product.Brand,
                        Category = p.Product.Category,
                        Stock = p.Product.Stock
                    }
                }).ToList()
            };
        }

        public bool UpdatePresentList(List<int> products, int idList)
        {
            var newPresentlist = new PresentList()
            {
                PresentListID = idList,
                Presents = products.Select(id => new Present()
                {
                    PresentListID = idList,
                    ProductID = id
                }).ToList()
            };

            return presentListRepository.Update(newPresentlist);
        }
    }
}
