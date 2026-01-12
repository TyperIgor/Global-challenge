using Device.API.Application.Message.Dto;
using Device.API.Application.Service.Interfaces;
using Device.API.Controllers.v1;
using NSubstitute;

namespace Device.API.Test.Controller
{
    internal class DeviceControllerTest
    {
        private readonly IDevicesOperation _deviceServiceSub;
        private readonly DeviceController _controller;

        public DeviceControllerTest()
        {
            _deviceServiceSub = Substitute.For<IDevicesOperation>();
            _controller = new DeviceController(_deviceServiceSub);
        }

        [Fact]
        public async Task GetAllDevices_ReturnsOkResult_WithListOfDevices()
        {

            // Arrange
            var devices = new ListDataResponse(
                    Data: new 
                )

            _deviceServiceSub.GetAllAsync().Returns(devices);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnDevices = Assert.IsAssignableFrom<IEnumerable<DeviceModel>>(okResult.Value);
            Assert.Equal(2, ((List<DeviceModel>)returnDevices).Count);
        }

        [Fact]
        public async Task GetDeviceById_DeviceExists_ReturnsOkResult()
        {
            // Arrange
            var device = new DeviceModel { Id = 1, Name = "Device1" };
            _deviceServiceSub.Setup(s => s.GetDeviceByIdAsync(1)).ReturnsAsync(device);

            // Act
            var result = await _controller.GetDeviceById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnDevice = Assert.IsType<DeviceModel>(okResult.Value);
            Assert.Equal(1, returnDevice.Id);
        }

        [Fact]
        public async Task GetDeviceById_DeviceNotFound_ReturnsNotFound()
        {
            // Arrange
            _deviceServiceSub.Setup(s => s.GetDeviceByIdAsync(99)).ReturnsAsync((DeviceModel)null);

            // Act
            var result = await _controller.GetDeviceById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateDevice_ValidDevice_ReturnsCreatedAtAction()
        {
            // Arrange
            var device = new DeviceModel { Id = 1, Name = "Device1" };
            _deviceServiceSub.Setup(s => s.CreateDeviceAsync(device)).ReturnsAsync(device);

            // Act
            var result = await _controller.CreateDevice(device);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnDevice = Assert.IsType<DeviceModel>(createdAtActionResult.Value);
            Assert.Equal(device.Id, returnDevice.Id);
        }

        [Fact]
        public async Task UpdateDevice_DeviceExists_ReturnsNoContent()
        {
            // Arrange
            var device = new DeviceModel { Id = 1, Name = "UpdatedDevice" };
            _deviceServiceSub.Setup(s => s.UpdateDeviceAsync(1, device)).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateDevice(1, device);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateDevice_DeviceNotFound_ReturnsNotFound()
        {
            // Arrange
            var device = new DeviceModel { Id = 99, Name = "NonExistent" };
            _deviceServiceSub.Setup(s => s.UpdateDeviceAsync(99, device)).ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateDevice(99, device);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteDevice_DeviceExists_ReturnsNoContent()
        {
            // Arrange
            _deviceServiceSub.Setup(s => s.DeleteDeviceAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteDevice(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteDevice_DeviceNotFound_ReturnsNotFound()
        {
            // Arrange
            _deviceServiceSub.Setup(s => s.DeleteDeviceAsync(99)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteDevice(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
