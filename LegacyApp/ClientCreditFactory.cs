using LegacyApp.Constants;
using LegacyApp.Models;
using LegacyApp.Models.Interfaces;
using LegacyApp.Services;

namespace LegacyApp
{
    public class ClientCreditFactory : IClientCreditFactory
    {
        private IClientCredit client;
        public IClientCredit CreateClientCredit(string clientName)
        {
            if (clientName == ClientName.VeryImportantClient)
                client = new VeryImportantClientCredit();
            else if (clientName == ClientName.ImportantClient)
                client = new ImportantClientCredit(new UserCreditServiceClient());
            else
                client = new ClientCredit(new UserCreditServiceClient());
            return client;

        }
    }
}
