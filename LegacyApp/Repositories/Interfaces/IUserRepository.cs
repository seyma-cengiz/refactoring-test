using LegacyApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(User user);
    }
}
