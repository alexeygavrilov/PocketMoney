using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;

namespace PocketMoney.FileSystem
{
    public struct ThumbnailOptions
    {
        public ThumbnailOptions(int width, int height, ThumbnailAlign align, bool color)
        {
            this.width = width;
            this.height = height;
            this.align = align;
            this.color = color;
        }

        public ThumbnailOptions(Size size, ThumbnailAlign align, bool color)
        {
            this.width = size.Width;
            this.height = size.Height;
            this.align = align;
            this.color = color;
        }

        private ThumbnailAlign align;

        public ThumbnailAlign Align
        {
            get { return align; }
            set { align = value; }
        }

        private int width;

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        private int height;

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        private bool color;

        public bool EnableColor
        {
            get { return color; }
            set { color = value; }
        }

        public Size Size
        {
            get
            {
                return new Size(width, height);
            }

            set
            {
                height = value.Height;
                width = value.Width;
            }
        }
    }

    //   Align
    //
    //2 -- 3 -- 4 
    //|         |
    //|         |
    //9    1    5
    //|         |
    //|         |
    //8 -- 7 -- 6
    //0 - The original image will be returned as thumbnail if its width and height are not greater than specified values.
    public enum ThumbnailAlign : int
    {
        Original = 0,
        Center = 1,
        LeftTop = 2,
        CenterTop = 3,
        RightTop = 4,
        RightCenter = 5,
        RightBottom = 6,
        CenterBottom = 7,
        LeftBottom = 8,
        LeftCenter = 9
    }

    public static class ImageUtilities
    {

        public static void DrawImage(Bitmap image, int x, int y, int width, int height, bool enableColor, ref Bitmap bitmap)
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                //graphics.Clear(Color.White);
                graphics.CompositingMode = CompositingMode.SourceOver;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                image.MakeTransparent();
                if (enableColor)
                {
                    graphics.DrawImage(image, x, y, width, height);
                }
                else
                {
                    //create the grayscale ColorMatrix
                    ColorMatrix colorMatrix = new ColorMatrix(
                       new float[][] 
                        {
                            new float[] {.3f, .3f, .3f, 0, 0},
                            new float[] {.59f, .59f, .59f, 0, 0},
                            new float[] {.11f, .11f, .11f, 0, 0},
                            new float[] {0, 0, 0, 1, 0},
                            new float[] {0, 0, 0, 0, 1}
                        });

                    ImageAttributes attributes = new ImageAttributes();

                    //set the color matrix attribute
                    attributes.SetColorMatrix(colorMatrix);
                    graphics.DrawImage(image, new Rectangle(x, y, width, height),
                       x, y, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
                graphics.Flush();
            }
        }

        public static void GetAlignPosition(ThumbnailAlign align, int maxWidth, int maxHeight, int localSizeX, int localSizeY, ref int left, ref int top)
        {
            left = 0;
            top = 0;

            switch (align)
            {
                case ThumbnailAlign.Center:
                    left = (maxWidth - localSizeX) / 2;
                    top = (maxHeight - localSizeY) / 2;
                    break;
                case ThumbnailAlign.LeftTop:
                    left = 0;
                    top = 0;
                    break;
                case ThumbnailAlign.CenterTop:
                    left = (maxWidth - localSizeX) / 2;
                    top = 0;
                    break;
                case ThumbnailAlign.RightTop:
                    left = maxWidth - localSizeX;
                    top = 0;
                    break;
                case ThumbnailAlign.RightCenter:
                    left = maxWidth - localSizeX;
                    top = (maxHeight - localSizeY) / 2;
                    break;
                case ThumbnailAlign.RightBottom:
                    left = maxWidth - localSizeX;
                    top = maxHeight - localSizeY;
                    break;
                case ThumbnailAlign.CenterBottom:
                    left = (maxWidth - localSizeX) / 2;
                    top = maxHeight - localSizeY;
                    break;
                case ThumbnailAlign.LeftBottom:
                    left = 0;
                    top = maxHeight - localSizeY;
                    break;
                case ThumbnailAlign.LeftCenter:
                    left = 0;
                    top = (maxHeight - localSizeY) / 2;
                    break;
            }
        }

        public static void GetProportionalSize(int originalWidth, int originalHeight, ref int width, ref int height)
        {
            double multiplier = (double)originalHeight / (double)originalWidth;

            if (height <= 0)
            {
                height = Convert.ToInt32(((multiplier > 1) ? (width / multiplier) : (width * multiplier)), CultureInfo.InvariantCulture);
                return;
            }
            else if (width <= 0)
            {
                width = Convert.ToInt32(((multiplier > 1) ? (height * multiplier) : (height / multiplier)), CultureInfo.InvariantCulture);
                return;
            }

            double widthPercent = ((originalWidth > 0) ? 100 * width / originalWidth : 100);
            double heightPercent = ((originalHeight > 0) ? 100 * height / originalHeight : 100);

            double currentPercent = ((widthPercent > heightPercent) ? heightPercent : widthPercent);
            if (currentPercent == 0)
            {
                if (heightPercent != 0)
                    currentPercent = heightPercent;
                else
                    currentPercent = widthPercent;
            }
            currentPercent = currentPercent / 100;

            width = Convert.ToInt32(originalWidth * currentPercent, CultureInfo.InvariantCulture);
            height = Convert.ToInt32(originalHeight * currentPercent, CultureInfo.InvariantCulture);
        }

        public static Stream Thumbnail(Stream stream, ThumbnailOptions option, ImageFormat format)
        {
            Stream outputStream = new MemoryStream();
            Bitmap originalImage = new Bitmap(stream);

            if (option.Align == ThumbnailAlign.Original && option.EnableColor)
            {
                if ((((option.Width > 0) && (originalImage.Width <= option.Width)) || (option.Width <= 0))
                    && (((option.Height > 0) && (originalImage.Height <= option.Height)) || (option.Height <= 0)))
                {
                    return stream;
                }
            }

            int outputWidth = option.Width;
            int outputHeight = option.Height;
            ImageUtilities.GetProportionalSize(originalImage.Width, originalImage.Height, ref outputWidth, ref outputHeight);

            Bitmap scaledImage = new Bitmap(outputWidth, outputHeight);
            ImageUtilities.DrawImage(originalImage, 0, 0, outputWidth, outputHeight, option.EnableColor, ref scaledImage);

            if (option.Align != ThumbnailAlign.Original)
            {
                if (option.Width == 0) option.Width = outputWidth;
                if (option.Height == 0) option.Height = outputHeight;
                int maxWidth = ((outputWidth > option.Width) ? outputWidth : option.Width);
                int maxHeight = ((outputHeight > option.Height) ? outputHeight : option.Height);

                int x = 0;
                int y = 0;
                ImageUtilities.GetAlignPosition(option.Align, maxWidth, maxHeight, outputWidth, outputHeight, ref x, ref y);

                Bitmap outputImage = new Bitmap(maxWidth, maxHeight);
                ImageUtilities.DrawImage(scaledImage, x, y, outputWidth, outputHeight, true, ref outputImage);
                outputImage.Save(outputStream, format);
                outputImage.Dispose();
            }
            else
            {
                scaledImage.Save(outputStream, format);
            }
            scaledImage.Dispose();
            originalImage.Dispose();
            return outputStream;
        }

    }

}