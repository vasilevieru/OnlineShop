using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace OnlineShop.Application.Interfaces
{
    public interface IImageFileService
    {
        Task<string> UploadAsync(IFormFile imageFile);

        Task DeleteAsync(string path);

        Task<Stream> OpenReadAsync(string imagePath);

        Task<Stream> OpenReadThumbnailAsync(string path);

        string GetThumbnailPath(string imagePath);

    }
}
