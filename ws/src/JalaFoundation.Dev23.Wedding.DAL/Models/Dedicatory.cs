using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JalaFoundation.Dev23.Wedding.DAL.Models
{
    public class Dedicatory
    {
        [Key]
        public int DedicatoryID { set; get; }
        [Required]
        public string Name { set; get; }
        public string Message { set; get; }
        public int PresentID { set; get; }

        [ForeignKey("PresentID")]
        public virtual Present Present { get; set; }
    }
}
