﻿using FluentValidation;
using workshopManager.Application.Commands.VehicleBrand;

namespace workshopManager.Application.Validators.VehicleBrand;

public sealed class CreateVehicleBrandValidator : AbstractValidator<CreateVehicleBrandCommand>
{
    public CreateVehicleBrandValidator()
    {
        RuleFor(vb => vb.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(255);
    }
}
