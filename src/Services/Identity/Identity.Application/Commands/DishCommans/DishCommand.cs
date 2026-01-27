using BuildingBlocks.Shared.Common;
using Identity.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Commands.DishCommands;

public record DishCommand
(
    string NameKa,
    string NameEn,
    string DescriptionKa,
    string DescriptionEn,
    decimal? Price,
    Guid DishCategoryId,
    int? PreparationTimeMinutes,
    int? Calories,
    int? SpicyLevel,
    string Ingredients,
    string IngredientsEn,
    string Volume,
    string AlcoholContent,
    bool IsVeganDish,
    string Comment,
    string ImageUrl,
    string VideoUrl
)   : IRequest<Result<DishResponseDto>>;
