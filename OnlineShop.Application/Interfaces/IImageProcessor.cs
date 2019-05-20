using System.IO;

namespace OnlineShop.Application.Interfaces
{
    public interface IImageProcessor
    {
        void Resize(Stream input, Stream output, int width, int heigh, bool keepAspectRatio = true);
    }
}
