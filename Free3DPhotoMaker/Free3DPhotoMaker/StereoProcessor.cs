using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using DVDVideoSoft.Utils;
using GdPicture;
using System.Runtime.InteropServices;
using System.IO;

namespace Free3DPhotoMaker
{

    //интерфейс к dll преобразования изображений. 
    public class C3danaglyph
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void progressHandler (int progress);    
        public struct ProcessCallback
        {
            public event progressHandler progress_EventHandler;
        }

        [DllImport("3danaglyph.dll", ExactSpelling = true, CharSet = CharSet.None)]
        public static extern int Make3DAnaglyph(String src, String dst, ProcessCallback callback, int opt); 
        [DllImport("3danaglyph.dll", ExactSpelling = true, CharSet = CharSet.None)]
        public static extern int Make3DAnaglyphPair(String src1, String src2,  String dst, ProcessCallback callback,  int opt); 
    }

    public class StereoProcessor : IDisposable
    {
        private GdPictureImaging imageProcessor;

        private int leftImage = -1;
        private int rightImage = -1;
        
        private int resImage = -1;
        private bool useSingle = false;

        private int transparentMergedImage = -1;

        private int width = int.MaxValue;
        private int height = int.MaxValue;

        private StereoAlgorithm algorithm = StereoAlgorithm.Optimized;
        private ProcessStatus status = ProcessStatus.NoError;

        private ImageSettings imageSetts = new ImageSettings();
        private String statusString;

        private bool stop = false;

        public ImageSettings ImageSetts
        {
            get { return imageSetts; }
            set { imageSetts = value; }
        }

        
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public StereoAlgorithm Algorithm
        {
            get { return algorithm; }
            set { algorithm = value; }
        }

        public ProcessStatus Status
        {
            get { return status; }
        }
        public String GetStatusMsg()
        {
            switch (status)
            { 
                case ProcessStatus.NoError:
                    return Properties.Resources.ErrOk;

                case ProcessStatus.ErrStoppedByUser:
                    return Properties.Resources.ErrCancel;

                case ProcessStatus.ErrProcess:
                    return Properties.Resources.ErrProcess;

                case ProcessStatus.ErrMemory:
                    return Properties.Resources.ErrMemory;

                case ProcessStatus.ErrLoadSource:
                    return Properties.Resources.ErrLoadSource;

                case ProcessStatus.ErrInvalidParams:
                    return Properties.Resources.ErrInvalidParams;

                case ProcessStatus.ErrCreateResult:
                    return Properties.Resources.ErrCreateResult;


            }
            return "";
        }

        public int LeftImage
        {
            get { return leftImage; }
            set { leftImage = value; }
        }

        public int RightImage
        {
            get { return rightImage; }
            set { rightImage = value; }
        }

        public int ResImage
        {
            get { return resImage; }
            set { resImage = value; }
        }

        public int TransparentMergedImage
        {
            get { return transparentMergedImage; }
            set { transparentMergedImage = value; }
        }

        public bool UseSingle
        {
            get { return useSingle; }
            set { useSingle = value; }
        }

        public enum ImageSide
        {
            Left = 0,
            Right
        }

        public enum StereoAlgorithm
        {
            Optimized = 0,
            Colored,
            Dark,
            Grey,
            Yellow
        }

        public class ImageSettings
        {
            private Point leftOffset;
            int leftAngle;

            private Point rightOffset;
            int rightAngle;
            
            public int LeftAngle
            {
                get { return leftAngle; }
                set { leftAngle = value; }
            }

            public int RightAngle
            {
                get { return rightAngle; }
                set { rightAngle = value; }
            }

            public Point LeftOffset
            {
                get { return leftOffset; }
                set { leftOffset = value; }
            }

            public Point RightOffset
            {
                get { return rightOffset; }
                set { rightOffset = value; }
            }

            public ImageSettings() { }
            public ImageSettings(Point left, Point right, int lAngle, int rAngle)
            {
                leftOffset = left;
                rightOffset = right;
                leftAngle = lAngle;
                rightAngle = rAngle;
            }
        }

        public enum ProcessStatus
        { 
            NoError = 0,
            ErrStoppedByUser, 
            ErrLoadSource, 
            ErrCreateResult, 
            ErrInvalidParams, 
            ErrMemory, 
            ErrProcess
        }

        public StereoProcessor()
        {
            imageProcessor = new GdPictureImaging();
            imageProcessor.SetLicenseNumber("4984677207738517457431824");
        }

        public void Dispose()
        {
            Clear();            
        }

        public void Clear()
        {
            if (leftImage >= 0)
            {
                imageProcessor.ReleaseGdPictureImage(leftImage);
                leftImage = -1;
            }
            if (rightImage >= 0)
            {
                imageProcessor.ReleaseGdPictureImage(rightImage);
                rightImage = -1;
            }
            if (resImage >= 0)
            {
                imageProcessor.ReleaseGdPictureImage(resImage);
                resImage = -1;
            }
        }

        public void SetAvailableAlgorithms(PropsProvider storage)
        {
            List<ComboBoxItem<StereoAlgorithm>> algs = new List<ComboBoxItem<StereoAlgorithm>>();
            algs.Add(new ComboBoxItem<StereoAlgorithm>(StereoAlgorithm.Optimized, Properties.Resources.OptimizedAlg));
            algs.Add(new ComboBoxItem<StereoAlgorithm>(StereoAlgorithm.Colored, Properties.Resources.ColoredAlg));

            algs.Add(new ComboBoxItem<StereoAlgorithm>(StereoAlgorithm.Dark, Properties.Resources.DarkAlg));
            algs.Add(new ComboBoxItem<StereoAlgorithm>(StereoAlgorithm.Grey, Properties.Resources.GrayAlg));
            algs.Add(new ComboBoxItem<StereoAlgorithm>(StereoAlgorithm.Yellow, Properties.Resources.YellowAlg));
            
         
            storage.Set(Configuration.AvailableAlgorithms, algs);
        }



        public int OpenImage(string fileName, ImageSide side)
        {
            switch (side)
            {
                case ImageSide.Left:
                    if (leftImage >= 0)
                    {
                        imageProcessor.ReleaseGdPictureImage(leftImage);
                        width = height = int.MaxValue;
                    }

                    leftImage = imageProcessor.CreateGdPictureImageFromFile(fileName);
                    if (leftImage >= 0)
                    {
                        if (width > imageProcessor.GetWidth(leftImage))
                        {
                            width = imageProcessor.GetWidth(leftImage);
                        }

                        if (height > imageProcessor.GetHeight(leftImage))
                        {
                            height = imageProcessor.GetHeight(leftImage);
                        }
                    }
                    return leftImage;
                case ImageSide.Right:
                    if (rightImage >= 0)
                    {
                        imageProcessor.ReleaseGdPictureImage(rightImage);
                        width = height = int.MaxValue;
                    }

                    rightImage = imageProcessor.CreateGdPictureImageFromFile(fileName);

                    if (rightImage >= 0)
                    {
                        if (width > imageProcessor.GetWidth(rightImage))
                        {
                            width = imageProcessor.GetWidth(rightImage);
                        }

                        if (height > imageProcessor.GetHeight(rightImage))
                        {
                            height = imageProcessor.GetHeight(rightImage);
                        }
                    }
                    return rightImage;
            }

            return -1;
        }

        public void Swap()
        {
            int tmp = leftImage;
            leftImage = rightImage;
            rightImage = tmp;
        }

        public int GetWidth(ImageSide side)
        {
            int img = 0;
            switch (side)
            {
                case ImageSide.Left:
                    img = leftImage;
                    break;
                case ImageSide.Right:
                    img = rightImage;
                    break;
            }

            return imageProcessor.GetWidth(img);
        }

        public int GetHeight(ImageSide side)
        {
            int img = 0;
            switch (side)
            {
                case ImageSide.Left:
                    img = leftImage;
                    break;
                case ImageSide.Right:
                    img = rightImage;
                    break;
            }

            return imageProcessor.GetHeight(img);
        }

        private Color CreateOptimizedAnaglyphPixel(Color srcLeft, Color srcRight)
        {
            double r =
                0.4154 * srcLeft.R + 0.4710 * srcLeft.G + 0.1669 * srcLeft.B -
                0.0109 * srcRight.R - 0.0364 * srcRight.G - 0.0060 * srcRight.B;
            double g =
                -0.0458 * srcLeft.R - 0.0484 * srcLeft.G - 0.0257 * srcLeft.B +
                0.3756 * srcRight.R + 0.7333 * srcRight.G + 0.0111 * srcRight.B;
            double b =
                -0.0547 * srcLeft.R - 0.0615 * srcLeft.G + 0.0128 * srcLeft.B -
                0.0651 * srcRight.R - 0.1287 * srcRight.G + 1.2971 * srcRight.B;
            if (r < 0.0)
                r = 0.0;
            if (r > 255.0)
                r = 255.0;
            if (g < 0.0)
                g = 0.0;
            if (g > 255.0)
                g = 255.0;
            if (b < 0.0)
                b = 0.0;
            if (b > 255.0)
                b = 255.0;

            return Color.FromArgb((int)r, (int)g, (int)b);
        }

        private Color CreateRedCyanAnaglyphPixel(Color srcLeft, Color srcRight)
        {
            return Color.FromArgb(srcLeft.R, srcRight.G, srcRight.B);
        }

        private Color CreateDarkAnaglyphPixel(Color srcLeft, Color srcRight)
        {
            double r = 0.299 * srcLeft.R + 0.587 * srcLeft.G + 0.114 * srcLeft.B;
            double g = 0;
            double b = 0.299 * srcRight.R + 0.587 * srcRight.G + 0.114 * srcRight.B;
            if (r < 0.0)
                r = 0.0;
            if (r > 255.0)
                r = 255.0;
            if (b < 0.0)
                b = 0.0;
            if (b > 255.0)
                b = 255.0;

            return Color.FromArgb((int)r,(int)g,(int)b);
        }

        private Color CreateGrayAnaglyphPixel(Color srcLeft, Color srcRight)
        {
            double r = 0.299 * srcLeft.R + 0.587 * srcLeft.G + 0.114 * srcLeft.B;
            double g = 0.299 * srcRight.R + 0.587 * srcRight.G + 0.114 * srcRight.B;
            double b = g;
            if (r < 0.0)
                r = 0.0;
            if (r > 255.0)
                r = 255.0;
            if (g < 0.0)
                g = 0.0;
            if (g > 255.0)
                g = 255.0;
            if (b < 0.0)
                b = 0.0;
            if (b > 255.0)
                b = 255.0;
            return Color.FromArgb((int)r,(int)g,(int)b);
        }

        private Color CreateYellowBlueAnaglyphPixel(Color srcLeft, Color srcRight)
        {
            return Color.FromArgb(srcLeft.R, srcLeft.G, srcRight.B);
        }

        public delegate void MergeProgressChangedDelegate(int progress, int max);
        public event MergeProgressChangedDelegate MergeProgressChanged;

        public void OnProgressCallback(int prgs)
        {
            MergeProgressChanged(prgs, 100);
        }

        private void MergeThread()
        {
            if (leftImage == -1 || rightImage == -1) return;

            if (resImage >= 0)
            {
                imageProcessor.ReleaseGdPictureImage(resImage);
                resImage = -1;
            }

            if (resImage < 0)
                resImage = imageProcessor.CreateNewGdPictureImage(width, height, (short)imageProcessor.GetBitDepth(leftImage), Color.Black);
            int a = 4;
            switch (algorithm)
            {
                case StereoAlgorithm.Optimized:
                    a = 4;
                    break;
                case StereoAlgorithm.Colored:
                    a = 2;
                    break;
                case StereoAlgorithm.Dark:
                    a = 0;
                    break;
                case StereoAlgorithm.Grey:
                    a = 1;
                    break;
                case StereoAlgorithm.Yellow:
                    a = 3;
                    break;            
            }
            status = ProcessStatus.NoError;
            statusString = "";
            stop = false;
            MergeProgressChanged(0, 100);
            C3danaglyph.ProcessCallback callback = new C3danaglyph.ProcessCallback();
            callback.progress_EventHandler += this.OnProgressCallback;
            string tempPath = System.IO.Path.GetTempPath();
            string srcFileName1 = tempPath + "\\__source__file1___.jpg";
            string srcFileName2 = tempPath + "\\__source__file2___.jpg";
            string dstFileName =  tempPath + "\\__dst__file___.jpg";
            DVDVideoSoft.ImageUtils.ImageUtils.SaveTo(imageProcessor, leftImage, srcFileName1, TiffCompression.TiffCompressionJPEG);
            DVDVideoSoft.ImageUtils.ImageUtils.SaveTo(imageProcessor, rightImage, srcFileName2, TiffCompression.TiffCompressionJPEG);
            int r = C3danaglyph.Make3DAnaglyphPair(srcFileName1, srcFileName2, dstFileName, callback, a);
            resImage = imageProcessor.CreateGdPictureImageFromFile(dstFileName);
            File.Delete(srcFileName1);
            File.Delete(srcFileName2);
            File.Delete(dstFileName);

            // установка статуса 
            if (stop)
            {
                statusString = "Cansel by user";
                status = ProcessStatus.ErrStoppedByUser;
            }
            else
                status = DoStatus(r);

            if (MergeProgressChanged != null)
                MergeProgressChanged(100, 100);
        }

        private void MergeThreadSingle()
        {
            if (leftImage == -1) return;

            if (resImage >= 0)
            {
                imageProcessor.ReleaseGdPictureImage(resImage);
                resImage = -1;
            }

            if (resImage < 0)
                resImage = imageProcessor.CreateNewGdPictureImage(width, height, (short)imageProcessor.GetBitDepth(leftImage), Color.Black);

            status = ProcessStatus.NoError;
            statusString = "";
            stop = false;
            MergeProgressChanged(0, 100);
            C3danaglyph.ProcessCallback callback = new C3danaglyph.ProcessCallback();
            callback.progress_EventHandler += this.OnProgressCallback;
            int a = 4;
            switch (algorithm)
            {
                case StereoAlgorithm.Optimized:
                    a = 4;
                    break;
                case StereoAlgorithm.Colored:
                    a = 2;
                    break;
                case StereoAlgorithm.Dark:
                    a = 0;
                    break;
                case StereoAlgorithm.Grey:
                    a = 1;
                    break;
                case StereoAlgorithm.Yellow:
                    a = 3;
                    break;            
            }
            string tempPath = System.IO.Path.GetTempPath();
            string srcFileName = tempPath + "\\__source__file___.jpg";
            string dstFileName = tempPath + "\\__dst__file___.jpg";
            DVDVideoSoft.ImageUtils.ImageUtils.SaveTo(imageProcessor, leftImage, srcFileName, TiffCompression.TiffCompressionJPEG);
            int r = C3danaglyph.Make3DAnaglyph(srcFileName, dstFileName, callback, a);
            resImage = imageProcessor.CreateGdPictureImageFromFile(dstFileName);
            File.Delete(srcFileName);
            File.Delete(dstFileName);
            // установка статуса 
            if (stop)
            {
                status = ProcessStatus.ErrStoppedByUser;
                statusString = "Cansel by user";
            }
            else
                status = DoStatus(r);
            if (MergeProgressChanged != null)
                MergeProgressChanged(100, 100);  
        }

        public void Merge()
        {
            if (!useSingle)
            {
                System.Threading.Thread mergeT = new System.Threading.Thread(MergeThread);
                mergeT.Start();
            }
            else
            {
                //string srcFileName = "__source__file___.jpg";
                //string dstFileName = "__dst__file___.jpg";
                //DVDVideoSoft.ImageUtils.ImageUtils.SaveTo(imageProcessor, leftImage, srcFileName, TiffCompression.TiffCompressionJPEG);
                //int r = C3danaglyph.Make3DAnaglyph(srcFileName, dstFileName, 3);
                //resImage = imageProcessor.CreateGdPictureImageFromFile(dstFileName);
                //File.Delete(srcFileName);
                //File.Delete(dstFileName);
                //if (MergeProgressChanged != null)
                //    MergeProgressChanged(99, 100);                

                System.Threading.Thread mergeT = new System.Threading.Thread(MergeThreadSingle);
                mergeT.Start();
            }
        }

        public void Stop()
        {
            stop = true;
        }

        int leftThumb = -1;
        int rightThumb = -1;

        private void CreatePreviews(int twidth, int theight)
        {
            
            if (leftImage >= 0)
            {
                float aspect=(float)imageProcessor.GetWidth(leftImage) / (float)imageProcessor.GetHeight(leftImage);
                if (leftThumb >= 0)
                {
                    imageProcessor.ReleaseGdPictureImage(leftThumb);
                }
                if(leftThumb == -1 || imageProcessor.GetWidth(leftThumb) != twidth || imageProcessor.GetHeight(leftThumb) != theight)
                {
                    leftThumb = imageProcessor.CreateThumbnail(leftImage, twidth > theight ? (int)Math.Round(theight * aspect) : twidth, twidth < theight ? (int)Math.Round(twidth * aspect) : theight);
                }
            }

            if (rightImage >= 0)
            {
                float aspect = (float)imageProcessor.GetWidth(rightImage) / (float)imageProcessor.GetHeight(rightImage);
                if (rightThumb >= 0)
                {
                    imageProcessor.ReleaseGdPictureImage(rightThumb);
                }
                if (rightThumb == -1 || imageProcessor.GetWidth(rightThumb) != twidth || imageProcessor.GetHeight(rightThumb) != theight)
                {
                    rightThumb = imageProcessor.CreateThumbnail(rightImage, twidth > theight ? (int)Math.Round(theight * aspect) : twidth, twidth < theight ? (int)Math.Round(twidth * aspect) : theight);
                }
            }
        }

        public int MergeTransparentPreview(ImageSettings settings, int twidth, int theight)
        {
            if (leftImage == -1 && rightImage == -1) return -1;


            if (transparentMergedImage >= 0)
            {
                imageProcessor.ReleaseGdPictureImage(transparentMergedImage);
                transparentMergedImage = -1;
            }

            CreatePreviews(twidth, theight);
            if (leftImage >= 0)
            {
                imageProcessor.RotateAngle(leftThumb, settings.LeftAngle);
            }

            if (rightImage >= 0)
            {
                imageProcessor.RotateAngle(rightThumb, settings.RightAngle);
            }

            Rectangle leftR;
            if (leftImage >= 0)
                leftR = new Rectangle(imageSetts.LeftOffset, new Size(imageProcessor.GetWidth(leftThumb), imageProcessor.GetHeight(leftThumb)));
            else
                leftR = new Rectangle(0, 0, twidth, theight);

            Rectangle rightR;
            if (rightImage >= 0)
                rightR = new Rectangle(imageSetts.RightOffset, new Size(imageProcessor.GetWidth(rightThumb), imageProcessor.GetHeight(rightThumb)));
            else
                rightR = new Rectangle(0, 0, twidth, theight);

            Rectangle res = Rectangle.Intersect(leftR, rightR);

            if (transparentMergedImage < 0)
            {
                transparentMergedImage = imageProcessor.CreateNewGdPictureImage(res.Width, res.Height, (short)imageProcessor.GetBitDepth(leftImage), Color.Black);
            }

            if (leftImage >= 0)
            {
                imageProcessor.DrawGdPictureImageRect(leftThumb, transparentMergedImage, 0, 0, res.Width, res.Height, res.Left, res.Top, res.Width, res.Height, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);
            }
            if (rightImage >= 0)
            {
                int tmp = imageProcessor.CreateClonedGdPictureImage(transparentMergedImage);
                imageProcessor.DrawGdPictureImageRect(rightThumb, tmp, 0, 0, res.Width, res.Height, res.Left, res.Top, res.Width, res.Height, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);
                imageProcessor.DrawGdPictureImageTransparency(tmp, transparentMergedImage, 128, 0, 0, res.Width, res.Height, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);
                imageProcessor.ReleaseGdPictureImage(tmp);
            }

            imageSetts = settings;

            return transparentMergedImage;
        }
       

        public void SaveToFile(string fileName)
        {
            try
            {
                DVDVideoSoft.ImageUtils.ImageUtils.SaveTo(imageProcessor, resImage, fileName,TiffCompression.TiffCompressionJPEG);
                /*imageProcessor.ReleaseGdPictureImage(resImage);
                resImage = -1;*/
            }
            catch
            {
            }
        }

        public bool Ready()
        {

            return (leftImage >= 0 && rightImage >= 0) || (useSingle && leftImage >=0);
        }

        private ProcessStatus DoStatus(int ErrCode)
        {
            if (ErrCode == 0)
                return ProcessStatus.NoError;
            else if (ErrCode == -8)
            {
                statusString = "No memory";
                return ProcessStatus.ErrMemory;
            }
            else if (ErrCode == -4)
            {
                statusString = "Invalid params";
                return ProcessStatus.ErrInvalidParams;
            }
            else if (ErrCode == -1)
            {
                statusString = "Error load source";
                return ProcessStatus.ErrLoadSource;
            }
            else if (ErrCode == -2)
            {
                statusString = "Error create result.";
                return ProcessStatus.ErrCreateResult;
            }
            else
                return ProcessStatus.ErrProcess;
        }
    }

}
