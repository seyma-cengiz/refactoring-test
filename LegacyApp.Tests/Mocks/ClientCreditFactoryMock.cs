using LegacyApp.Constants;
using LegacyApp.Models;
using LegacyApp.Models.Interfaces;
using LegacyApp.Services;
using Moq;

namespace LegacyApp.Tests.Mocks;
public class ClientCreditFactoryMock
{
    public static Mock<IClientCreditFactory> GetMock(string clientName, int creditLimit)
    {
        var userCreditServiceMock = new Mock<IUserCreditService>();
        userCreditServiceMock.Setup(t => t.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(() => creditLimit);

        IClientCredit expectedClientCreditResult;
        if (clientName == ClientName.VeryImportantClient)
            expectedClientCreditResult = new VeryImportantClientCredit();
        else if (clientName == ClientName.ImportantClient)
            expectedClientCreditResult = new ImportantClientCredit(userCreditServiceMock.Object);
        else
            expectedClientCreditResult = new ClientCredit(userCreditServiceMock.Object);

        var clientCreditFactoryMock = new Mock<IClientCreditFactory>();

        clientCreditFactoryMock.Setup(t => t.CreateClientCredit(It.IsAny<string>())).Returns(() => expectedClientCreditResult);
        return clientCreditFactoryMock;
    }
}
