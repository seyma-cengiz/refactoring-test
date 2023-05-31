using LegacyApp.Entities;
using LegacyApp.Repositories.Interfaces;
using Moq;

namespace LegacyApp.Tests.Mocks;
public class UserRepositoryMock
{
    public static Mock<IUserRepository> GetMock()
    {
        var mock = new Mock<IUserRepository>();

        mock.Setup(t => t.AddUser(It.IsAny<User>()));
        return mock;
    }
}
