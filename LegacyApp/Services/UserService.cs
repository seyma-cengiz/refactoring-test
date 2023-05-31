using LegacyApp.Entities;
using LegacyApp.Extensions;
using LegacyApp.Repositories;
using LegacyApp.Repositories.Interfaces;
using System;

namespace LegacyApp.Services
{
    public class UserService
    {
        private readonly IClientCreditFactory _clientCredit;
        private readonly IUserRepository _userRepository;
        private readonly IClientRepository _clientRepository;
        public UserService() : this(new ClientRepository(),
                                    new UserRepository(),
                                    new ClientCreditFactory())
        { }

        public UserService(IClientRepository clientRepository,
                           IUserRepository userRepository,
                           IClientCreditFactory clientCredit)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            _clientCredit = clientCredit;
        }

        public bool AddUser(string firstName, string surname, string email, DateTime dateOfBirth, int clientId)
        {

            if (!IsValid(firstName, surname, email, dateOfBirth)) return false;

            var client = _clientRepository.GetById(clientId);
            var user = CreateUser(client, firstName, surname, email, dateOfBirth);

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            _userRepository.AddUser(user);

            return true;
        }

        private User CreateUser(Client client, string firstName, string surname, string email, DateTime dateOfBirth)
        {
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = firstName,
                Surname = surname
            };

            var clientCredit = _clientCredit.CreateClientCredit(user.Client.Name);
            clientCredit.CalculateCredit(ref user);

            return user;
        }

        private bool IsValid(string firstName, string surname, string email, DateTime dateOfBirth)
        {
            if (firstName.IsNullOrWhiteSpace() || surname.IsNullOrWhiteSpace() || email.IsEmailValid())
            {
                return false;
            }

            return IsUserOlderThanTwenty(dateOfBirth);
        }

        private bool IsUserOlderThanTwenty(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;

            if (now.Month < dateOfBirth.Month || now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)
            {
                age--;
            }
            return age > 20;
        }
    }
}