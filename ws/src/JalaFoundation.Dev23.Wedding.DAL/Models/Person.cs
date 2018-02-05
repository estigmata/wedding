using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JalaFoundation.Dev23.Wedding.DAL.Models
{
    public class Person
    {
        [Key]
        public int PersonID { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public int CI { set; get; }
        public int Telephone { set; get; }
        public string Direction { set; get; }
        public string Mail { set; get; }
    }
}
