using LegacyApp.Constants;
using LegacyApp.Entities;
using LegacyApp.Models;
using LegacyApp.Repositories.Interfaces;
using LegacyApp.Services;
using LegacyApp.Tests.Mocks;
using Moq;
using System.Collections;

namespace LegacyApp.Tests.ServiceTests;

[TestFixture]
public class UserServiceTests
{
    private Mock<IUserRepository> _userRepositoryMock;
    private UserService _userService;

    private void SetupForDifferentScenarios(string clientName, int creditLimit)
    {
        var expectedResult = new Client
        {
            Id = 1,
            Name = clientName,
            ClientStatus = ClientStatus.none
        };

        _userRepositoryMock = UserRepositoryMock.GetMock();
        var _clientRepositoryMock = ClientRepositoryMock.GetMock(expectedResult);
        var _clientCreditFactoryMock = ClientCreditFactoryMock.GetMock(clientName, creditLimit);

        _userService = new UserService(_clientRepositoryMock.Object, _userRepositoryMock.Object, _clientCreditFactoryMock.Object);
    }

    [TestCase("", "Test", "test@gmail.com")]
    [TestCase("Test", "", "test@gmail.com")]
    [TestCase("Test", "", "testgmail.com")]
    [TestCase("Test", "", "test@gmailcom")]
    public void AddUser_WhenParameterIsNotValid_ReturnsFalse(string firstName, string surname, string email)
    {
        //Setup
        SetupForDifferentScenarios(ClientName.Regular, 500);
        //Act
        var result = _userService.AddUser(firstName, surname, email, new DateTime(2000, 1, 1), 1);
        //Assert
        Assert.That(result, Is.False);
    }

    [TestCaseSource(typeof(UserDateOFBirthSource))]
    public void AddUser_WhenUserBirthOfDateIsNotValid_ReturnsFalse(DateTime dateOfBirth)
    {
        //Setup
        SetupForDifferentScenarios(ClientName.Regular, 500);
        //Act
        var result = _userService.AddUser("Test", "Test", "test@gmail.com", dateOfBirth, 1);
        //Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void AddUser_WhenClientIsRegularAndCreditLimitLessThan500_ReturnsFalse()
    {
        //Setup
        SetupForDifferentScenarios(ClientName.Regular, 499);
        //Act
        var result = _userService.AddUser("Test", "Test", "test@gmail.com", new DateTime(2000, 1, 1), 1);
        //Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void AddUser_WhenClientIsRegularAndCreditLimitMoreThan500_ReturnsTrue()
    {
        //Setup
        SetupForDifferentScenarios(ClientName.Regular, 500);
        //Act
        var result = _userService.AddUser("Test", "Test", "test@gmail.com", new DateTime(2000, 1, 1), 1);
        //Assert
        _userRepositoryMock.Verify(t => t.AddUser(It.IsAny<User>()), Times.Once);
        Assert.That(result, Is.True);
    }

    [Test]
    public void AddUser_WhenClientIsImportantAndCreditLimitLessThan500_ReturnsFalse()
    {
        //Setup
        SetupForDifferentScenarios(ClientName.ImportantClient, 249);
        //Act
        var result = _userService.AddUser("Test", "Test", "test@gmail.com", new DateTime(2000, 1, 1), 1);
        //Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void AddUser_WhenClientIsImportantAndCreditLimitMoreThan500_ReturnsTrue()
    {
        //Setup
        SetupForDifferentScenarios(ClientName.ImportantClient, 250);
        //Act
        var result = _userService.AddUser("Test", "Test", "test@gmail.com", new DateTime(2000, 1, 1), 1);
        //Assert
        _userRepositoryMock.Verify(t => t.AddUser(It.IsAny<User>()), Times.Once);
        Assert.That(result, Is.True);
    }

    [Test]
    public void AddUser_WhenClientIsVeyImportantAndCreditLimitLessThan500_AddsUser()
    {
        //Setup
        SetupForDifferentScenarios(ClientName.VeryImportantClient, 499);
        //Act
        var result = _userService.AddUser("Test", "Test", "test@gmail.com", new DateTime(2000, 1, 1), 1);
        //Assert
        _userRepositoryMock.Verify(t => t.AddUser(It.IsAny<User>()), Times.Once);
        Assert.That(result, Is.True);
    }

    [Test]
    public void AddUser_WhenClientIsImportantAndCreditLimitMoreThan500_AddsUser()
    {
        //Setup
        SetupForDifferentScenarios(ClientName.VeryImportantClient, 500);
        //Act
        var result = _userService.AddUser("Test", "Test", "test@gmail.com", new DateTime(2000, 1, 1), 1);
        //Assert
        _userRepositoryMock.Verify(t => t.AddUser(It.IsAny<User>()), Times.Once);
        Assert.That(result, Is.True);
    }

}

public class UserDateOFBirthSource : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        var now = DateTime.Now;
        yield return new DateTime(now.Year - 20, 1, 1);
        yield return new DateTime(now.Year - 21, now.Month == 12 ? 1 : now.Month + 1, 1);
        var days = DateTime.DaysInMonth(now.Year, now.Month);
        yield return new DateTime(now.Year - 21, now.Day == days ? now.Month + 1 : now.Month, now.Day == days ? 1 : now.Day + 1);
    }
}