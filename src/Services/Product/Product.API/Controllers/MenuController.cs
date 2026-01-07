using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Application.DTOs;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using Product.Infrastructure.Data;
using System.Text.Json;

namespace Product.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly ProductDbContext _context;
    private readonly IFileUploadService _fileUploadService;

    public MenuController(ProductDbContext context, IFileUploadService fileUploadService)
    {
        _context = context;
        _fileUploadService = fileUploadService;
    }

    // Get all menu items
    [HttpGet]
    public async Task<ActionResult<List<MenuItemDto>>> GetMenuItems([FromQuery] string? category = null)
    {
        var query = _context.MenuItems.AsQueryable();

        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(m => m.Category == category);
        }

        var items = await query
            .Where(m => m.IsAvailable)
            .OrderBy(m => m.SortOrder)
            .ThenBy(m => m.NameEn)
            .ToListAsync();

        var dtos = items.Select(item => new MenuItemDto(
            item.Id,
            item.NameKa,
            item.NameEn,
            item.DescriptionKa,
            item.DescriptionEn,
            item.Price,
            item.Category,
            item.ImageUrl,
            item.VideoUrl,
            item.PreparationTimeMinutes,
            item.Calories,
            item.SpicyLevel,
            JsonSerializer.Deserialize<List<string>>(item.IngredientsJson) ?? new List<string>(),
            JsonSerializer.Deserialize<List<string>>(item.AllergensJson) ?? new List<string>(),
            item.IsAvailable,
            item.SortOrder,
            item.Volume,
            item.AlcoholContent,
            item.ServingTemperature,
            item.CreatedAt
        )).ToList();

        return Ok(dtos);
    }

    // Get menu item by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<MenuItemDto>> GetMenuItem(Guid id)
    {
        var item = await _context.MenuItems.FindAsync(id);
        if (item == null)
            return NotFound();

        var dto = new MenuItemDto(
            item.Id,
            item.NameKa,
            item.NameEn,
            item.DescriptionKa,
            item.DescriptionEn,
            item.Price,
            item.Category,
            item.ImageUrl,
            item.VideoUrl,
            item.PreparationTimeMinutes,
            item.Calories,
            item.SpicyLevel,
            JsonSerializer.Deserialize<List<string>>(item.IngredientsJson) ?? new List<string>(),
            JsonSerializer.Deserialize<List<string>>(item.AllergensJson) ?? new List<string>(),
            item.IsAvailable,
            item.SortOrder,
            item.Volume,
            item.AlcoholContent,
            item.ServingTemperature,
            item.CreatedAt
        );

        return Ok(dto);
    }

    // Create menu item
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<MenuItemDto>> CreateMenuItem([FromForm] CreateMenuItemDto dto, IFormFile image, IFormFile? video = null)
    {
        try
        {
            // Upload image
            string imageUrl;
            using (var imageStream = image.OpenReadStream())
            {
                imageUrl = await _fileUploadService.UploadImageAsync(imageStream, image.FileName, "menu-items");
            }

            // Upload video if provided
            string? videoUrl = null;
            if (video != null)
            {
                using (var videoStream = video.OpenReadStream())
                {
                    videoUrl = await _fileUploadService.UploadVideoAsync(videoStream, video.FileName);
                }
            }

            var menuItem = new MenuItem(
                dto.NameKa,
                dto.NameEn,
                dto.DescriptionKa,
                dto.DescriptionEn,
                dto.Price,
                dto.Category,
                imageUrl,
                dto.PreparationTimeMinutes,
                dto.Calories,
                dto.SpicyLevel
            );

            menuItem.UpdateIngredients(JsonSerializer.Serialize(dto.Ingredients));
            menuItem.UpdateAllergens(JsonSerializer.Serialize(dto.Allergens));
            
            if (video != null)
                menuItem.UpdateVideo(videoUrl);

            if (!string.IsNullOrEmpty(dto.Volume) || !string.IsNullOrEmpty(dto.AlcoholContent))
                menuItem.SetDrinkInfo(dto.Volume, dto.AlcoholContent, dto.ServingTemperature);

            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();

            var result = new MenuItemDto(
                menuItem.Id,
                menuItem.NameKa,
                menuItem.NameEn,
                menuItem.DescriptionKa,
                menuItem.DescriptionEn,
                menuItem.Price,
                menuItem.Category,
                menuItem.ImageUrl,
                menuItem.VideoUrl,
                menuItem.PreparationTimeMinutes,
                menuItem.Calories,
                menuItem.SpicyLevel,
                dto.Ingredients,
                dto.Allergens,
                menuItem.IsAvailable,
                menuItem.SortOrder,
                menuItem.Volume,
                menuItem.AlcoholContent,
                menuItem.ServingTemperature,
                menuItem.CreatedAt
            );

            return CreatedAtAction(nameof(GetMenuItem), new { id = menuItem.Id }, result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    // Update menu item
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<MenuItemDto>> UpdateMenuItem(Guid id, [FromForm] UpdateMenuItemDto dto, IFormFile? image = null, IFormFile? video = null)
    {
        var menuItem = await _context.MenuItems.FindAsync(id);
        if (menuItem == null)
            return NotFound();

        try
        {
            // Update image if provided
            if (image != null)
            {
                // Delete old image
                await _fileUploadService.DeleteFileAsync(menuItem.ImageUrl);
                
                // Upload new image
                using (var imageStream = image.OpenReadStream())
                {
                    var imageUrl = await _fileUploadService.UploadImageAsync(imageStream, image.FileName, "menu-items");
                    menuItem.UpdateImage(imageUrl);
                }
            }

            // Update video if provided
            if (video != null)
            {
                // Delete old video if exists
                if (!string.IsNullOrEmpty(menuItem.VideoUrl))
                    await _fileUploadService.DeleteFileAsync(menuItem.VideoUrl);
                
                // Upload new video
                using (var videoStream = video.OpenReadStream())
                {
                    var videoUrl = await _fileUploadService.UploadVideoAsync(videoStream, video.FileName);
                    menuItem.UpdateVideo(videoUrl);
                }
            }

            menuItem.Update(
                dto.NameKa,
                dto.NameEn,
                dto.DescriptionKa,
                dto.DescriptionEn,
                dto.Price,
                dto.Category,
                dto.PreparationTimeMinutes,
                dto.Calories,
                dto.SpicyLevel
            );

            menuItem.UpdateIngredients(JsonSerializer.Serialize(dto.Ingredients));
            menuItem.UpdateAllergens(JsonSerializer.Serialize(dto.Allergens));
            menuItem.SetDrinkInfo(dto.Volume, dto.AlcoholContent, dto.ServingTemperature);

            await _context.SaveChangesAsync();

            var result = new MenuItemDto(
                menuItem.Id,
                menuItem.NameKa,
                menuItem.NameEn,
                menuItem.DescriptionKa,
                menuItem.DescriptionEn,
                menuItem.Price,
                menuItem.Category,
                menuItem.ImageUrl,
                menuItem.VideoUrl,
                menuItem.PreparationTimeMinutes,
                menuItem.Calories,
                menuItem.SpicyLevel,
                dto.Ingredients,
                dto.Allergens,
                menuItem.IsAvailable,
                menuItem.SortOrder,
                menuItem.Volume,
                menuItem.AlcoholContent,
                menuItem.ServingTemperature,
                menuItem.CreatedAt
            );

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    // Delete menu item
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteMenuItem(Guid id)
    {
        var menuItem = await _context.MenuItems.FindAsync(id);
        if (menuItem == null)
            return NotFound();

        // Delete associated files
        await _fileUploadService.DeleteFileAsync(menuItem.ImageUrl);
        if (!string.IsNullOrEmpty(menuItem.VideoUrl))
            await _fileUploadService.DeleteFileAsync(menuItem.VideoUrl);

        menuItem.MarkAsDeleted();
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Update availability
    [HttpPatch("{id}/availability")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAvailability(Guid id, [FromBody] bool isAvailable)
    {
        var menuItem = await _context.MenuItems.FindAsync(id);
        if (menuItem == null)
            return NotFound();

        menuItem.SetAvailability(isAvailable);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Get categories
    [HttpGet("categories")]
    public async Task<ActionResult<List<string>>> GetCategories()
    {
        var categories = await _context.MenuItems
            .Where(m => m.IsAvailable)
            .Select(m => m.Category)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();

        return Ok(categories);
    }
}
