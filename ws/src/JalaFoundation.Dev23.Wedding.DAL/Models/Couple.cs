using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JalaFoundation.Dev23.Wedding.DAL.Models
{
    public class Couple
    {
        [Key]

        public int CoupleID { get; set; }
        public DateTime WeddingDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Address { get; set; }
        public int? WifeID { get; set; }
        public int? HusbandID { get; set; }
        public int? PresentListID { get; set; }
        
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        [ForeignKey("WifeID")]
        public virtual Person Wife { get; set; }

        [ForeignKey("HusbandID")]
        public virtual Person Husband { get; set; }
        [ForeignKey("PresentListID")]
        public virtual PresentList PresentList { get; set; }
    }
}
