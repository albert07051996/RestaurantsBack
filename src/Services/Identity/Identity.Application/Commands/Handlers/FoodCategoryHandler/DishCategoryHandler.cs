using BuildingBlocks.Shared.Common;
using BuildingBlocks.Shared.Interfaces;
using Identity.Application.Commands.AuthCommands;
using Identity.Application.Commands.DishCategoriesCommands;
using Identity.Application.Commands.DishCommands;
using Identity.Application.DTOs;
using Identity.Application.Interfaces;
using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using MediatR;

namespace Identity.Application.Commands.Handlers.AuthHandler;

public class DishCategoryHandler : IRequestHandler<DishCategoryCommand, Result<DishCategoryResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IDishCategoryRepository _DishCategoryRepository;

    public DishCategoryHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IDishRepository DishRepository,
        IDishCategoryRepository DishCategoryRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _DishCategoryRepository = DishCategoryRepository;
    }


    public async Task<Result<DishCategoryResponseDto>> Handle(DishCategoryCommand request, CancellationToken cancellationToken)
    {
        var DishCategory = new DishCategory(
        request.NameKa,
        request.NameEn,
        request.DescriptionKa,
        request.DescriptionEn,
        request.Priority,
        request.ImageUrl

        );

        await _DishCategoryRepository.AddAsync(DishCategory, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var DishResponse = new DishCategoryResponseDto("Dish item created successfully");
        return Result<DishCategoryResponseDto>.Success(DishResponse);
    }
}
