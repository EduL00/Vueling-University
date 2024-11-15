using Microsoft.EntityFrameworkCore;
using Universities.Infraestructure.Contracts;
using Universities.Infraestructure.Contracts.DBEntities;
using Universities.Infraestructure.Impl.DBContext;

namespace Universities.Infraestructure.Impl
{
    public class DBUniversityRepository : IDBUniversityRepository
    {
        private readonly SimulUnivContext _dbContext;
        private const int TrueInt = 1;

        public DBUniversityRepository(SimulUnivContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void RegisterUniv(DBUniversityEntity university)
        {
            if (_dbContext.Universities.FirstOrDefault(x => x.Name == university.Name) != null) return;//elc quizas actualizar mas que eliminar

            _dbContext.Universities.Add (university);
            _dbContext.SaveChanges();
        }

        public List<DBUniversityEntity> GetUnivsEssentialInfo()
        {
            return _dbContext.Universities.ToList();
        }

        public List<DBUniversityEntity> GetUnivsInfoWebs()
        {
            return _dbContext.Universities.Include(x => x.Webs).ToList();
        }

        public List<DBUniversityEntity> GetUnivsInfoDomains()
        {
            return _dbContext.Universities.Include(x => x.Domains).ToList();
        }

        public List<DBUniversityEntity> GetUnivsAllInfo()
        {
            return _dbContext.Universities.Include(x => x.Webs).Include(x => x.Domains).ToList();
        }

        public DBUniversityEntity GetSingleUnivInfo(int idUniv)
        {
            return _dbContext.Universities.FirstOrDefault(x => x.Id == idUniv);
        }

        public void DeleteUniv(int idUniv)
        {
            DBUniversityEntity univ = GetSingleUnivInfo(idUniv);

            univ.Deleted = TrueInt;

            _dbContext.SaveChanges();
        }



    }
}
