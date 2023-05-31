using LegacyApp.Models.Interfaces;

namespace LegacyApp
{
    public interface IClientCreditFactory
    {
        IClientCredit CreateClientCredit(string clientName);
    }
}
