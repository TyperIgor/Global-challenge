
namespace Device.API.Domain.Models.Entities
{
    public class DeviceEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public State State { get; set; }

        public DateTime CreationTime { get; set;}
    }

    public enum State
    {
        Active = 1,
        InUse = 2,
        Inactive = 3,
    }
}
