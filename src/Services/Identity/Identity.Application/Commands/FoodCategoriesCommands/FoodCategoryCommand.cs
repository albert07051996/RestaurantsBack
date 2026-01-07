using BuildingBlocks.Shared.Common;
using Identity.Application.DTOs;
using MediatR;

namespace Identity.Application.Commands.FoodCategoriesCommands;

public record FoodCategoryCommand
(
    string NameKa,
    string NameEn,
    string DescriptionKa,
    string DescriptionEn,
    int Priority,
    string ImageUrl
) : IRequest<Result<FoodCategoryResponseDto>>;
