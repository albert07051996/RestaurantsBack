using BuildingBlocks.Shared.Common;
using BuildingBlocks.Shared.Interfaces;
using Identity.Application.Commands.AuthCommands;
using Identity.Application.Commands.FoodCategoriesCommands;
using Identity.Application.Commands.MenuCommands;
using Identity.Application.DTOs;
using Identity.Application.Interfaces;
using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using MediatR;

namespace Identity.Application.Commands.Handlers.AuthHandler;

public class FoodCategoryHandler : IRequestHandler<FoodCategoryCommand, Result<FoodCategoryResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IFoodCategoryRepository _foodCategoryRepository;

    public FoodCategoryHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IMenuRepository menuRepository,
        IFoodCategoryRepository foodCategoryRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _foodCategoryRepository = foodCategoryRepository;
    }


    public async Task<Result<FoodCategoryResponseDto>> Handle(FoodCategoryCommand request, CancellationToken cancellationToken)
    {
        var foodCategory = new FoodCategory(
        request.NameKa,
        request.NameEn,
        request.DescriptionKa,
        request.DescriptionEn,
        request.Priority,
        request.ImageUrl

        );

        await _foodCategoryRepository.AddAsync(foodCategory, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var menuResponse = new FoodCategoryResponseDto("Menu item created successfully");
        return Result<FoodCategoryResponseDto>.Success(menuResponse);
    }
}
