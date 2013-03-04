using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DVDVideoSoft.Utils
{
    public class GraphicsUtils
    {
        public static System.Drawing.Drawing2D.GraphicsPath GetRoundedRect(RectangleF baseRect, float radius)
        {
            // if corner radius is less than or equal to zero, 
            // return the original rectangle 
            if (radius <= 0.0F)
            {
                System.Drawing.Drawing2D.GraphicsPath mPath = new System.Drawing.Drawing2D.GraphicsPath();
                mPath.AddRectangle(baseRect);
                mPath.CloseFigure();
                return mPath;
            }

            // if the corner radius is greater than or equal to 
            // half the width, or height (whichever is shorter) 
            // then return a capsule instead of a lozenge 
            if (radius >= (Math.Min(baseRect.Width, baseRect.Height)) / 2.0)
                return GetCapsule(baseRect);

            // create the arc for the rectangle sides and declare 
            // a graphics path object for the drawing 
            float diameter = radius * 2.0F;
            SizeF sizeF = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(baseRect.Location, sizeF);
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

            // top left arc 
            path.AddArc(arc, 180, 90);

            // top right arc 
            arc.X = baseRect.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc 
            arc.Y = baseRect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc
            arc.X = baseRect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }


        public static System.Drawing.Drawing2D.GraphicsPath GetCapsule(RectangleF baseRect)
        {
            float diameter;
            RectangleF arc;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            try
            {
                if (baseRect.Width > baseRect.Height)
                {
                    // return horizontal capsule 
                    diameter = baseRect.Height;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 90, 180);
                    arc.X = baseRect.Right - diameter;
                    path.AddArc(arc, 270, 180);
                }
                else if (baseRect.Width < baseRect.Height)
                {
                    // return vertical capsule 
                    diameter = baseRect.Width;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 180, 180);
                    arc.Y = baseRect.Bottom - diameter;
                    path.AddArc(arc, 0, 180);
                }
                else
                {
                    // return circle 
                    path.AddEllipse(baseRect);
                }
            }
            catch (Exception)
            {
                path.AddEllipse(baseRect);
            }
            finally
            {
                path.CloseFigure();
            }
            return path;
        }

        public static Bitmap GetBitmapClip(Bitmap bitmap, int x, int y, int width, int height)
        {
            if (bitmap == null)
                return null;

            Bitmap bmp = new Bitmap(width, height);
            Graphics gc = Graphics.FromImage(bmp);
            gc.DrawImage(bitmap, new RectangleF(0, 0, width, height), new RectangleF(x, y, width, height), GraphicsUnit.Pixel);
            gc.Dispose();
            return bmp;
        }

        public static void AdjustArtworkImage(string fileName, int size)
        {
            Image src = null;
            try
            {
                src = Image.FromFile(fileName);
            }
            catch { }

            if (src == null)
                throw new ArgumentNullException();

            int width = size;
            int height = size;

            int normalizedW = width;
            int normalizedH = height;
            float resizeMultiplier = Math.Max((float)src.Width / width, (float)src.Height / height);
            if (resizeMultiplier > 1.0f)
            {
                normalizedW = (int)Math.Round((float)src.Width / resizeMultiplier);
                normalizedH = (int)Math.Round((float)src.Height / resizeMultiplier);
            }

            int xOffset = (size - normalizedW) / 2;
            int yOffset = (size - normalizedH) / 2;
            if (xOffset < 0)
                xOffset = 0;
            if (yOffset < 0)
                yOffset = 0;

            Bitmap bmp = new Bitmap(size, size);
            Graphics gc = Graphics.FromImage(bmp);
            gc.FillRectangle(Brushes.Black, new Rectangle(0, 0, width, height));

            gc.DrawImage(src, new RectangleF(xOffset, yOffset, normalizedW, normalizedH),
                         new RectangleF(0, 0, src.Width, src.Height),
                         GraphicsUnit.Pixel);
            gc.Dispose();
            src.Dispose();
            try
            {
                ImageCodecInfo codecInfo = GetEncoderInfo(ImageFormat.Jpeg);
                EncoderParameters parameters = new EncoderParameters(1);
                parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 75L);
                bmp.Save(fileName, codecInfo, parameters);
            }
            catch { }
        }

        public static Image AdjustArtworkImage(Image src, Color backColor, int width, int height)
        {
            if (src == null)
                throw new ArgumentNullException();

            int normalizedW = width;
            int normalizedH = height;
            float resizeMultiplier = Math.Max((float)src.Width / width, (float)src.Height / height);
            if (resizeMultiplier > 1.0f)
            {
                normalizedW = (int)Math.Round((float)src.Width / resizeMultiplier);
                normalizedH = (int)Math.Round((float)src.Height / resizeMultiplier);
            }

            int xOffset = (width - normalizedW) / 2;
            int yOffset = (height - normalizedH) / 2;
            if (xOffset < 0)
                xOffset = 0;
            if (yOffset < 0)
                yOffset = 0;

            Bitmap bmp = new Bitmap(width, height);
            Graphics gc = Graphics.FromImage(bmp);
            gc.FillRectangle(new SolidBrush(backColor), new Rectangle(0, 0, width, height));

            gc.DrawImage(src, new RectangleF(xOffset, yOffset, normalizedW, normalizedH),
                         new RectangleF(0, 0, src.Width, src.Height),
                         GraphicsUnit.Pixel);
            gc.Dispose();
            return bmp;
        }

        public static ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return new List<ImageCodecInfo>(ImageCodecInfo.GetImageEncoders()).Find(delegate(ImageCodecInfo codec)
            {
                return codec.FormatID == format.Guid;
            });
        }

        public static Image AdjustYouTubeThumbnail(Image src, int width = 120, int height = 68)
        {
            if (src == null)
                return null;

            int thumbWidth = width, thumbHeight = height;
            int yOffset = 22;
            Bitmap ret = new Bitmap(thumbWidth, thumbHeight);
            Graphics gc = Graphics.FromImage(ret);
            gc.FillRectangle(Brushes.Black, new Rectangle(0, 0, thumbWidth, thumbHeight));
            int yy = src.Height - yOffset;
            gc.DrawImage(src, new Rectangle(0, 0, thumbWidth, thumbHeight),
                         new Rectangle(0, yOffset / 2, src.Width, yy),
                         GraphicsUnit.Pixel);
            gc.Dispose();

            return ret;
        }

        public static Bitmap ConvertToGrayscale(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

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

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }
    }

    public class GDI32
    {
        public const int SRCCOPY = 13369376;

        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern IntPtr DeleteDC(IntPtr hDc);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern IntPtr DeleteObject(IntPtr hDc);

        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        public static extern bool BitBlt(IntPtr hdcDest, int xDest,
                   int yDest, int wDest, int hDest, IntPtr hdcSource,
                   int xSrc, int ySrc, int RasterOp);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc,
                   int nWidth, int nHeight);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);
    }
}
