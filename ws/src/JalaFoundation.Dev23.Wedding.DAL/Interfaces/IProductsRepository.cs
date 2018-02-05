using JalaFoundation.Dev23.Wedding.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JalaFoundation.Dev23.Wedding.DAL.Interfaces
{
    public interface IProductsRepository
    {
        bool Add(Product product);
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(int ProductID);
        Product UpdateProduct(Product product);
        Product UpdateProduct(Product product, int quantity);
    }
}
