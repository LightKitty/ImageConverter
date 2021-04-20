using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encoder = System.Drawing.Imaging.Encoder;

namespace ImageConverter
{
    public static class Converter
    {
        public static ImageFormat GetImageFormat(string fileName)
        {
            var imgType = Path.GetExtension(fileName).TrimStart('.'); //图片后缀
            switch(imgType.ToLower())
            {
                case "jpg":
                case "jpeg":
                    return ImageFormat.Jpeg;
                case "png":
                    return ImageFormat.Png;
                default:
                    throw new BadImageFormatException();
            }
        }

        public static void Zoom(string imgPath, string newImgPath, double scale)
        {
            Zoom(imgPath, newImgPath, scale, GetImageFormat(imgPath));
        }

        public static void Zoom(string imgPath, string newImgPath, double scale, ImageFormat format)
        {
            using (var imgBmp = new Bitmap(imgPath))
            {
                int height = Convert.ToInt32(imgBmp.Height * scale);
                int width = Convert.ToInt32(imgBmp.Width * scale);
                using (var newImg = new Bitmap(imgBmp, width, height))
                {
                    newImg.Save(newImgPath, format);
                }
            }
        }
    }
}
