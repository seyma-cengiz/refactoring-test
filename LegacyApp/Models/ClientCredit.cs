using LegacyApp.Entities;
using LegacyApp.Models.Interfaces;
using LegacyApp.Services;

namespace LegacyApp.Models
{
    public class ClientCredit : IClientCredit
    {
        private readonly IUserCreditService _userCreditService;
        public ClientCredit(IUserCreditService userCreditService)
        {
            _userCreditService = userCreditService;
        }
        public void CalculateCredit(ref User user)
        {
            user.HasCreditLimit = true;
            var creditLimit = _userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
            user.CreditLimit = creditLimit;
        }
    }
}
