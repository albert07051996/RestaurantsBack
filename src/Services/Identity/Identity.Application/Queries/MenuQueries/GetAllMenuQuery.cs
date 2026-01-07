using BuildingBlocks.Shared.Common;
using Identity.Application.DTOs;
using MediatR;

namespace Identity.Application.Queries;

public record GetAllMenuQuery : IRequest<Result<List<MenuItemDto>>>;
