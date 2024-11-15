using Universities.Library.Contracts.DTOs.ResDTOs;

namespace Universities.Library.Contracts
{
    public interface IAppService
    {
        public Task<MigrateInfoResDTO> MigrateInfo();

        public ListCountryUnivDTO LisUnivsCountry();
        public ListWebUnivDTO LisUnivsWebs(string containName);

        public DeleteUnivDTO DeleteUniv(int univId);
    }
}
