using JalaFoundation.Dev23.Wedding.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JalaFoundation.Dev23.Wedding.BL.Models;
using JalaFoundation.Dev23.Wedding.DAL.Interfaces;
using JalaFoundation.Dev23.Wedding.DAL.Repositories;
using JalaFoundation.Dev23.Wedding.DAL.Models;

namespace JalaFoundation.Dev23.Wedding.BL.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IAccountsRepository _accountRepository;
        public AccountsService()
        {
            _accountRepository = new AccountsRepository();
        }
        public int AddAccount(AccountBusiness accountBusiness)
        {
            var account = new Account()
            {
                Name = accountBusiness.Name,
                Password = accountBusiness.Password,
                CoupleID = accountBusiness.CoupleID,
                Token = accountBusiness.Token
            };

            return _accountRepository.Add(account);
        }

        public AccountBusiness Authenticate(string username, string password)
        {
            Account account = _accountRepository.isValid(username, password);

            if (account != null)
            {
                return new AccountBusiness()
                {
                    Id = account.AccountID,
                    Name = account.Name,
                    Password = account.Password,
                    Token = account.Token,
                    ProductListId = (int) account.Couple.PresentListID,
                    WifeName = account.Couple.Wife.FirstName,
                    HusbandName = account.Couple.Husband.FirstName,
                    WeddingDate = account.Couple.WeddingDate
                };
            }

            return null;
        }
    }
}
