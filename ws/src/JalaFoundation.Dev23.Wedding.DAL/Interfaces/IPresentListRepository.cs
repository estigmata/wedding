﻿using JalaFoundation.Dev23.Wedding.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JalaFoundation.Dev23.Wedding.DAL.Interfaces
{
    public interface IPresentListRepository
    {
        List<Couple> SearchPresentsList(string FirstName, string LastName);
        int Add(PresentList presnetList);
        PresentList GetPresents(int id);
        bool Update(PresentList presentList);
    }
}
