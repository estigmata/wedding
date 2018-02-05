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
    public class PersonsService : IPersonsService
    {
        private readonly IPersonsRepository _personRepository;
        public PersonsService()
        {
            _personRepository = new PersonsRepository();
        }
        public int AddPerson(PersonBusiness personBusiness)
        {
            var person = new Person()
            {
                CI = personBusiness.CI,
                Direction = personBusiness.Direction,
                FirstName = personBusiness.FirstName,
                LastName = personBusiness.LastName,
                Mail = personBusiness.Mail,
                Telephone = personBusiness.Telephone
            };

            return _personRepository.Add(person);
        }
    }
}
