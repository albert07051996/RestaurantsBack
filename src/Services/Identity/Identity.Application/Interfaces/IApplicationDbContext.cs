using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Common.Interfaces;

public interface IApplicationDbContext
{
	DbSet<Dish> Dishes { get; } // ჩაამატეთ თქვენი Entity-ები
	Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}