using Nasa.Common.DTO.CurrentFire;

namespace Nasa.BLL.ServicesContracts
{
    public interface ICurrentFiresService
    {
        Task<IEnumerable<CurrentFireDto>> GetCurrentFires(DateTime date, int numberDays);
    }
}
