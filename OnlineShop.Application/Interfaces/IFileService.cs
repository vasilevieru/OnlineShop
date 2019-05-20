using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineShop.Application.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile file);

        Task<string> UploadAsync(Func<Stream> openStream, string fileName, bool uniqueName);

        Task<(string path, long length)> SaveModelAsJsonAsync<T>(string filename, T model);

        Task DeleteAsync(string path);

        Task<Stream> OpenReadAsync(string path);

        Task<bool> ExistsAsync(string path);
    }
}
