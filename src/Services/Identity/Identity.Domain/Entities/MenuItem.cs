using BuildingBlocks.Shared.Domain;

namespace Identity.Domain.Entities
{
    public class MenuItem : BaseEntity
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
        public bool IsVeganFood { get; set; } = false;
        public string Comment { get; set; } = string.Empty;
        public int? Calories { get; set; }
        public int? SpicyLevel { get; set; }
        public Guid FoodCategoryId { get; private set; }
        public FoodCategory FoodCategory { get; private set; } = null!;
        public Guid? ImagePublicId { get; set; } 


        private MenuItem() { }

        public MenuItem(string nameKa, string nameEn, string descriptionKa, string descriptionEn, decimal? price, Guid foodCategoryId, string imageUrl, string videoUrl, int? preparationTimeMinutes, string volume, string alcoholContent, string ingredients, string ingredientsEn, bool isVeganFood, string comment, int? calories, int? spicyLevel, Guid? imagePublicId)
        {
            NameKa = nameKa;
            NameEn = nameEn;
            DescriptionKa = descriptionKa;
            DescriptionEn = descriptionEn;
            Price = price;
            FoodCategoryId = foodCategoryId;
            ImageUrl = imageUrl;
            VideoUrl = videoUrl;
            PreparationTimeMinutes = preparationTimeMinutes;
            Volume = volume;
            AlcoholContent = alcoholContent;
            Ingredients = ingredients;
            IngredientsEn = ingredientsEn;
            IsVeganFood = isVeganFood;
            Comment = comment;
            Calories = calories;
            SpicyLevel = spicyLevel;
            ImagePublicId = imagePublicId;
        }

        public void Update(string nameKa, string nameEn, string descriptionKa, string descriptionEn, decimal? price, Guid foodCategoryId,
            string imageUrl, string videoUrl, int? preparationTimeMinutes, bool isAvailable, string volume, string alcoholContent,
            string ingredients, string ingredientsEn, bool isVeganFood, string comment, int? calories, int? spicyLevel, Guid? imagePublicId)
        {
            NameKa = nameKa;
            NameEn = nameEn;
            DescriptionKa = descriptionKa;
            DescriptionEn = descriptionEn;
            Price = price;
            FoodCategoryId = foodCategoryId;
            ImageUrl = imageUrl;
            VideoUrl = videoUrl;
            PreparationTimeMinutes = preparationTimeMinutes;
            IsAvailable = isAvailable;
            Volume = volume;
            AlcoholContent = alcoholContent;
            Ingredients = ingredients;
            IngredientsEn = ingredientsEn;
            IsVeganFood = isVeganFood;
            Comment = comment;
            Calories = calories;
            SpicyLevel = spicyLevel;
            ImagePublicId = imagePublicId;
            UpdateTimestamp();
        }
    }
}
