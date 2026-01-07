using System.Text.Json.Serialization;

namespace Device.API.Application.Message.Dto
{
    public record DeviceRequest 
    (
         [property: JsonPropertyName("name")] string Name,
         [property: JsonPropertyName("brand")] string Brand
    );

    public record DeviceResponse
    (
         [property: JsonPropertyName("id")] string Id,
         [property: JsonPropertyName("name")] string Name,
         [property: JsonPropertyName("brand")] string Brand,
         [property: JsonPropertyName("state")] string State,
         [property: JsonPropertyName("creationTime")] string CreationTime
    );
}
