using BuildingBlocks.Shared.Domain;

namespace Identity.Domain.Entities
{
    public class Dish : BaseEntity
    {
        public string NameKa { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string DescriptionKa { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public decimal? Price { get; set; }      
        public string ImageUrl { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public int? PreparationTimeMinutes { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string Volume { get; set; } = string.Empty;
        public string AlcoholContent { get; set; }= string.Empty;
        public string Ingredients { get; set; } = string.Empty;
        public string IngredientsEn { get; set; } = string.Empty;
        public bool IsVeganDish { get; set; } = false;
        public string Comment { get; set; } = string.Empty;
        public int? Calories { get; set; }
        public int? SpicyLevel { get; set; }
        public Guid DishCategoryId { get; private set; }
        public DishCategory DishCategory { get; private set; } = null!;
        public string? ImagePublicId { get; set; }


        private Dish() { }

        public Dish(string nameKa, string nameEn, string descriptionKa, string descriptionEn, decimal? price, Guid dishCategoryId, string imageUrl, string videoUrl, int? preparationTimeMinutes, string volume, string alcoholContent, string ingredients, string ingredientsEn, bool isVeganDish, string comment, int? calories, int? spicyLevel)
        {
            NameKa = nameKa;
            NameEn = nameEn;
            DescriptionKa = descriptionKa;
            DescriptionEn = descriptionEn;
            Price = price;
            DishCategoryId = dishCategoryId;
            ImageUrl = imageUrl;
            VideoUrl = videoUrl;
            PreparationTimeMinutes = preparationTimeMinutes;
            Volume = volume;
            AlcoholContent = alcoholContent;
            Ingredients = ingredients;
            IngredientsEn = ingredientsEn;
            IsVeganDish = isVeganDish;
            Comment = comment;
            Calories = calories;
            SpicyLevel = spicyLevel;
        }

        public void Update(string nameKa, string nameEn, string descriptionKa, string descriptionEn, decimal? price, Guid dishCategoryId,
            string imageUrl, string videoUrl, int? preparationTimeMinutes, bool isAvailable, string volume, string alcoholContent,
            string ingredients, string ingredientsEn, bool isVeganDish, string comment, int? calories, int? spicyLevel)
        {
            NameKa = nameKa;
            NameEn = nameEn;
            DescriptionKa = descriptionKa;
            DescriptionEn = descriptionEn;
            Price = price;
            DishCategoryId = dishCategoryId;
            ImageUrl = imageUrl;
            VideoUrl = videoUrl;
            PreparationTimeMinutes = preparationTimeMinutes;
            IsAvailable = isAvailable;
            Volume = volume;
            AlcoholContent = alcoholContent;
            Ingredients = ingredients;
            IngredientsEn = ingredientsEn;
            IsVeganDish = isVeganDish;
            Comment = comment;
            Calories = calories;
            SpicyLevel = spicyLevel;

            UpdateTimestamp();
        }
    }
}
