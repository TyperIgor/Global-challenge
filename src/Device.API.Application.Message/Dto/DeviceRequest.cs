using System.Text.Json.Serialization;

namespace Device.API.Application.Message.Dto
{
    public record DeviceRequest 
    (
         [property: JsonPropertyName("id")] Guid Id,
         [property: JsonPropertyName("name")] string? Name,
         [property: JsonPropertyName("brand")] string? Brand,
         [property: JsonPropertyName("state")] int? State
    );

    public record DeviceUpdateRequest
    (
     [property: JsonPropertyName("id")] Guid Id,
     [property: JsonPropertyName("name")] string? Name,
     [property: JsonPropertyName("brand")] string? Brand,
     [property: JsonPropertyName("state")] int? State
    );

    public record DeviceResponse
    (
         [property: JsonPropertyName("id")] string Id,
         [property: JsonPropertyName("name")] string Name,
         [property: JsonPropertyName("brand")] string Brand,
         [property: JsonPropertyName("state")] string State,
         [property: JsonPropertyName("creationTime")] string CreationTime
    );

    public record SingleDataResponse
    (
         [property: JsonPropertyName("data")] DeviceResponse Data,
         [property: JsonPropertyName("message")] string Message
    );

    public record ListDataResponse
    (
         [property: JsonPropertyName("data")] IEnumerable<DeviceResponse> Data,
         [property: JsonPropertyName("message")] string Message
    );
}
