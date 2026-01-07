using BuildingBlocks.Shared.Common;
using BuildingBlocks.Shared.Interfaces;
using Identity.Application.Commands.AuthCommands;
using Identity.Application.Commands.MenuCommands;
using Identity.Application.DTOs;
using Identity.Application.Interfaces;
using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using MediatR;

namespace Identity.Application.Commands.Handlers.AuthHandler;

public class MenuCommandHandler : IRequestHandler<MenuCommand, Result<MenuResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IMenuRepository _menuRepository;

    public MenuCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IMenuRepository menuRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _menuRepository = menuRepository;
    }


    public async Task<Result<MenuResponseDto>> Handle(MenuCommand request, CancellationToken cancellationToken)
    {
        var menu = new MenuItem(
        request.NameKa,
        request.NameEn,
        request.DescriptionKa,
        request.DescriptionEn,
        request.Price,
        request.foodCategoryId,
        request.ImageUrl,
        request.VideoUrl,
        request.PreparationTimeMinutes,
        request.Volume,
        request.AlcoholContent,
        request.Ingredients,
        request.IngredientsEn,
        request.IsVeganFood,
        request.Comment,
        request.Calories,
        request.SpicyLevel
        );

        await _menuRepository.AddAsync(menu, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var menuResponse = new MenuResponseDto("Menu item created successfully");
        return Result<MenuResponseDto>.Success(menuResponse);
    }
}
