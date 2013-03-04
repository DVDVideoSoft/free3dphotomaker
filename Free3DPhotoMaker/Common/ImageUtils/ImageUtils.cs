using System;
using System.Collections.Generic;
using System.Text;
using GdPicture;
using DVDVideoSoft.Resources;

namespace DVDVideoSoft.ImageUtils
{
    public class ImageUtils
    {
        public static string GetOpenDialogFilter()
        {
            return CommonData.OpenDlgImageFilter + "|*.RAW;*.PICT;*.BMP;*.DIB;*.RLE;*.ICO;*.EMF;*.WMF;*.GIF;*.JPEG;*.JPG;*.JPE;*.JFIF;*.PNG;*.TIFF;*.TIF;*.PNM;*.PPM;*.PBM;*.PGM;*.PFM;*.RPPM;*.RPGM;*.RPBM;*.PCX;*.XPM;*.XBM;*.WBMP;*.TGA;*.SGI;*.RAS;*.PSD;*.MNG;*.JP2;*.J2K;*.JNG;*.JBIG;*.IFF;*.HDR|RAW|*.RAW|PICT|*.PICT|BMP|*.BMP|JPEG|*.JPEG;*.JPG;*.JPE;*.JFIF;*.JP2;*.J2K;|RLE|*.RLE|ICO|*.ICO|EMF|*.EMF|WMF|*.WMF|PNG|*.PNG|TARGA|*.TGA|GIF|*.GIF";
        }

        public static string GetSaveDialogFilter()
        {
            return CommonData.OpenDlgImageFilter + "|*.BMP;*.GIF;*.JPEG;*.JPG;*.PNG;*.TIFF;*.TGA;|BMP|*.BMP|JPEG|*.JPEG;*.JPG;|PNG|*.PNG|TARGA|*.TGA|GIF|*.GIF|TIFF|*.TIFF";
        }

        public static void SaveTo(GdPictureImaging ImageProcessor, int ImageID, string fileName, TiffCompression compression)
        {
            GdPictureStatus stat = GdPictureStatus.OK;
            if (fileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || fileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                stat = ImageProcessor.SaveAsJPEG(ImageID, fileName, 100);
            else
                if (fileName.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
                    stat = ImageProcessor.SaveAsBMP(ImageID, fileName);
                else
                    if (fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                        stat = ImageProcessor.SaveAsPNG(ImageID, fileName);
                    else
                        if (fileName.EndsWith(".tga", StringComparison.OrdinalIgnoreCase))
                            stat = ImageProcessor.SaveAsTGA(ImageID, fileName);
                        else
                            if (fileName.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                                stat = ImageProcessor.SaveAsGIF(ImageID, fileName);
                            else
                                if (fileName.EndsWith(".tiff", StringComparison.OrdinalIgnoreCase))
                                    stat = ImageProcessor.SaveAsTIFF(ImageID, fileName,compression);
                                else
                                    if(fileName.EndsWith(".pdf",StringComparison.OrdinalIgnoreCase))
                                        stat = ImageProcessor.SaveAsPDF(ImageID,fileName,false,fileName,"","","",Environment.UserName);

            if (stat != GdPictureStatus.OK)
                throw new Exception("Save to " + fileName + " failed with error: " + stat.ToString());
        }
    }
}
