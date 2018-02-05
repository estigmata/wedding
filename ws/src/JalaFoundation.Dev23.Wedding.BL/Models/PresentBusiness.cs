using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JalaFoundation.Dev23.Wedding.BL.Models
{
    public class PresentBusiness
    {
        public int PresentID { get; set; }
        public ProductBusiness Product { get; set; }
        public PresentListBusiness PresentList { get; set; }
        public Tuple<bool, string> Status { get; set; }
    }
}
