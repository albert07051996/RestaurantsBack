using BuildingBlocks.Shared.Common;
using BuildingBlocks.Shared.Interfaces;
using Identity.Application.Commands.AuthCommands;
using Identity.Application.DTOs;
using Identity.Application.Interfaces;
using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using MediatR;

namespace Identity.Application.Commands.Handlers.AuthHandler;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<AuthResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterUserCommandHandler(
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

    public async Task<Result<AuthResponseDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Validate email uniqueness
        if (!await _userRepository.IsEmailUniqueAsync(request.Email, cancellationToken))
        {
            return Result<AuthResponseDto>.Failure("Email is already registered");
        }

        // Validate username uniqueness
        if (!await _userRepository.IsUserNameUniqueAsync(request.UserName, cancellationToken))
        {
            return Result<AuthResponseDto>.Failure("Username is already taken");
        }

        // Hash password
        var passwordHash = _passwordHasher.HashPassword(request.Password);

        // Create user
        var user = new User(
            request.Email,
            request.UserName,
            passwordHash,
            request.FirstName,
            request.LastName,
            request.PhoneNumber
        );

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Generate tokens
        var token = _jwtTokenGenerator.GenerateToken(user);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        var userDto = new UserDto(
            user.Id,
            user.Email,
            user.UserName,
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            user.IsActive,
            user.CreatedAt,
            new List<string>()
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
