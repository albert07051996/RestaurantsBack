using BuildingBlocks.Shared.Common;
using BuildingBlocks.Shared.Interfaces;
using Identity.Application.Commands.AuthCommands;
using Identity.Application.DTOs;
using Identity.Application.Interfaces;
using Identity.Domain.Interfaces;
using MediatR;

namespace Identity.Application.Commands.Handlers.AuthHandler;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user == null)
        {
            return Result<AuthResponseDto>.Failure("Invalid email or password");
        }

        if (!user.IsActive)
        {
            return Result<AuthResponseDto>.Failure("Account is deactivated");
        }

        if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
        {
            return Result<AuthResponseDto>.Failure("Invalid email or password");
        }

        // Update last login
        user.UpdateLastLogin();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Generate tokens
        var token = _jwtTokenGenerator.GenerateToken(user);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        var userWithRoles = await _userRepository.GetUserWithRolesAsync(user.Id, cancellationToken);
        var roles = userWithRoles?.UserRoles.Select(ur => ur.Role.Name).ToList() ?? new List<string>();

        var userDto = new UserDto(
            user.Id,
            user.Email,
            user.UserName,
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            user.IsActive,
            user.CreatedAt,
            roles
        );

        var authResponse = new AuthResponseDto(
            token,
            refreshToken,
            DateTime.UtcNow.AddHours(24),
            userDto
        );

        return Result<AuthResponseDto>.Success(authResponse);
    }
}
