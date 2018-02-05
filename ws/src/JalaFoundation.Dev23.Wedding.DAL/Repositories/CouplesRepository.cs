using JalaFoundation.Dev23.Wedding.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JalaFoundation.Dev23.Wedding.DAL.Models;

namespace JalaFoundation.Dev23.Wedding.DAL.Repositories
{
    public class CouplesRepository : ICouplesRepository
    {
        WeddingContext weddingContext;

        public CouplesRepository()
        {
            weddingContext = new WeddingContext();
        }

        public int Add(Couple couple)
        {
            var coupleSaved = weddingContext.Couples.Add(couple);
            weddingContext.SaveChanges();
            return coupleSaved.CoupleID;
        }

        public Account IsValid(string account, string password)
        {
            throw new NotImplementedException();
        }
    }
}
