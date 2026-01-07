namespace Product.Application.DTOs;

public record CreateMenuItemDto(
    string NameKa,
    string NameEn,
    string DescriptionKa,
    string DescriptionEn,
    decimal Price,
    string Category,
    int PreparationTimeMinutes,
    int Calories,
    int SpicyLevel,
    List<string> Ingredients,
    List<string> Allergens,
    string? Volume,
    string? AlcoholContent,
    string? ServingTemperature
);

public record UpdateMenuItemDto(
    string NameKa,
    string NameEn,
    string DescriptionKa,
    string DescriptionEn,
    decimal Price,
    string Category,
    int PreparationTimeMinutes,
    int Calories,
    int SpicyLevel,
    List<string> Ingredients,
    List<string> Allergens,
    string? Volume,
    string? AlcoholContent,
    string? ServingTemperature
);

public record MenuItemDto(
    Guid Id,
    string NameKa,
    string NameEn,
    string DescriptionKa,
    string DescriptionEn,
    decimal Price,
    string Category,
    string ImageUrl,
    string? VideoUrl,
    int PreparationTimeMinutes,
    int Calories,
    int SpicyLevel,
    List<string> Ingredients,
    List<string> Allergens,
    bool IsAvailable,
    int SortOrder,
    string? Volume,
    string? AlcoholContent,
    string? ServingTemperature,
    DateTime CreatedAt
);

public record RestaurantSettingsDto(
    Guid Id,
    string RestaurantName,
    string RestaurantNameEn,
    string TaglineKa,
    string TaglineEn,
    string Address,
    string Phone,
    string Email,
    string BackgroundImageUrl,
    string LogoUrl,
    string OpeningHours,
    string PrimaryColor,
    string SecondaryColor
);

public record UpdateRestaurantSettingsDto(
    string RestaurantName,
    string RestaurantNameEn,
    string TaglineKa,
    string TaglineEn,
    string Address,
    string Phone,
    string Email,
    string OpeningHours,
    string PrimaryColor,
    string SecondaryColor
);
