namespace Identity.Application.DTOs;

public record CreateMenuItemDto(
    string NameKa,
    string NameEn,
    string DescriptionKa,
    string DescriptionEn,
    decimal? Price,
    Guid FoodCategoryId,
    int? PreparationTimeMinutes,
    int? Calories,
    int? SpicyLevel,
    string Ingredients,
    string IngredientsEn,
    string Volume,
    string AlcoholContent,
    bool IsVeganFood,
    string Comment,
    string ImageUrl,
    string VideoUrl, 
    Guid? ImagePublicId


);

public record UpdateMenuItemDto(
    string NameKa,
    string NameEn,
    string DescriptionKa,
    string DescriptionEn,
    decimal Price,
    Guid FoodCategoryId,
    int PreparationTimeMinutes,
    int Calories,
    int SpicyLevel,
    string Ingredients,
    string IngredientsEn,
    string Volume,
    string AlcoholContent,
    bool IsVeganFood,
    string Comment,
    string ImageUrl,
    string VideoUrl
);

public record MenuItemDto(
    Guid Id,
    string NameKa,
    string NameEn,
    string DescriptionKa,
    string DescriptionEn,
    decimal? Price,
    Guid FoodCategoryId,
    int? PreparationTimeMinutes,
    int? Calories,
    int? SpicyLevel,
    string Ingredients,
    string IngredientsEn,
    string Volume,
    string AlcoholContent,
    bool IsVeganFood,
    string Comment,
    string ImageUrl,
    string VideoUrl,
    DateTime CreatedAt
);

public record MenuResponseDto(
    string messige
   
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

public record CreateFoodCategoryDto(
    string NameKa,
    string NameEn,
    string DescriptionKa,
    string DescriptionEn,
    int Priority,
    string ImageUrl
    );
public record FoodCategoryResponseDto(
    string messige
);