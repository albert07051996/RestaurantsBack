namespace Identity.Application.DTOs;

public record CreateDishDto(
    string NameKa,
    string NameEn,
    string DescriptionKa,
    string DescriptionEn,
    decimal? Price,
    Guid DishCategoryId,
    int? PreparationTimeMinutes,
    int? Calories,
    int? SpicyLevel,
    string Ingredients,
    string IngredientsEn,
    string Volume,
    string AlcoholContent,
    bool IsVeganDish,
    string Comment,
    string ImageUrl,
    string VideoUrl 


);

public record UpdateDishDto(
    string NameKa,
    string NameEn,
    string DescriptionKa,
    string DescriptionEn,
    decimal Price,
    Guid DishCategoryId,
    int PreparationTimeMinutes,
    int Calories,
    int SpicyLevel,
    string Ingredients,
    string IngredientsEn,
    string Volume,
    string AlcoholContent,
    bool IsVeganDish,
    string Comment,
    string ImageUrl,
    string VideoUrl
);

public record DishDto(
    Guid Id,
    string NameKa,
    string NameEn,
    string DescriptionKa,
    string DescriptionEn,
    decimal? Price,
    Guid DishCategoryId,
    int? PreparationTimeMinutes,
    int? Calories,
    int? SpicyLevel,
    string Ingredients,
    string IngredientsEn,
    string Volume,
    string AlcoholContent,
    bool IsVeganDish,
    string Comment,
    string ImageUrl,
    string VideoUrl,
    DateTime CreatedAt
);

public record DishResponseDto(
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

public record CreateDishCategoryDto(
    string NameKa,
    string NameEn,
    string DescriptionKa,
    string DescriptionEn,
    int Priority,
    string ImageUrl
    );
public record DishCategoryResponseDto(
    string messige
);