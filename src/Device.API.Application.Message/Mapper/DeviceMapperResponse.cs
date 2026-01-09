using Device.API.Application.Message.Dto;
using Device.API.Domain.Models.Entities;
using Device.API.Domain.Models.Model;
using Device.API.Infrastructure.Utils.Extension;

namespace Device.API.Application.Message.Mapper
{
    public static class DeviceMapperResponse
    {
        public static SingleDataResponse MapperToSingleResponse(DeviceEntity deviceDTO)
        {
            return new SingleDataResponse
            (
                new DeviceResponse
                (
                    deviceDTO.Id.ToString(),
                    deviceDTO.Name,
                    deviceDTO.Brand,
                    deviceDTO.State.GetDescription(),
                    deviceDTO.CreationTime.ToString()
                ),
                "Device retrieved successfully."
            );
        }

        public static ListDataResponse MapperToListResponse(List<DeviceEntity> deviceDTO)
        {
            return new ListDataResponse
            (
                deviceDTO.Select(device => new DeviceResponse
                (
                    device.Id.ToString(),
                    device.Name,
                    device.Brand,
                    device.State.GetDescription(),
                    device.CreationTime.ToString()
                )),
                "Device List retrieved successfully."
            );
        }
    }
}
