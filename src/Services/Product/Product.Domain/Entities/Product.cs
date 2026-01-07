using BuildingBlocks.Shared.Domain;

namespace Product.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string? ImageUrl { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    public bool IsAvailable { get; private set; }

    private Product() { }

    public Product(string name, string description, decimal price, int stock, Guid categoryId, string? imageUrl = null)
    {
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        CategoryId = categoryId;
        ImageUrl = imageUrl;
        IsAvailable = true;
    }

    public void Update(string name, string description, decimal price, int stock, Guid categoryId, string? imageUrl = null)
    {
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        CategoryId = categoryId;
        ImageUrl = imageUrl;
        UpdateTimestamp();
    }

    public void UpdateStock(int quantity)
    {
        Stock = quantity;
        UpdateTimestamp();
    }

    public void DecreaseStock(int quantity)
    {
        if (Stock < quantity)
        {
            throw new InvalidOperationException("Insufficient stock");
        }
        Stock -= quantity;
        UpdateTimestamp();
    }

    public void IncreaseStock(int quantity)
    {
        Stock += quantity;
        UpdateTimestamp();
    }

    public void SetAvailability(bool isAvailable)
    {
        IsAvailable = isAvailable;
        UpdateTimestamp();
    }
}
