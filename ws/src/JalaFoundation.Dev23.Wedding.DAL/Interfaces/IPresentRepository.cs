﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JalaFoundation.Dev23.Wedding.DAL.Models;

namespace JalaFoundation.Dev23.Wedding.DAL.Interfaces
{
    public interface IPresentRepository
    {
        List<Present> GetPresents(int presentListID);
        bool AddPresentList(List<Present> presents);
        bool DeletePresent(int presentListID, int productID);
        Present GetPresent(int presentID);
        Present UpdatePresent(Present presentID);
    }
}
