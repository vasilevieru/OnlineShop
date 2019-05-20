using Microsoft.AspNetCore.Http;
using OnlineShop.Application.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services
{
    public class ImageFileService : IImageFileService
    {
        private readonly IFileService _fileService;
        private readonly IImageProcessor _imageProcessor;
        private readonly ImageThumbnailConfiguration _thumbnailConfig;

        public ImageFileService(IFileService fileService, IImageProcessor imageProcessor, ImageThumbnailConfiguration thumbnailConfig)
        {
            _fileService = fileService;
            _imageProcessor = imageProcessor;
            _thumbnailConfig = thumbnailConfig;
        }

        public Task DeleteAsync(string imagePath)
        {
            Task deleteImageTask = _fileService.DeleteAsync(imagePath);

            string thumbPath = GetThumbnailPath(imagePath);
            Task deleteThumbTask = _fileService.DeleteAsync(thumbPath);

            return Task.WhenAll(deleteImageTask, deleteThumbTask);
        }

        public Task<Stream> OpenReadAsync(string imagePath)
        {
            return _fileService.OpenReadAsync(imagePath);
        }

        public Task<string> UploadAsync(IFormFile imageFile)
        {
            return _fileService.UploadAsync(imageFile);
        }

        public string GetThumbnailPath(string imagePath)
        {
            string ext = Path.GetExtension(imagePath);
            return Path.ChangeExtension(imagePath, "thumb" + ext);
        }

        public async Task<Stream> OpenReadThumbnailAsync(string imagePath)
        {
            string thumbPath = GetThumbnailPath(imagePath);
            await EnsureThumbnailIsCreatedAsync(imagePath, thumbPath);
            return await _fileService.OpenReadAsync(thumbPath);
        }

        private async Task EnsureThumbnailIsCreatedAsync(string imagePath, string thumbPath)
        {
            bool thumbExists = await _fileService.ExistsAsync(thumbPath);
            if (!thumbExists)
            {
                await CreateThumbnailAsync(imagePath, thumbPath);
            }
        }

        private async Task CreateThumbnailAsync(string imagePath, string thumbPath)
        {
            bool imageExists = await _fileService.ExistsAsync(imagePath);
            if (!imageExists)
            {
                throw new InvalidOperationException($"Can't create a thumbnail. Image '{imagePath}' doesn't exist.");
            }

            using (MemoryStream thumbStream = new MemoryStream())
            {
                using (Stream imageStream = await _fileService.OpenReadAsync(imagePath))
                {
                    _imageProcessor.Resize(imageStream, thumbStream, _thumbnailConfig.Width, _thumbnailConfig.Height);
                }
                thumbStream.Flush();
                thumbStream.Position = 0;
                string thumbName = Path.GetFileName(thumbPath);
                await _fileService.UploadAsync(() => thumbStream, thumbName, uniqueName: false);
            }
        }
    }
}
