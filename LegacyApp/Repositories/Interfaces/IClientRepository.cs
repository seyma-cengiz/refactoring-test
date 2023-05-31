using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyApp.Entities;

namespace LegacyApp.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Client GetById(int id);
    }
}
