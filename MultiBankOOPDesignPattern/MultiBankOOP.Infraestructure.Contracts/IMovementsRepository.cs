using MultiBankOOP.Infrastructure.Contracts.Entities;
using MultiBankOOP.Infrastructure.Contracts.Entities;

namespace MultiBankOOP.Infrastructure.Contracts
{
    public interface IMovementsRepository
    {
        List<MovementEntity> GetMovements();
        void AddMovement(MovementEntity newMovement);
    }
}
