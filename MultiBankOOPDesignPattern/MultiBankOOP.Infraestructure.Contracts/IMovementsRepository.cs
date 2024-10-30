using MultiBankOOP.Infrastructure.Contracts.Entities;
using MultiBankOOP.Infrastructure.Contracts.Entities;
using System.Globalization;

namespace MultiBankOOP.Infrastructure.Contracts
{
    public interface IMovementsRepository
    {
        List<MovementEntity> GetMovements(string user);
        void AddMovement(string user, MovementEntity newMovement);
    }
}
