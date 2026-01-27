using BuildingBlocks.Shared.Common;
using Identity.Application.Common.Interfaces;
using Identity.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;



namespace Identity.Application.Queries.MenyQueriesHandlers;
public class GetAllDishQueryHandler : IRequestHandler<GetAllDishesQuery, Result<List<DishDto>>>
{
    private readonly IApplicationDbContext _context; // ინტერფეისი

    public GetAllDishQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<DishDto>>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var Dish = await _context.Dishes

                .Where(u => !u.IsDeleted)
                .ToListAsync(cancellationToken);

            var DishDtos = Dish.Select(Dish => new DishDto(
                Dish.Id,
                Dish.NameKa,
                Dish.NameEn,
                Dish.DescriptionKa,
                Dish.DescriptionEn,
                Dish.Price,
                Dish.DishCategoryId,
                Dish.PreparationTimeMinutes,
                Dish.Calories,
                Dish.SpicyLevel,
                Dish.Ingredients,
                Dish.IngredientsEn,
                Dish.Volume,
                Dish.AlcoholContent,
                Dish.IsVeganDish,
                Dish.Comment,
                Dish.ImageUrl,
                Dish.VideoUrl,
                Dish.CreatedAt
                )).ToList();


            return Result<List<DishDto>>.Success(DishDtos);
        }
        catch (Exception ex)
        {
            return Result<List<DishDto>>.Failure($"Error retrieving users: {ex.Message}");
        }
    }
}
