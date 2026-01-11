using Device.API.Domain.Contracts.Repositories;
using Device.API.Domain.Models.Entities;
using Device.API.Infrastructure.Data.Context;
using Device.API.Infrastructure.Data.Interfaces;
using Device.API.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Device.API.Test.Repositories
{
    public class DeviceRepositoryTest
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IDbContext _dbContext;
        private DeviceRepositoryImp? _deviceRepositoryObj;

        public DeviceRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;

            var context = new AppDbContext(options);

            _deviceRepository = Substitute.For<IDeviceRepository>();
            _dbContext = Substitute.For<IDbContext>();
            _deviceRepositoryObj = new DeviceRepositoryImp(_dbContext, context);
        }

        [Fact]
        public async Task DeviceRepositoryObj_CreateAsync_ShouldThrowsArgumentNullException()
        {
            // Arrange
             DeviceEntity device = null;

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _deviceRepositoryObj.CreateAsync(device));
        }

        [Fact]
        public async Task DeviceRepositoryObj_GetAllAsync_ReturnsList()
        {
            // Act
            var result = await _deviceRepositoryObj.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<DeviceEntity>>(result);
        }

        [Fact]
        public async Task DeviceRepositoryObj_GetAsync_ShouldThrowsNullReferenceException()
        {
            // Arrange
            _deviceRepositoryObj = null;

            // Act
            // Assert
            await Assert.ThrowsAnyAsync<NullReferenceException>(() => _deviceRepositoryObj.GetAsync(null));
        }

        [Fact]
        public async Task DeviceRepositoryObj_GetAllByBrand_NullBrand_ShouldReturnEmpty()
        {
            // Arrange
            string brand = null;

            // Act
            var emptyList = await _deviceRepositoryObj.GetAllByBrand(brand);

            //Assert
            Assert.Empty(emptyList);
        }

        [Fact]
        public async Task DeviceRepositoryObj_GetAllByState_InvalidState_ShoulReturnEmpty()
        {
            // Arrange
            int state = -1;

            // Act 

            var result = await _deviceRepositoryObj.GetAllByState(state);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task DeviceRepositoryObj_CheckDeviceExistAndIsInUse_EmptyGuid_ShouldReturnFalse()
        {
            // Arrange
            Guid id = Guid.Empty;

            // Act 
            var result = await _deviceRepositoryObj.CheckDeviceExistAndIsInUse(id);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeviceRepositoryObj_CheckOnlyDeviceExist_EmptyGuid_ThrowsException()
        {
            // Arrange
            Guid id = Guid.Empty;

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _deviceRepositoryObj.CheckOnlyDeviceExist(id));
        }

        [Fact]
        public async Task DeviceRepositoryObj_PartiallyOrFullyUpdateAsync_NullEntity_ThrowsNullReferenceException()
        {
            // Arrange
            DeviceEntity entity = null;

            // Act
            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _deviceRepositoryObj.PartiallyOrFullyUpdateAsync(entity));
        }

    }
}
