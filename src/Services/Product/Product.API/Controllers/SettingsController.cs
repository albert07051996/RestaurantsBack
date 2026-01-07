using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Application.DTOs;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using Product.Infrastructure.Data;

namespace Product.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SettingsController : ControllerBase
{
    private readonly ProductDbContext _context;
    private readonly IFileUploadService _fileUploadService;

    public SettingsController(ProductDbContext context, IFileUploadService fileUploadService)
    {
        _context = context;
        _fileUploadService = fileUploadService;
    }

    // Get restaurant settings
    [HttpGet]
    public async Task<ActionResult<RestaurantSettingsDto>> GetSettings()
    {
        var settings = await _context.RestaurantSettings.FirstOrDefaultAsync();
        
        if (settings == null)
        {
            // Return default settings if none exist
            return Ok(new RestaurantSettingsDto(
                Guid.Empty,
                "სუფრა",
                "SUPRA",
                "ტრადიციული ქართული სამზარეულო",
                "Traditional Georgian Cuisine",
                "ვაჟა-ფშაველას გამზირი 42, თბილისი",
                "+995 555 789 123",
                "info@supra.ge",
                "",
                "",
                "ღია ყოველდღე: 11:00 - 23:00",
                "#d4af37",
                "#c19a6b"
            ));
        }

        var dto = new RestaurantSettingsDto(
            settings.Id,
            settings.RestaurantName,
            settings.RestaurantNameEn,
            settings.TaglineKa,
            settings.TaglineEn,
            settings.Address,
            settings.Phone,
            settings.Email,
            settings.BackgroundImageUrl,
            settings.LogoUrl,
            settings.OpeningHours,
            settings.PrimaryColor,
            settings.SecondaryColor
        );

        return Ok(dto);
    }

    // Update restaurant settings
    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<RestaurantSettingsDto>> UpdateSettings([FromBody] UpdateRestaurantSettingsDto dto)
    {
        var settings = await _context.RestaurantSettings.FirstOrDefaultAsync();

        if (settings == null)
        {
            // Create new settings
            settings = new RestaurantSettings(
                dto.RestaurantName,
                dto.RestaurantNameEn,
                dto.TaglineKa,
                dto.TaglineEn,
                dto.Address,
                dto.Phone,
                dto.Email
            );
            settings.UpdateColors(dto.PrimaryColor, dto.SecondaryColor);
            _context.RestaurantSettings.Add(settings);
        }
        else
        {
            settings.Update(
                dto.RestaurantName,
                dto.RestaurantNameEn,
                dto.TaglineKa,
                dto.TaglineEn,
                dto.Address,
                dto.Phone,
                dto.Email,
                dto.OpeningHours
            );
            settings.UpdateColors(dto.PrimaryColor, dto.SecondaryColor);
        }

        await _context.SaveChangesAsync();

        var result = new RestaurantSettingsDto(
            settings.Id,
            settings.RestaurantName,
            settings.RestaurantNameEn,
            settings.TaglineKa,
            settings.TaglineEn,
            settings.Address,
            settings.Phone,
            settings.Email,
            settings.BackgroundImageUrl,
            settings.LogoUrl,
            settings.OpeningHours,
            settings.PrimaryColor,
            settings.SecondaryColor
        );

        return Ok(result);
    }

    // Upload background image
    [HttpPost("background")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<string>> UploadBackground(IFormFile file)
    {
        try
        {
            var settings = await _context.RestaurantSettings.FirstOrDefaultAsync();
            if (settings == null)
            {
                return BadRequest("Please create settings first");
            }

            // Delete old background if exists
            if (!string.IsNullOrEmpty(settings.BackgroundImageUrl))
            {
                await _fileUploadService.DeleteFileAsync(settings.BackgroundImageUrl);
            }

            // Upload new background
            using (var stream = file.OpenReadStream())
            {
                var imageUrl = await _fileUploadService.UploadImageAsync(stream, file.FileName, "backgrounds");
                settings.UpdateBackgroundImage(imageUrl);
                await _context.SaveChangesAsync();
                
                return Ok(new { url = imageUrl });
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    // Upload logo
    [HttpPost("logo")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<string>> UploadLogo(IFormFile file)
    {
        try
        {
            var settings = await _context.RestaurantSettings.FirstOrDefaultAsync();
            if (settings == null)
            {
                return BadRequest("Please create settings first");
            }

            // Delete old logo if exists
            if (!string.IsNullOrEmpty(settings.LogoUrl))
            {
                await _fileUploadService.DeleteFileAsync(settings.LogoUrl);
            }

            // Upload new logo
            using (var stream = file.OpenReadStream())
            {
                var imageUrl = await _fileUploadService.UploadImageAsync(stream, file.FileName, "logos");
                settings.UpdateLogo(imageUrl);
                await _context.SaveChangesAsync();
                
                return Ok(new { url = imageUrl });
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
