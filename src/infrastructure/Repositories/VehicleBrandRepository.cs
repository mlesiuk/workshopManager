﻿using Microsoft.EntityFrameworkCore;
using workshopManager.Application.Abstractions.Interfaces;
using workshopManager.Domain.Entities;
using workshopManager.Infrastructure.Persistence;

namespace workshopManager.Infrastructure.Repositories;

public sealed class VehicleBrandRepository(ApplicationDbContext context) : IVehicleBrandRepository
{
    private readonly DbSet<VehicleBrand> _vehicleBrands = context.Set<VehicleBrand>();

	public async Task AddAsync(VehicleBrand entity, CancellationToken cancellationToken = default)
	{
		await _vehicleBrands.AddAsync(entity, cancellationToken);
	}

	public async Task<bool> AlreadyExistsAsync(VehicleBrand entity, CancellationToken cancellationToken = default)
	{
		if (await GetByIdAsync(entity.Id, cancellationToken) is not null)
		{
			return true;
		}

		if (await GetByNameAsync(entity.Name, cancellationToken) is not null)
		{
			return true;
		}

		return false;
    }

    public async Task<IEnumerable<VehicleBrand>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _vehicleBrands
            .Where(vb => vb.Deleted == null)
            .ToListAsync(cancellationToken) ?? [];
    }

    public async Task<VehicleBrand?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return await _vehicleBrands
			.SingleOrDefaultAsync(vb => vb.Id == id && vb.Deleted == null, cancellationToken);
	}

    public async Task<VehicleBrand?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _vehicleBrands
            .FirstOrDefaultAsync(vb => EF.Functions.Like(vb.Name, name) && vb.Deleted == null, cancellationToken);
    }

	public async Task RemoveAsync(VehicleBrand entity, CancellationToken cancellationToken = default)
    {
        var vehicleBrandToRemove = await _vehicleBrands
            .SingleOrDefaultAsync(vb => vb.Id == entity.Id && vb.Deleted == null, cancellationToken);
        if (vehicleBrandToRemove is not null)
        {
            _vehicleBrands.Remove(vehicleBrandToRemove);
        }
    }

	public async Task UpdateAsync(VehicleBrand entity, CancellationToken cancellationToken = default)
    {
        var vehicleBrandToUpdate = await _vehicleBrands
            .SingleOrDefaultAsync(vb => vb.Id == entity.Id && vb.Deleted == null, cancellationToken);
        if (vehicleBrandToUpdate is not null)
        {
            _vehicleBrands
                .Entry(vehicleBrandToUpdate).CurrentValues
                .SetValues(entity);
        }
    }
}
