using LegacyApp.Entities;
using LegacyApp.Models.Interfaces;
using LegacyApp.Services;

namespace LegacyApp.Models
{
    public class ImportantClientCredit : IClientCredit
    {
        private readonly IUserCreditService _userCreditService;
        public ImportantClientCredit(IUserCreditService userCreditService)
        {
            _userCreditService = userCreditService;
        }
        public void CalculateCredit(ref User user)
        {
            user.HasCreditLimit = true;
            var creditLimit = _userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
            creditLimit = creditLimit * 2;
            user.CreditLimit = creditLimit;
        }
    }
}
