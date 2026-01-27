using BuildingBlocks.Shared.Domain;

namespace Identity.Domain.Entities;

public class DishCategory : BaseEntity
{
    public string NameKa { get; private set; } = string.Empty;
    public string NameEn { get; private set; } = string.Empty;
    public string DescriptionKa { get; private set; } = string.Empty;
    public string DescriptionEn { get; private set; } = string.Empty;
    public int Priority { get; private set; }
    public string ImageUrl { get; set; } = string.Empty;

    public ICollection<Dish> Dishes { get; private set; } = new List<Dish>();

    private DishCategory() { }

    public DishCategory(string nameKa, string nameEn, string descriptionKa, string descriptionEn, int priority, string imageUrl)
    {
        NameKa = nameKa;
        NameEn = nameEn;
        DescriptionKa = descriptionKa;
        DescriptionEn = descriptionEn;
        Priority = priority;
        ImageUrl = imageUrl;
    }
}
