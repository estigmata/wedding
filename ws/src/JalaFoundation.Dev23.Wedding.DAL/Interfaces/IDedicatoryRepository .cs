using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JalaFoundation.Dev23.Wedding.DAL.Models;

namespace JalaFoundation.Dev23.Wedding.DAL.Interfaces
{
    public interface IDedicatoryRepository
    {
        bool Add(Dedicatory dedicatory);
    }
}
