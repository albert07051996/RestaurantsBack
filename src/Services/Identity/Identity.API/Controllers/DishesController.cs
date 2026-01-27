using Identity.Application.Commands;
using Identity.Application.Commands.DishCategoriesCommands;
using Identity.Application.Commands.DishCommands;

using Identity.Application.DTOs;
using Identity.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DishesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DishesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[HttpGet("getAllDishItems")]
    //public async Task<ActionResult<List<DishItemDto>>> GetAllDishItems([FromQuery] string? category = null)
    //{


    //var query = _context.DishItems.AsQueryable();

    //if (!string.IsNullOrEmpty(category))
    //{
    //    query = query.Where(m => m.Category == category);
    //}

    //var items = await query
    //    .Where(m => m.IsAvailable)
    //    .OrderBy(m => m.SortOrder)
    //    .ThenBy(m => m.NameEn)
    //    .ToListAsync();

    //var dtos = items.Select(ToDto).ToList();
    //return Ok(dtos);
    //}
    [HttpGet("getAllDishes")]
    public async Task<IActionResult> GetAllUsers()
    {
        var query = new GetAllDishesQuery();
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return BadRequest(new { message = result.ErrorMessage });
        }

        return Ok(result.Data);
    }


    [HttpPost("addDish")]
    public async Task<IActionResult> AddDish([FromBody] CreateDishDto dto)
    {
        var command = new DishCommand(
            dto.NameKa,
            dto.NameEn,
            dto.DescriptionKa,
            dto.DescriptionEn,
            dto.Price,
            dto.DishCategoryId,
            dto.PreparationTimeMinutes,
            dto.Calories,
            dto.SpicyLevel,
            dto.Ingredients,
            dto.IngredientsEn,
            dto.Volume,
            dto.AlcoholContent,
            dto.IsVeganDish,
            dto.Comment,
            dto.ImageUrl,
            dto.VideoUrl
        );

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(new { errors = result.Errors, message = result.ErrorMessage });
        }

        return Ok(result.Data);
    }

    [HttpPost("addDishCategory")]
    public async Task<IActionResult> addDishCategory([FromBody] CreateDishCategoryDto dto)
    {
        var command = new DishCategoryCommand(
            dto.NameKa,
            dto.NameEn,
            dto.DescriptionKa,
            dto.DescriptionEn,
            dto.Priority,
            dto.ImageUrl


        );

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(new { errors = result.Errors, message = result.ErrorMessage });
        }

        return Ok(result.Data);
    }
}
