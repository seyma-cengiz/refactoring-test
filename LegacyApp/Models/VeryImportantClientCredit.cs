using LegacyApp.Entities;
using LegacyApp.Models.Interfaces;

namespace LegacyApp.Models
{
    public class VeryImportantClientCredit : IClientCredit
    {
        public void CalculateCredit(ref User user)
        {
            user.HasCreditLimit = false;
        }
    }
}
