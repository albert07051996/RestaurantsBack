using MediatR;
using Microsoft.AspNetCore.Mvc;
using Identity.Application.Features.Products.Commands.UploadImage;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{id}/image")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImage(
            Guid id,
            IFormFile image,
            CancellationToken cancellationToken)
        {
            if (image == null || image.Length == 0)
                return BadRequest("No image provided");

            // ვალიდაცია
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
                return BadRequest("Invalid file type");

            if (image.Length > 5 * 1024 * 1024) // 5MB
                return BadRequest("File too large");

            await using var stream = image.OpenReadStream();

            var command = new UploadProductImageCommand(
                id,
                stream,
                image.FileName
            );

            var imageUrl = await _mediator.Send(command, cancellationToken);

            return Ok(new { imageUrl });
        }
    }
}