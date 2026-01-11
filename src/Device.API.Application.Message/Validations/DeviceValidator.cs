using Device.API.Application.Message.Dto;
using FluentValidation;

namespace Device.API.Application.Message.Validations
{
    internal class DeviceValidator : AbstractValidator<DeviceRequest>
    {
        public DeviceValidator()
        {
            RuleFor(m => m.Name)
                 .NotEmpty().NotNull()
                     .WithMessage("Field Name must not be null or empty")
                 .MaximumLength(100)
                     .WithMessage("Field Name must be less than 100 ")
                 .MinimumLength(4)
                     .WithMessage("Field Name must have 4 legth at minimum");

            RuleFor(l => l.Brand)
                .NotEmpty().NotNull()
                    .WithMessage("Field Name must not be null or empty")
                .MaximumLength(100)
                    .WithMessage("Field Name must be less than 100 ")
                .MinimumLength(5)
                    .WithMessage("Field Name must have 5 legth at minimum");
        }
    }

    internal class UpdateDeviceValidator : AbstractValidator<DeviceUpdateRequest>
    {
        public UpdateDeviceValidator()
        {
            RuleFor(m => m.Id)
                 .NotNull()
                     .WithMessage("Field ID must not be null")
                 .NotEmpty()
                    .WithMessage("Id field cannot be empty");
        }
    }
}
