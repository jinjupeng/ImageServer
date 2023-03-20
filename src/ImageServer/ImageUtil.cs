using SkiaSharp;

namespace ImageServer
{
    public static class ImageUtil
    {
        /// <summary>
        /// 创建图像的缩略图。使用SkiaSharp，以支持跨平台。
        /// </summary>
        /// <param name="fileOriginalPath">原图文件全路径</param>
        /// <param name="fileThumbnailPath">将生成的缩略图文件全路径</param>
        /// <param name="width">缩略图宽</param>
        /// <param name="height">缩略图高</param>
        /// <param name="skEncodedImageFormat">默认png文件</param>
        /// <returns>生成缩略图的文件名称</returns>
        public static string MakeThumb(string fileOriginalPath, string fileThumbnailPath, int width, int height, SKEncodedImageFormat skEncodedImageFormat = SKEncodedImageFormat.Png)
        {
            const int quality = 100; //质量为[SKFilterQuality.Medium]结果的100%
            using var input = System.IO.File.OpenRead(fileOriginalPath);
            using var inputStream = new SKManagedStream(input);
            using var original = SKBitmap.Decode(inputStream);
            using (var resized = original.Resize(new SKImageInfo(width, height), SKFilterQuality.Medium))
            {
                if (resized == null) return "";
                using var image = SKImage.FromBitmap(resized);
                using var output = System.IO.File.OpenWrite(fileThumbnailPath);
                image.Encode(skEncodedImageFormat, quality).SaveTo(output);
            }
            return fileThumbnailPath;
        }
    }
}
