using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OnlineShop.Application.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services
{
    public class FileService : IFileService
    {
        private readonly string _rootPath;
        private readonly string _virtualPath;
        private readonly string _relativePath;
        private readonly string _jsonRelativePath;
        private readonly string _jsonVirtualPath;

        public FileService(string rootPath, string relativePath, string virtualPath)
        {
            _rootPath = rootPath;
            _virtualPath = virtualPath;
            _relativePath = relativePath;
            _jsonRelativePath = Path.Combine(relativePath, "json");
            _jsonVirtualPath = virtualPath + "/json";
        }

        public Task DeleteAsync(string path)
        {
            string absolutePath = GetAbsoluteFilePath(path);

            if (File.Exists(absolutePath))
            {
                File.Delete(absolutePath);
            }

            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(string path)
        {
            string absolutePath = GetAbsoluteFilePath(path);
            return Task.FromResult(File.Exists(absolutePath));
        }

        public Task<Stream> OpenReadAsync(string path)
        {
            string absolutePath = GetAbsoluteFilePath(path);
            return Task.FromResult((Stream)File.OpenRead(absolutePath));
        }

        public async Task<(string path, long length)> SaveModelAsJsonAsync<T>(string filename, T model)
        {
            var jsonDirectoryPath = Path.Combine(_rootPath, _jsonRelativePath);

            if (!Directory.Exists(jsonDirectoryPath))
            {
                Directory.CreateDirectory(jsonDirectoryPath);
            }

            string uniqueFileName = GetUniqueFileName(filename);
            string path = Path.Combine(jsonDirectoryPath, uniqueFileName);

            await Task.Run(() =>
            {
                using (var sw = File.CreateText(path))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(sw, model);
                }
            });

            var length = new FileInfo(path).Length;
            var virtualPath = GetJsonFileVirtualPath(uniqueFileName);
            return (virtualPath, length);
        }

        public Task<string> UploadAsync(IFormFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(paramName: nameof(file));
            }

            return UploadAsync(file.OpenReadStream, file.FileName, uniqueName: true);
        }

        public Task<string> UploadAsync(Func<Stream> openStream, string fileName, bool uniqueName)
        {
            string actualFileName = fileName;
            if (uniqueName)
                actualFileName = GetUniqueFileName(actualFileName);

            string path = Path.Combine(_rootPath, _relativePath, actualFileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            using (var stream = openStream())
            {
                stream.CopyTo(fileStream);
            }

            string virtualPath = GetFileVirtualPath(actualFileName);
            return Task.FromResult(virtualPath);
        }
        private static string GetUniqueFileName(string fileName)
        {
            return $"{Guid.NewGuid()}_{fileName}";
        }

        private string GetFileVirtualPath(string fileName)
        {
            return $"{_virtualPath}/{fileName}";
        }

        private string GetAbsoluteFilePath(string path)
        {
            return _rootPath + path;
        }

        private string GetJsonFileVirtualPath(string fileName)
        {
            return $"{_jsonVirtualPath}/{fileName}";
        }
    }
}
