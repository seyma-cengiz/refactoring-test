using LegacyApp.Entities;

namespace LegacyApp.Models.Interfaces
{
    public interface IClientCredit
    {
        void CalculateCredit(ref User user);
    }
}
