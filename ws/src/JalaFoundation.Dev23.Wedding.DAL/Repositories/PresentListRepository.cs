using JalaFoundation.Dev23.Wedding.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JalaFoundation.Dev23.Wedding.DAL.Models;
using System.Data.Entity;
using System.Diagnostics;

namespace JalaFoundation.Dev23.Wedding.DAL.Repositories
{
    public class PresentListRepository : IPresentListRepository
    {
        WeddingContext weddingContext;

        public PresentListRepository()
        {
            weddingContext = new WeddingContext();
        }

        public int Add(PresentList presentList)
        {
            var newPresentList = weddingContext.PresentLists.Add(presentList);
            weddingContext.SaveChanges();
            return newPresentList.PresentListID;
        }

        public PresentList GetPresents(int id)
        {
            return weddingContext.PresentLists
                .Include(p => p.Presents.Select(pr => pr.Product))
                .SingleOrDefault(p => p.PresentListID == id);
        }

        public List<Couple> SearchPresentsList(string FirstName, string LastName)
        {
            return weddingContext.Couples.Where(x => (x.Husband.FirstName == FirstName && x.Husband.LastName == LastName) || (x.Wife.FirstName == FirstName && x.Wife.LastName
             == LastName)).ToList();
        }

        public bool Update(PresentList presentList)
        {
            for (int i = 0; i < presentList.Presents.Count; i++)
            {
                var present = presentList.Presents.ElementAt(i);
                if (!weddingContext.Presents.Any(p => p.PresentListID == present.PresentListID && p.ProductID == present.ProductID))
                {
                    weddingContext.Presents.Add(present);
                }
            }

            return weddingContext.SaveChanges() > 0;
        }
    }
}
