using MediatR;
using Identity.Application.Interfaces;


using Identity.Domain.Interfaces;

namespace Identity.Application.Features.Products.Commands.UploadImage
{

    public class UploadProductImageCommandHandler
        : IRequestHandler<UploadProductImageCommand, string>
    {
        private readonly IImageStorageService _imageStorageService;
        private readonly IMenuRepository _menuRepository;

        public UploadProductImageCommandHandler(
            IImageStorageService imageStorageService,
            IMenuRepository productRepository)
        {
            _imageStorageService = imageStorageService;
            _menuRepository = productRepository;
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
            var product = await _menuRepository.GetByIdAsync(
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
            await _menuRepository.UpdateAsync(product, cancellationToken);

            // დააბრუნე URL
            return await _imageStorageService.GetImageUrlAsync(publicId);
        }
    }
}
