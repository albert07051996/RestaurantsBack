using MediatR;

namespace Identity.Application.Features.Products.Commands.UploadImage
{
    public record UploadProductImageCommand(
       Guid ProductId,
       Stream ImageStream,
       string FileName
   ) : IRequest<string>;
}
