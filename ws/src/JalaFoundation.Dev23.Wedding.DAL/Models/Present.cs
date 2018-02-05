using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JalaFoundation.Dev23.Wedding.DAL.Models
{
    public class Present
    {
        [Key]
        public int PresentID { get; set; }
        public int PresentListID { get; set; }
        public int ProductID { get; set; }
        public bool Status { get; set; }
        [ForeignKey("PresentListID")]
        public virtual PresentList PresentList { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
