
using Device.API.Domain.Models.Entities;

namespace Device.API.Test.Domain
{
    public class DeviceEntityTest
    {
        [Fact]
        public void DeviceEntity_Validade_PropertiesType()
        {
            // Arrange
            var device = new DeviceEntity();

            // Act
            device.Id = Guid.NewGuid();
            device.Name = "Test Device";
            device.State = State.Active;
            device.Brand = "SN123456";
            device.CreationTime = new DateTime(2024, 1, 1);

            // Assert
            Assert.IsType<Guid>(device.Id);
            Assert.IsType<string>(device.Name);
            Assert.IsType<string>(device.Brand);
            Assert.IsType<State>(device.State);
            Assert.IsType<DateTime>(device.CreationTime);
        }
    }
}
