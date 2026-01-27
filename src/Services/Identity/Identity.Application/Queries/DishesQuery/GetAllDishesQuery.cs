using BuildingBlocks.Shared.Common;
using Identity.Application.DTOs;
using MediatR;

namespace Identity.Application.Queries;

public record GetAllDishesQuery : IRequest<Result<List<DishDto>>>;
