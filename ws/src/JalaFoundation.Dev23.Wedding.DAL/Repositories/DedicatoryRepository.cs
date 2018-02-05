using JalaFoundation.Dev23.Wedding.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JalaFoundation.Dev23.Wedding.DAL.Models;
namespace JalaFoundation.Dev23.Wedding.DAL.Repositories
{
    public class DedicatoryRepository : IDedicatoryRepository
    {
        public bool Add(Dedicatory dedicatory)
        {
            using (WeddingContext weddingContext = new WeddingContext())
            {
                weddingContext.Dedicatories.Add(dedicatory);
                weddingContext.SaveChanges();
                return dedicatory.DedicatoryID > 0;
            }
        }
    }
}
