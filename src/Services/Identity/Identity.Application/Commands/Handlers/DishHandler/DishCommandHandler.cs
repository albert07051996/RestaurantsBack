using BuildingBlocks.Shared.Common;
using BuildingBlocks.Shared.Interfaces;
using Identity.Application.Commands.AuthCommands;
using Identity.Application.Commands.DishCommands;
using Identity.Application.DTOs;
using Identity.Application.Interfaces;
using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using MediatR;

namespace Identity.Application.Commands.Handlers.AuthHandler;

public class DishCommandHandler : IRequestHandler<DishCommand, Result<DishResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IDishRepository _DishRepository;

    public DishCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IDishRepository DishRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _DishRepository = DishRepository;
    }


    public async Task<Result<DishResponseDto>> Handle(DishCommand request, CancellationToken cancellationToken)
    {
        var Dish = new Dish(
        request.NameKa,
        request.NameEn,
        request.DescriptionKa,
        request.DescriptionEn,
        request.Price,
        request.DishCategoryId,
        request.ImageUrl,
        request.VideoUrl,
        request.PreparationTimeMinutes,
        request.Volume,
        request.AlcoholContent,
        request.Ingredients,
        request.IngredientsEn,
        request.IsVeganDish,
        request.Comment,
        request.Calories,
        request.SpicyLevel
        );

        await _DishRepository.AddAsync(Dish, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var DishResponse = new DishResponseDto("Dish item created successfully");
        return Result<DishResponseDto>.Success(DishResponse);
    }
}
