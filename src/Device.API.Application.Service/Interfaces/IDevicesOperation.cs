
using Device.API.Application.Message.Dto;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Device.API.Infrastructure.DI")]
namespace Device.API.Application.Service.Interfaces
{
    public interface IDevicesOperation
    {
        Task<ListDataResponse> GetAllAsync();

        Task<SingleDataResponse> GetById(Guid id);

        Task<bool> CreateAsync(string name, string brand);

        Task<ListDataResponse> GetByBrand(string Brand);

        Task<ListDataResponse> GetByState(int state);

        Task<bool> DeleteDeviceAsync(Guid id);

        Task<bool> PartialOrFullUpdateAsync(DeviceUpdateRequest request);
    }
}
