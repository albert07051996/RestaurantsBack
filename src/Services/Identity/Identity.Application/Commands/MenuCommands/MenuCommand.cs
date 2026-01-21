using BuildingBlocks.Shared.Common;
using Identity.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Commands.MenuCommands;

public record MenuCommand
(
    string NameKa,
    string NameEn,
    string DescriptionKa,
    string DescriptionEn,
    decimal? Price,
    Guid foodCategoryId,
    int? PreparationTimeMinutes,
    int? Calories,
    int? SpicyLevel,
    string Ingredients,
    string IngredientsEn,
    string Volume,
    string AlcoholContent,
    bool IsVeganFood,
    string Comment,
    string ImageUrl,
    string VideoUrl,
    Guid? ImagePublicId
)   : IRequest<Result<MenuResponseDto>>;
