using OnlineShop.Application.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OnlineShop.Persistence.Concrete
{
    public class ImageProcessor : IImageProcessor
    {
        public void Resize(Stream input, Stream output, int width, int heigh, bool keepAspectRatio = true)
        {
            IImageFormat imageFormat = Image.DetectFormat(input);
            using (Image<Rgba32> image = Image.Load(input))
            {
                int actualWidth = width;
                int actualHeight = heigh;
                if (keepAspectRatio)
                {
                    Rectangle imageSize = image.Bounds();
                    var size = CalculateSizeKeepingAspectRatio(imageSize.Width, imageSize.Height, width, heigh);
                    actualWidth = size.width;
                    actualHeight = size.height;
                }

                image.Mutate(x => x.Resize(actualWidth, actualHeight));
                image.Save(output, imageFormat);
            }
        }

        private static (int width, int height) CalculateSizeKeepingAspectRatio(int sourceWidth, int sourceHeight, int targetWidth, int targetHeight)
        {
            if (sourceWidth == 0 || sourceHeight == 0 || targetWidth == 0 || targetHeight == 0)
                return (0, 0);

            double ratio = Math.Min(targetWidth / (double)sourceWidth, targetHeight / (double)sourceHeight);
            return (width: (int)(sourceWidth * ratio), height: (int)(sourceHeight * ratio));
        }
    }
}
