using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JalaFoundation.Dev23.Wedding.BL.Models;

namespace JalaFoundation.Dev23.Wedding.BL.Interfaces
{
    public interface IPresentListService
    {
        List<PresentListBusiness> SearchPresentsList(string firstName, string lastName);

        int Add(PresentListBusiness presentList);

        List<PresentBusiness> GetPresents(int id);

        bool AddProducts(List<int> products, int idList);
        bool UpdatePresent(PresentBusiness presentBusiness);

        bool AddDedicatory(string name, string message, int presentID);

        bool DeletePresent(int presentListID, int productID);
        PresentListBusiness GetPresentList(int id);
        bool UpdatePresentList(List<int> products, int idList);
    }
}
