using BuildingBlocks.Shared.Interfaces;
using Identity.Domain.Entities;

namespace Identity.Domain.Interfaces;

public interface IDishRepository : IRepository<Dish>
{
    //Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    //Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    //Task<User?> GetUserWithRolesAsync(Guid userId, CancellationToken cancellationToken = default);
    //Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);
    //Task<bool> IsUserNameUniqueAsync(string userName, CancellationToken cancellationToken = default);
}
