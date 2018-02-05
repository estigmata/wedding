using JalaFoundation.Dev23.Wedding.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JalaFoundation.Dev23.Wedding.BL.Interfaces
{
    public interface IAccountsService
    {
        int AddAccount(AccountBusiness account);
        AccountBusiness Authenticate(string username, string password);
    }
}
