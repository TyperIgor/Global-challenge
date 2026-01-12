using Device.API.Application.Message.Dto;
using Device.API.Application.Service.Interfaces;
using Device.API.Controllers.v1;
using Device.API.Domain.Models.Entities;
using AutoFixture;
using NSubstitute;
using Microsoft.AspNetCore.Mvc;

namespace Device.API.Test.Controller
{
    public class DeviceControllerTest
    {
        private readonly IDevicesOperation _deviceServiceSub;
        private readonly DeviceController _controller;
        private Fixture _fixture = new Fixture();

        public DeviceControllerTest()
        {
            _deviceServiceSub = Substitute.For<IDevicesOperation>();
            _controller = new DeviceController(_deviceServiceSub);
        }

        [Fact]
        public async Task DeviceControllerObj_GetAllDevices_ShouldReturnWithListOfDevices()
        {
            var objlist = _fixture.Create<List<DeviceResponse>>();

            // Arrange
            var devices = new ListDataResponse(
                    Data: objlist,
                    Message: ""
                );

            _deviceServiceSub.GetAllAsync().Returns(devices);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsType<ActionResult<ListDataResponse>>(result);
        }

        [Fact]
        public async Task DeviceControllerObj_GetDeviceById_DeviceExists_ShouldReturnDataType()
        {
            // Arrange
            var response = _fixture.Create<SingleDataResponse>();
            _deviceServiceSub.GetById(new Guid()).Returns(response);

            // Act
            var result = await _controller.GetById(new Guid());

            // Assert
             Assert.IsType<ActionResult<SingleDataResponse>>(result);
        }

        [Fact]
        public async Task DeviceControllerObj_GetDeviceById_DeviceNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var response = _fixture.Create<SingleDataResponse>();

            response = null;

            _deviceServiceSub.GetById(new Guid()).ReturnsForAnyArgs(response);

            // Act
            var result = await _controller.GetById(new Guid());

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task DeviceControllerObj_CreateDevice_ShouldReturnsCreatedAtActionStatus()
        {
            // Arrange
            var device = _fixture.Create<DeviceRequest>();
            _deviceServiceSub.CreateAsync("","").ReturnsForAnyArgs(true);

            // Act
            var result = await _controller.Create("","");

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task DeviceControllerObj_UpdateDevice_DeviceExists_ShouldReturnStatusOK()
        {
            // Arrange
            var deviceobj = _fixture.Create<DeviceUpdateRequest>();
            _deviceServiceSub.PartialOrFullUpdateAsync(deviceobj).ReturnsForAnyArgs(true);

            // Act
            var result = await _controller.UpdateFullyOrPartialDevice(deviceobj);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeviceControllerObj_UpdateDevice_DeviceNotFound_ShouldReturnsNotFound()
        {
            // Arrange
            var deviceobj = _fixture.Create<DeviceUpdateRequest>();
            _deviceServiceSub.PartialOrFullUpdateAsync(deviceobj).Returns(false);

            // Act
            var result = await _controller.UpdateFullyOrPartialDevice(deviceobj);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task _DeviceControllerObj_DeleteDevice_ShouldReturnBadRequestIfNotExist()
        {
            // Arrange
            _deviceServiceSub.DeleteDeviceAsync(new Guid()).ReturnsForAnyArgs(false);

            // Act
            var result = await _controller.DeleteById(new Guid());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
