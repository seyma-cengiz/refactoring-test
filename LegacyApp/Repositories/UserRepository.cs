using LegacyApp.DataAccess;
using LegacyApp.Entities;
using LegacyApp.Repositories.Interfaces;

namespace LegacyApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        public void AddUser(User user)
        {
            //to make this static method testable
            UserDataAccess.AddUser(user);
        }
    }
}
