using Universities.Infraestructure.Contracts.JSONEntities;

namespace Universities.Infraestructure.Contracts
{
    public interface IAPIRepository
    {
        public Task<JSONListUniversityEntities> GetApiInfo();
    }
}
