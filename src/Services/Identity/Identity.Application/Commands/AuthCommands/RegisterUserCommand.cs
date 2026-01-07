using BuildingBlocks.Shared.Common;
using Identity.Application.DTOs;
using MediatR;

namespace Identity.Application.Commands.AuthCommands;

public record RegisterUserCommand(
    string Email,
    string UserName,
    string Password,
    string FirstName,
    string LastName,
    string? PhoneNumber
) : IRequest<Result<AuthResponseDto>>;
