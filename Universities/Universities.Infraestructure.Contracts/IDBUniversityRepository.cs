using Universities.Infraestructure.Contracts.DBEntities;

namespace Universities.Infraestructure.Contracts
{
    public interface IDBUniversityRepository
    {
        public void RegisterUniv (DBUniversityEntity university);
        public List<DBUniversityEntity> GetUnivsEssentialInfo();
        public List<DBUniversityEntity> GetUnivsInfoWebs();

        public List<DBUniversityEntity> GetUnivsInfoDomains();

        public List<DBUniversityEntity> GetUnivsAllInfo();

        public DBUniversityEntity GetSingleUnivInfo(int idUniv);

        public void DeleteUniv(int idUniv);
    }
}
