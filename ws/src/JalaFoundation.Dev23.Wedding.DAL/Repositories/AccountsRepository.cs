using JalaFoundation.Dev23.Wedding.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JalaFoundation.Dev23.Wedding.DAL.Models;

namespace JalaFoundation.Dev23.Wedding.DAL.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly WeddingContext weddingContext;

        public AccountsRepository()
        {
            weddingContext = new WeddingContext();
        }

        public int Add(Account account)
        {
            var accountSaved = weddingContext.Accounts.Add(account);
            weddingContext.SaveChanges();
            return accountSaved.AccountID;
        }

        public Account isValid(string username, string password)
        {
            Account account = weddingContext.Accounts
                .SingleOrDefault(a => a.Name == username && a.Password == password);

            if (account != null)
            {
                Couple couple = weddingContext.Couples
                    .Include(c => c.Husband)
                    .Include(c => c.Wife)
                    .SingleOrDefault(c => c.CoupleID == account.CoupleID);

                account.Couple = couple;

                if (DateTime.Now > couple.DeliveryDate.AddDays(14))
                {
                    account.Password = "expired";
                }
                return account;
            }

            return null;
        }
    }
}
