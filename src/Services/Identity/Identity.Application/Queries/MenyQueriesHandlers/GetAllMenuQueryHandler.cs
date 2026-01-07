using BuildingBlocks.Shared.Common;
using Identity.Application.Common.Interfaces;
using Identity.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;



namespace Identity.Application.Queries.MenyQueriesHandlers;
public class GetAllMenuQueryHandler : IRequestHandler<GetAllMenuQuery, Result<List<MenuItemDto>>>
{
    private readonly IApplicationDbContext _context; // ინტერფეისი

    public GetAllMenuQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<MenuItemDto>>> Handle(GetAllMenuQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var menu = await _context.MenuItems 
                
                .Where(u => !u.IsDeleted)
                .ToListAsync(cancellationToken);

            var menuDtos = menu.Select(menu => new MenuItemDto(
                menu.Id,
                menu.NameKa,
                menu.NameEn,
                menu.DescriptionKa,
                menu.DescriptionEn,
                menu.Price,
                menu.FoodCategoryId,
                menu.PreparationTimeMinutes,
                menu.Calories,
                menu.SpicyLevel,
                menu.Ingredients,
                menu.IngredientsEn,
                menu.Volume,
                menu.AlcoholContent,
                menu.IsVeganFood,
                menu.Comment,
                menu.ImageUrl,
                menu.VideoUrl,
                menu.CreatedAt
                )).ToList();


            return Result<List<MenuItemDto>>.Success(menuDtos);
        }
        catch (Exception ex)
        {
            return Result<List<MenuItemDto>>.Failure($"Error retrieving users: {ex.Message}");
        }
    }
}
