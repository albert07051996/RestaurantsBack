using BuildingBlocks.Shared.Domain;

namespace Product.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public ICollection<Product> Products { get; private set; } = new List<Product>();

    private Category() { }

    public Category(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
        UpdateTimestamp();
    }
}
