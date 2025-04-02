using ApiFortress.Application.DTOs;

namespace ApiFortress.Application.Interfaces
{
    public interface IDataService
    {
        Task<IEnumerable<DataItemDTO>> GetAllDataAsync();
        Task<DataItemDTO> AddDataAsync(DataItemDTO dataDto);
    }
}