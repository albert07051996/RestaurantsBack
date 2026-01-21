using Identity.Application.Commands;
using Identity.Application.Commands.FoodCategoriesCommands;
using Identity.Application.Commands.MenuCommands;

using Identity.Application.DTOs;
using Identity.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly IMediator _mediator;

    public MenuController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[HttpGet("getAllMenuItems")]
    //public async Task<ActionResult<List<MenuItemDto>>> GetAllMenuItems([FromQuery] string? category = null)
    //{


    //var query = _context.MenuItems.AsQueryable();

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
    [HttpGet("getAllMenuItems")]
    public async Task<IActionResult> GetAllUsers()
    {
        var query = new GetAllMenuQuery();
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return BadRequest(new { message = result.ErrorMessage });
        }

        return Ok(result.Data);
    }


    [HttpPost("addFood")]
    public async Task<IActionResult> AddFood([FromBody] CreateMenuItemDto dto)
    {
        var command = new MenuCommand(
            dto.NameKa,
            dto.NameEn,
            dto.DescriptionKa,
            dto.DescriptionEn,
            dto.Price,
            dto.FoodCategoryId,
            dto.PreparationTimeMinutes,
            dto.Calories,
            dto.SpicyLevel,
            dto.Ingredients,
            dto.IngredientsEn,
            dto.Volume,
            dto.AlcoholContent,
            dto.IsVeganFood,
            dto.Comment,
            dto.ImageUrl,
            dto.VideoUrl,
            dto.ImagePublicId
        );

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(new { errors = result.Errors, message = result.ErrorMessage });
        }

        return Ok(result.Data);
    }

    [HttpPost("addFoodCategory")]
    public async Task<IActionResult> addFoodCategory([FromBody] CreateFoodCategoryDto dto)
    {
        var command = new FoodCategoryCommand(
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
