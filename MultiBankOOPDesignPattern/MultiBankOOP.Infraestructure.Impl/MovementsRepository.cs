using MultiBankOOP.Infrastructure.Contracts;
using MultiBankOOP.Infrastructure.Contracts.Entities;
using MultiBankOOP.Infrastructure.Contracts.Entities;
using MultiBankOOP.Infrastructure.Contracts;

namespace MultiBankOOP.Infrastructure.Impl
{
    public class MovementsRepository : IMovementsRepository
    {
        private static List<MovementEntity> simulatedMovementsDBTable = new();

        public List<MovementEntity> GetMovements()
        {
            return simulatedMovementsDBTable;
        }

        public void AddMovement(MovementEntity newMovement)
        {
            simulatedMovementsDBTable.Add(newMovement);
        }
    }
}
