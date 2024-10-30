using MultiBankOOP.Infrastructure.Contracts;
using MultiBankOOP.Infrastructure.Contracts.Entities;
using MultiBankOOP.Infrastructure.Contracts.Entities;
using MultiBankOOP.Infrastructure.Contracts;

namespace MultiBankOOP.Infrastructure.Impl
{
    public class MovementsRepository : IMovementsRepository
    {

        private static Dictionary<string, List<MovementEntity>> simulatedMovementsDBTable = new()
        {
            {"1000", new List<MovementEntity>()},
            {"2000", new List<MovementEntity>()},
            {"3000", new List<MovementEntity>()},
        };

        public List<MovementEntity> GetMovements(string user)
        {
            return simulatedMovementsDBTable[user];
        }

        public void AddMovement(string user, MovementEntity newMovement)
        {
            List <MovementEntity> movs = simulatedMovementsDBTable[user];
            movs.Add(newMovement);
        }
    }
}
