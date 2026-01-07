using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Common.Interfaces;

public interface IApplicationDbContext
{
	DbSet<MenuItem> MenuItems { get; } // ჩაამატეთ თქვენი Entity-ები
	Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}