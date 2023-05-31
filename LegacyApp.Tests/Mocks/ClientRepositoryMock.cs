using LegacyApp.Entities;
using LegacyApp.Repositories.Interfaces;
using Moq;

namespace LegacyApp.Tests.Mocks;
public class ClientRepositoryMock
{
    public static Mock<IClientRepository> GetMock(Client expectedResult)
    {
        var mock = new Mock<IClientRepository>();

        mock.Setup(t => t.GetById(It.IsAny<int>())).Returns(() => expectedResult);
        return mock;
    }
}
