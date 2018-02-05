using JalaFoundation.Dev23.Wedding.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JalaFoundation.Dev23.Wedding.BL.Models;
using JalaFoundation.Dev23.Wedding.DAL.Interfaces;
using JalaFoundation.Dev23.Wedding.DAL.Repositories;
using JalaFoundation.Dev23.Wedding.DAL.Models;

namespace JalaFoundation.Dev23.Wedding.BL.Services
{
    public class CouplesService : ICouplesService
    {
        private readonly ICouplesRepository _coupleRepository;

        public CouplesService()
        {
            _coupleRepository = new CouplesRepository();
        }
        public int AddCouple(CoupleBusiness coupleBusiness)
        {
            var couple = new Couple()
            {
                Address = coupleBusiness.Address,
                DeliveryDate = coupleBusiness.DeliveryDate,
                WeddingDate = coupleBusiness.WeddingDate,
                HusbandID = coupleBusiness.HusbandID,
                WifeID = coupleBusiness.WifeID,
                PresentListID = coupleBusiness.PresentListID,
                Latitude = coupleBusiness.Latitude,
                Longitude = coupleBusiness.Longitude
            };

            return _coupleRepository.Add(couple);
        }
    }
}
