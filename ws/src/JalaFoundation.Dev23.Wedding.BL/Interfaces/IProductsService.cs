using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JalaFoundation.Dev23.Wedding.BL.Models;
using JalaFoundation.Dev23.Wedding.DAL.Models;

namespace JalaFoundation.Dev23.Wedding.BL.Interfaces
{
    public interface IProductsService
    {
        bool AddProduct(ProductBusiness product);
        List<ProductBusiness> GetProducts();
        ProductBusiness UpdateStockProduct(ProductBusiness productBusiness);
        ProductBusiness UpdateStockProduct(ProductBusiness productBusiness, int quantity);
        
    }
}
