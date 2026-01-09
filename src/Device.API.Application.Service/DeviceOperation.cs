using Device.API.Application.Message.Dto;
using Device.API.Application.Message.Mapper;
using Device.API.Application.Service.Interfaces;
using Device.API.Domain.Contracts;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Device.API.Infrastructure.DI")]
namespace Device.API.Application.Service
{
    internal class DeviceOperation(IDeviceCRUD deviceCRUD) : IDevicesOperation
    {
        private readonly IDeviceCRUD _deviceCRUD = deviceCRUD ?? throw new ArgumentNullException(nameof(deviceCRUD));

        public async Task<bool> CreateAsync(string name, string brand)
        {
            return await _deviceCRUD.CreateDeviceAsync(name, brand);    
        }

        public async Task<bool> DeleteDeviceAsync(Guid id)
        {
            return await _deviceCRUD.DeleteDeviceAsync(id);
        }

        public async Task<ListDataResponse> GetAllAsync()
        {
            var response = await _deviceCRUD.GetAllDevicesAsync();

            return DeviceMapperResponse.MapperToListResponse(response);
        }

        public async Task<SingleDataResponse> GetById(Guid id)
        {
            var response = await _deviceCRUD.GetDeviceByIdAsync(id);

            if (response is null)
                return null;

            return DeviceMapperResponse.MapperToSingleResponse(response);
        }

        public Task PatchDeviceAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
