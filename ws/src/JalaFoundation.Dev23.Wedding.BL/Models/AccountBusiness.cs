using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JalaFoundation.Dev23.Wedding.BL.Models
{
    public class AccountBusiness
    {
        public string Name;
        public string Password;
        public int CoupleID;
        public string Token;
        public int ProductListId { get; set; }
        public int Id { get; set; }
        public string WifeName { get; set; }
        public string HusbandName { get; set; }
        public DateTime WeddingDate { get; set; }

        public AccountBusiness()
        {
            
        }

        public AccountBusiness(PersonBusiness person, PersonBusiness personPartner, int CoupleID)
        {
            this.Name = AccountGenerator(person.FirstName, person.LastName, person.CI, personPartner.FirstName, personPartner.LastName, personPartner.CI);
            this.Password = PasswordGenerator();
            this.CoupleID = CoupleID;
            this.Token = Convert.ToBase64String(Encoding.UTF8.GetBytes(this.Name + ":" + this.Password));
        }

        private string PasswordGenerator()
        {
            Random randomSelector = new Random();

            string alfabeticCharacterUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string alfabeticCharacterLower = "abcdefghijklmnopqrstyvwxyz";
            string numbers = "0123456789";

            string password = "";
            int selector;

            for (int i = 0; i < 9; i++)
            {
                if (i == 3 || i == 4 || i == 5)
                {
                    selector = randomSelector.Next(0, 10);
                    password = password + numbers[selector];
                }
                else if (i == 0 || i == 1 || i == 2)
                {
                    selector = randomSelector.Next(0, 26);
                    password = password + alfabeticCharacterUpper[selector];
                }
                else if (i == 6 || i == 7 || i == 8)
                {
                    selector = randomSelector.Next(0, 26);
                    password = password + alfabeticCharacterLower[selector];
                }
            }

            return password;
        }

        private string AccountGenerator(string personName, string personLastName, int personID, string personPartnerName, string personPartnerLastname, int personPartnerID)
        {
            string account = "";

            account = account + personName.Substring(0, 1);
            account = account + personName.Substring(personName.Length - 1, 1);

            account = account + personLastName.Substring(0, 1);
            account = account + personLastName.Substring(personLastName.Length - 1, 1);

            account = account + personPartnerName.Substring(0, 1);
            account = account + personPartnerName.Substring(personPartnerName.Length - 1, 1);

            account = account + personPartnerLastname.Substring(0, 1);
            account = account + personPartnerLastname.Substring(personPartnerLastname.Length - 1, 1);

            account = account + personID.ToString().Substring(0, 1);
            account = account + personID.ToString().Substring(personID.ToString().Length - 1, 1);

            account = account + personPartnerID.ToString().Substring(0, 1);
            account = account + personPartnerID.ToString().Substring(personPartnerID.ToString().Length - 1, 1);

            return account.ToUpper();
        }
    }
}
