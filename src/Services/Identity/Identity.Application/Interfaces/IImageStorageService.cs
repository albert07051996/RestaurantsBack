using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Interfaces
{
    public interface IImageStorageService
    {
        Task<string> UploadImageAsync(Stream imageStream, string fileName, CancellationToken cancellationToken = default);
        Task<bool> DeleteImageAsync(string publicId, CancellationToken cancellationToken = default);
        Task<string> GetImageUrlAsync(string publicId, int? width = null, int? height = null);
    }
}
