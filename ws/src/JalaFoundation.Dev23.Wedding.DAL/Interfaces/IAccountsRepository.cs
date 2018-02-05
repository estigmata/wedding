using JalaFoundation.Dev23.Wedding.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JalaFoundation.Dev23.Wedding.DAL.Interfaces
{
    public interface IAccountsRepository
    {
        int Add(Account account);
        Account isValid(string username, string password);
    }
}
