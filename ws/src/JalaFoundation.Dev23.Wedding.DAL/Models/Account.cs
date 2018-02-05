using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JalaFoundation.Dev23.Wedding.DAL.Models
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int CoupleID { get; set; }
        public string Token { get; set; }


        [ForeignKey("CoupleID")]
        public virtual Couple Couple { get; set; }
    }
}
