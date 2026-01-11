

namespace Device.API.Domain.Models.Model
{
    public class DeviceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public string State { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
