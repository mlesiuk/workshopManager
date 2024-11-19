﻿using FluentValidation;
using workshopManager.Application.Commands.VehicleGeneration;

namespace workshopManager.Application.Validators.VehicleGeneration;

public sealed class UpdateVehicleGenerationValidator : AbstractValidator<UpdateVehicleGenerationCommand>
{
    public UpdateVehicleGenerationValidator()
    {
        RuleFor(vg => vg.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(255);
    }
}