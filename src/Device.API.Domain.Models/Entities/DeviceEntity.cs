
using System.ComponentModel;
using Dapper.Contrib.Extensions;

namespace Device.API.Domain.Models.Entities
{
    [Table("device")]
    public class DeviceEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public State? State { get; set; }

        public DateTime CreationTime { get; set;}
    }

    public enum State
    {
        [Description("available")]
        Active = 1,
        [Description("in-use")]
        InUse = 2,
        [Description("inactive")]
        Inactive = 3,
    }
}
