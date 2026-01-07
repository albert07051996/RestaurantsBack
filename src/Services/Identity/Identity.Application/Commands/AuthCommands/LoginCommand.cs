using BuildingBlocks.Shared.Common;
using Identity.Application.DTOs;
using MediatR;

namespace Identity.Application.Commands.AuthCommands;

public record LoginCommand(
    string Email,
    string Password
) : IRequest<Result<AuthResponseDto>>;
