using BuildingBlocks.Shared.Interfaces;

namespace Product.Domain.Interfaces;

public interface IProductRepository : IRepository<Entities.Product>
{
    Task<IEnumerable<Entities.Product>> GetProductsByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Entities.Product>> GetAvailableProductsAsync(CancellationToken cancellationToken = default);
    Task<Entities.Product?> GetProductWithCategoryAsync(Guid productId, CancellationToken cancellationToken = default);
}
