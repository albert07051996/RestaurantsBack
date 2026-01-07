using BuildingBlocks.Shared.Domain;

namespace Product.Domain.Entities;

public class MenuItem : BaseEntity
{
    public string NameKa { get; private set; } = string.Empty;
    public string NameEn { get; private set; } = string.Empty;
    public string DescriptionKa { get; private set; } = string.Empty;
    public string DescriptionEn { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public string Category { get; private set; } = string.Empty;
    public string ImageUrl { get; private set; } = string.Empty;
    public string? VideoUrl { get; private set; }
    public int PreparationTimeMinutes { get; private set; }
    public int Calories { get; private set; }
    public int SpicyLevel { get; private set; }
    public bool IsAvailable { get; private set; }
    public int SortOrder { get; private set; }
    
    // JSON stored fields
    public string IngredientsJson { get; private set; } = "[]";
    public string AllergensJson { get; private set; } = "[]";
    
    // Drink specific
    public string? Volume { get; private set; }
    public string? AlcoholContent { get; private set; }
    public string? ServingTemperature { get; private set; }

    private MenuItem() { }

    public MenuItem(
        string nameKa,
        string nameEn,
        string descriptionKa,
        string descriptionEn,
        decimal price,
        string category,
        string imageUrl,
        int preparationTimeMinutes,
        int calories,
        int spicyLevel = 0)
    {
        NameKa = nameKa;
        NameEn = nameEn;
        DescriptionKa = descriptionKa;
        DescriptionEn = descriptionEn;
        Price = price;
        Category = category;
        ImageUrl = imageUrl;
        PreparationTimeMinutes = preparationTimeMinutes;
        Calories = calories;
        SpicyLevel = spicyLevel;
        IsAvailable = true;
        SortOrder = 0;
    }

    public void Update(
        string nameKa,
        string nameEn,
        string descriptionKa,
        string descriptionEn,
        decimal price,
        string category,
        int preparationTimeMinutes,
        int calories,
        int spicyLevel)
    {
        NameKa = nameKa;
        NameEn = nameEn;
        DescriptionKa = descriptionKa;
        DescriptionEn = descriptionEn;
        Price = price;
        Category = category;
        PreparationTimeMinutes = preparationTimeMinutes;
        Calories = calories;
        SpicyLevel = spicyLevel;
        UpdateTimestamp();
    }

    public void UpdateImage(string imageUrl)
    {
        ImageUrl = imageUrl;
        UpdateTimestamp();
    }

    public void UpdateVideo(string? videoUrl)
    {
        VideoUrl = videoUrl;
        UpdateTimestamp();
    }

    public void UpdateIngredients(string ingredientsJson)
    {
        IngredientsJson = ingredientsJson;
        UpdateTimestamp();
    }

    public void UpdateAllergens(string allergensJson)
    {
        AllergensJson = allergensJson;
        UpdateTimestamp();
    }

    public void SetAvailability(bool isAvailable)
    {
        IsAvailable = isAvailable;
        UpdateTimestamp();
    }

    public void UpdateSortOrder(int sortOrder)
    {
        SortOrder = sortOrder;
        UpdateTimestamp();
    }

    public void SetDrinkInfo(string? volume, string? alcoholContent, string? servingTemperature)
    {
        Volume = volume;
        AlcoholContent = alcoholContent;
        ServingTemperature = servingTemperature;
        UpdateTimestamp();
    }
}
