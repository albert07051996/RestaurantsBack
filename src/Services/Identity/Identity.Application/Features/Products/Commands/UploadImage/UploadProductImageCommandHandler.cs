using MediatR;
using Identity.Application.Interfaces;


using Identity.Domain.Interfaces;

namespace Identity.Application.Features.Products.Commands.UploadImage
{

    public class UploadProductImageCommandHandler
        : IRequestHandler<UploadProductImageCommand, string>
    {
        private readonly IImageStorageService _imageStorageService;
        private readonly IDishRepository _DishRepository;

        public UploadProductImageCommandHandler(
            IImageStorageService imageStorageService,
            IDishRepository productRepository)
        {
            _imageStorageService = imageStorageService;
            _DishRepository = productRepository;
        }

        public async Task<string> Handle(
            UploadProductImageCommand request,
            CancellationToken cancellationToken)
        {
            // ატვირთე სურათი Cloudinary-ზე
            var publicId = await _imageStorageService.UploadImageAsync(
                request.ImageStream,
                request.FileName,
                cancellationToken
            );

            // მოძებნე პროდუქტი
            var product = await _DishRepository.GetByIdAsync(
                request.ProductId,
                cancellationToken
            );

            if (product == null)
            {
                // თუ არ მოიძებნა, წაშალე ატვირთული სურათი
                await _imageStorageService.DeleteImageAsync(publicId, cancellationToken);
                throw new Exception("Product not found");
            }

            // წინა სურათის წაშლა (თუ არსებობს)
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                await _imageStorageService.DeleteImageAsync(
                    product.ImageUrl,
                    cancellationToken
                );
            }

            // განაახლე პროდუქტი
            product.ImageUrl = publicId;
            await _DishRepository.UpdateAsync(product, cancellationToken);

            // დააბრუნე URL
            return await _imageStorageService.GetImageUrlAsync(publicId);
        }
    }
}
