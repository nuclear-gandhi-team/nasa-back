using Nasa.Common.DTO.Coordinates;

namespace Nasa.BLL.ServicesContracts
{
    public interface ICurrentFiresService
    {
        Task<IEnumerable<CoordinatesDto>> GetCurrentFires(DateTime date, int numberDays);
    }
}
