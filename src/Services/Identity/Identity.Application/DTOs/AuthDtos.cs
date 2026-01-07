namespace Identity.Application.DTOs;

public record RegisterUserDto(
    string Email,
    string UserName,
    string Password,
    string FirstName,
    string LastName,
    string? PhoneNumber
);

public record LoginDto(
    string Email,
    string Password
);

public record UserDto(
    Guid Id,
    string Email,
    string UserName,
    string FirstName,
    string LastName,
    string? PhoneNumber,
    bool IsActive,
    DateTime CreatedAt,
    List<string> Roles
);

public record AuthResponseDto(
    string Token,
    string RefreshToken,
    DateTime ExpiresAt,
    UserDto User
);

public record RefreshTokenDto(
    string RefreshToken
);

public record UpdateProfileDto(
    string FirstName,
    string LastName,
    string? PhoneNumber
);

public record ChangePasswordDto(
    string CurrentPassword,
    string NewPassword
);
