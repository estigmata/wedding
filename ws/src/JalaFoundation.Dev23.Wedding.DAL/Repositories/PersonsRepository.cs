using JalaFoundation.Dev23.Wedding.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JalaFoundation.Dev23.Wedding.DAL.Models;

namespace JalaFoundation.Dev23.Wedding.DAL.Repositories
{
    public class PersonsRepository : IPersonsRepository
    {
        WeddingContext weddingContext;
   
        public PersonsRepository()
        {
            weddingContext = new WeddingContext();
        }

        public int Add(Person person)
        {
            var personSaved = weddingContext.Persons.Add(person);
            weddingContext.SaveChanges();
            return personSaved.PersonID;
        }
    }
}
