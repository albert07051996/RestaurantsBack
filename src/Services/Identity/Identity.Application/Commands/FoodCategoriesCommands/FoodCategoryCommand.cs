using BuildingBlocks.Shared.Common;
using Identity.Application.DTOs;
using MediatR;

namespace Identity.Application.Commands.DishCategoriesCommands;

public record DishCategoryCommand
(
    string NameKa,
    string NameEn,
    string DescriptionKa,
    string DescriptionEn,
    int Priority,
    string ImageUrl
) : IRequest<Result<DishCategoryResponseDto>>;
