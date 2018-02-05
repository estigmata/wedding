using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JalaFoundation.Dev23.Wedding.DAL.Models
{
    public class PresentList
    {
        [Key]
        [Required]
        public int PresentListID { get; set; }

        [Required]
        public int Quantity { get; set; }

        public ICollection<Present> Presents { get; set; }
    }
}
