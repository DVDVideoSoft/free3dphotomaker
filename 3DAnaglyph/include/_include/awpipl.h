/*
//
//  RCS:
//      $Source: /home/cvs/awp/imageproc/awpipl-v2/src/awpipl.h,v $
//      $Revision: 1.56 $
//      $Date:
//
//    Purpose: AWPIPL header file
//    Contents: awpipl main header file 
//    Authors : Alexander Telnykh
*/

	/** @defgroup datastructures Graphic data structures 
	*   @{
	*	@}
    */

    /** @defgroup commongroup Common functions
    *   @{
	*	@}
    */

    /** @defgroup filesgroup Input/Output functions
    *   @{
	*	@}
    */

    /** @defgroup geometricgroup Geometry functions
    *   @{
	*	@}
    */

	/** @defgroup editgroup Copy-Paste functions
	*	@{
	*	@}
    */


#ifndef _AWPIPL_H_
#define _AWPIPL_H_


#ifdef __cplusplus
extern "C" {
#endif
#include "awperror.h"

#ifdef WIN32
#include <windows.h>
#endif

#ifdef HAVE_FFTW3
#include <fftw3.h>
#endif

#define AWP_MAGIC2     0x000A0D5D /*magic = AWP*/

#define AWP_VERSION   2

#ifndef NULL
	#define NULL 0
#endif

#ifndef TRUE
	#define TRUE     -1
#endif

#ifndef FALSE
	#define FALSE     0
#endif

#define AWP_MAX(a,b) (((a) > (b)) ? (a) : (b))
#define AWP_MIN(a,b) (((a) < (b)) ? (a) : (b))

#ifndef AWP_PI
	#define AWP_PI 3.14159265358979323846
#endif

/*data types*/
#define AWP_BYTE				0x01
#define AWP_SHORT				0x02
#define AWP_FLOAT				0x03
#define	AWP_DOUBLE				0x04


/* convert options */
#define AWP_CONVERT_BASE          	0
#define AWP_CONVERT_TO_BYTE         	(AWP_CONVERT_BASE+0)
#define AWP_CONVERT_TO_BYTE_WITH_NORM   (AWP_CONVERT_BASE+1)
#define AWP_CONVERT_TO_SHORT            (AWP_CONVERT_BASE+2)
#define AWP_CONVERT_TO_FLOAT            (AWP_CONVERT_BASE+3)
#define AWP_CONVERT_TO_DOUBLE           (AWP_CONVERT_BASE+4)
#define AWP_CONVERT_3TO1_BYTE			(AWP_CONVERT_BASE+5)
/*geometric transformations*/
#define AWP_FLIP_VERT  0x1
#define AWP_FLIP_HRZT  0x2 
#define AWP_FLIP_LEFT  0x3
#define AWP_FLIP_RGHT  0x4

/** @defgroup arifgroup Arithmetic and logical operations
*   @{
*/
/*calculate images*/
#define AWP_CALC_ANDIMAGES  0x00000001
#define AWP_CALC_ORIMAGES   0x00000002
#define AWP_CALC_XORIMAGES  0x00000003
#define AWP_CALC_ADDIMAGES  0x00000004
#define AWP_CALC_SUBIMAGES  0x00000005
#define AWP_CALC_MLTIMAGES  0x00000006
#define AWP_CALC_DIVIMAGES  0x00000007
#define AWP_CALC_MINIMAGES  0x00000008
#define AWP_CALC_MAXIMAGES  0x00000009
#define AWP_CALC_AVGIMAGES  0x0000000A
/*flags of destinations for calculate images functions */
#define AWP_CALC_INPLACE    0x00000001
#define AWP_CALC_OUTPLACE   0x00000002

/** @} */ /* end of arifgroup  */

#define AWP_BINARY	            0x00000000
#define AWP_INV_BINARY          0x00000001

#ifdef HAVE_FFTW3
        #define AWP_FFT_FORWARD FFTW_FORWARD
        #define AWP_FFT_INVERSE FFTW_BACKWARD
#endif


#define AWP_EDGE_DIR_HORZ			0
#define AWP_EDGE_DIR_VERT			1
#define AWP_EDGE_DIR_BOTH			2

#define AWP_BLUR					0
#define AWP_BLUR_MORE				1
#define AWP_SHARPEN					2
#define AWP_SHARPEN_MORE			3
#define AWP_SHARPEN_EDGES			4
#define AWP_FIND_EDGES				5
#define AWP_FIND_EDGES1				6
#define AWP_EMBOSS_NORTH			7
#define AWP_EMBOSS_NORTH_OST		8
#define AWP_EMBOSS_OST				9
#define AWP_EMBOSS_SOUTH_OST		10
#define AWP_EMBOSS_SOUTH			11
#define AWP_EMBOSS_SOUTH_WEST		12
#define AWP_EMBOSS_WEST				13
#define AWP_EMBOSS_NORTH_WEST		14

#define PREWITT_V				    15
#define PREWITT_H					16
#define SOBEL_V						17
#define SOBEL_H						18


#define AWP_FILTER_METHOD_ORDINARY      0
#define AWP_FILTER_METHOD_FFT			1



/**/
typedef int              AWP_BOOL;
typedef unsigned char    BYTE;
typedef unsigned short   WORD;
typedef short            SHORT;
typedef long             LONG;
typedef unsigned long    DWORD;
typedef float			 FLOAT;
typedef double           DOUBLE;


	/** \addtogroup datastructures
	*   @{
	*/

    /**
    \struct tagawpImage
    \brief Awp image struct
    */
    typedef struct tagawpImage
    {
        /** image idetificator*/
        DWORD		nMagic;
        /** number of channels*/
        BYTE		bChannels;
        /** width image */
        WORD		sSizeX;
        /** height image */
        WORD		sSizeY;
        /** type of pixels */
        DWORD		dwType;
        /** pointer to pixels */
        void*		pPixels;
    } awpImage;

	/**
	\struct tagawpColor
	\brief RGB color structure
	*/
	typedef struct tagawpColor
	{
   		/** red channel */
		BYTE bRed;
	   	/** green channel */
		BYTE bGreen;
	   	/** blue channel */
   		BYTE bBlue;
	} awpColor;

	/**
	\struct ColorHSTDef
	\brief Histogramm from color image
	*/
	typedef struct ColorHSTDef
	{
		/** red channel */
		DOUBLE RedChannel[256];
		/** green channel */
		DOUBLE GreenChannel[256];
		/** blue channel */
		DOUBLE BlueChannel[256];
	}ColorHST;

	/**
	\struct awpStatDataDef Statictics result
	\brief Contain result statistics processing pictures
	*/
	typedef struct awpStatDataDef
	{
		/** red channel */
		DOUBLE dRed;
		/** green channel */
		DOUBLE dGreen;
		/** blue channel */
		DOUBLE dBlue;
		/** bright channel */
		DOUBLE dBright;
	}awpStat;
    
	/**
	\struct awpHistogrammDef Historgramm
	\brief Contain histogramm image
	*/
	typedef struct awpHistogrammDef
	{
		DOUBLE    Intensity[256];
                int       NumChannels;
                ColorHST  ColorData;
	} awpHistogramm;


    /**
    \struct awpPoint awpipl.h
    \brief Point abstraction
    */
    typedef struct
    {
        /** X coordinate */
        SHORT X;
        /** Y coordinate */
        SHORT Y;

    }awpPoint;

    /**
    \struct awpRect awpipl.h
    \brief Rectangle abstraction
    */
    typedef struct
    {
        /** left  coordinare rectangle */
        SHORT left;
        /** top  coordinare rectangle */
        SHORT top;
        /** right coordinare rectangle */
        SHORT right;
        /** bottom coordinare rectangle */
        SHORT bottom;
    }awpRect;

    /**
    \struct awpStroke awpipl.h
    \brief Stroke structure
    */
    typedef struct
    {
        /** begin stroke */
        int xl;
        /** end stroke */
        int xr;
        /** Y coordinate stroke */
        int y;
    }awpStroke;

    /**
    \struct awpStrokeObj awpipl.h
    \brief Stroke object
    */
    typedef struct
    {
        /** count strokes */
        DWORD           Num;
        /** array strokes */
        awpStroke*      strokes;
    }awpStrokeObj;

    /**
    \struct awpContour awpipl.h
    \brief Contain array points of countors
    */
    typedef struct
    {
        /** Count point in countor */
        DWORD	NumPoints;
        /** Direction: TRUE - external,FALSE - internal */
        AWP_BOOL	Direction;
        /** Array points */
        awpPoint* Points;
    } awpContour;


    /**
    \struct awpQuadrangle awpipl.h
    \brief Describes quadrangle
    */
    typedef struct
    {
		/**size of this structure*/
	    int		size;
		/**left_top of the quadrangle*/
        awpPoint        left_top;
		/**right_top of the quadrangle*/
        awpPoint        right_top;
		/**right_bottom of the quadrangle*/
        awpPoint        right_bottom;
		/**left_bottom of the quadrangle*/
        awpPoint        left_bottom;
    } awpQuadrangle;


	/** @} */ /*  end of datastructures group */


#define _AWP_BPIX_(v) \
           (BYTE*)v->pPixels;

#define _AWP_SPIX_(v) \
            (SHORT*)v->pPixels;



    /** \addtogroup commongroup
    *   @{
    */

    /**
    \brief get version awp image library
    \param pVersion version library
    \result AWP_OK if success else AWP_ERROR
    */
    AWPRESULT awpGetVersion(int* pVersion);

    /**
    \brief allocs memory for image \a ppImage type \a dwType size \a (width,height) with \a channels
    \param ppImage pointer on create image
    \param width width image
    \param height height image
    \param channels channels in image
    \param dwType type create image
    \result AWP_OK if success else AWP_ERROR
    */
    AWPRESULT awpCreateImage(awpImage** ppImage,WORD width,WORD height,BYTE channels, DWORD dwType);

    /**
    \brief Free memory for awpImage
    \param ppImage pointer on image
    \result AWP_OK if success else AWP_ERROR
    */
    AWPRESULT awpReleaseImage(awpImage** ppImage);

    /**
    \brief Check image structure
    \param pImage pointer on image
    \result AWP_OK if success else AWP_ERROR
    */
    AWPRESULT awpCheckImage(const awpImage* pImage);

    /**
    \brief Check rectangle structure
    \param pRect pointer on image
    \result AWP_OK if success else AWP_ERROR
    */
    AWPRESULT awpCheckRect(const awpRect* pRect);

	/**
	*	\brief Calculate size of image pixels array
	*	\param pImage pointer to the source image
	*	\param pSize pointer to returning image size value
	*	\return AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpGetImageSize(const awpImage* pImage, int* pSize);

	/**
	*	\brief Calculate size of image header
	*	\param pImage - pointer to the source image
	*	\param iSize pointer to returning image size value
	*	\return AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpGetImageHeaderSize(const awpImage* pImage,int *iSize);

	/**
	*	\brief Get image pixel type
	*	\param pImage pointer to the source image
	*	\param pType pointer to returning type value
	*	\return AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpGetImagePixelType(const awpImage* pImage, int* pType);

    /**
    \brief Check whether rect lies in image or not
    \param pImage pointer on image
    \param pRect pointer on image
    \result AWP_OK if success else AWP_ERROR
    */
    AWPRESULT awpRectInImage(const awpImage* pImage,const awpRect* pRect);

    /** @} */ /*  end of commongroup */


    /** \addtogroup filesgroup
    *   @{
    */
    /**
	*	\brief Load image from file
	*	\param lpFileName image file path
	*	\param ppImage pointer to awpImage
	*	\return AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpLoadImage(const char* lpFileName, awpImage** ppImage);

    /**
	*	\brief Save image to file
	*	\param lpFileName image file path
	*	\param ppImage pointer to awpImage
	*	\return AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpSaveImage(const char* lpFileName, awpImage* pImage);

    /**
    *	\brief Pack awpImage in memory buffer
    *	\param pImage image
    *	\param ppData memory buffer
    *	\param length lenght memory buffer in bytes
    *	\return AWP_OK if success or else AWP_ERROR
    */
    AWPRESULT awpImagePack( const awpImage* pImage, BYTE** ppData, int* length );

    /**
    *	\brief Unpack memory buffer to awpImage
    *	\param pData memory buffer
    *	\param length lenght memory buffer in bytes
    *	\param ppImage image
    *	\return AWP_OK if success or else AWP_ERROR
    */

    AWPRESULT awpImageUnpack( const BYTE* pData, int length, awpImage** ppImage );

    /** @} */ /* end of filesgroup    */


	/** \addtogroup editgroup
	*	@{
	*/

	/**
	*	\brief Make a copy of awpImage
	*	\param pSrcImage pointer to source image
	*	\param ppDstImage pointer to pointer to destination image
	*	\return AWP_OK if success else AWP_ERROR
	*	\remarks Destination image is creating by function. So *ppDstImage should be NULL
	*/
    AWPRESULT awpCopyImage(const awpImage* pSrcImage, awpImage** ppDstImage);
	/**
	*	\brief Make a copy of awpImage
	*	\param pSrcImage pointer to source image
	*	\param pDstImage pointer to to destination image with same characteristics
	*	\return AWP_OK if success else AWP_ERROR
	*	\remarks Destination image is creating by function. So *ppDstImage should be NULL
	*/
    AWPRESULT awpFastCopyImage(const awpImage* pSrcImage, awpImage* pDstImage);

	/**
	*	\brief Copy rectangle area to a new image
	*	\param pSrcImage pointer to the source image
	*	\param ppDstImage pointer to pointer to resulting image
	*	\param pRect pointer to the rectangle of area to copy
	*	\return AWP_OK if success else AWP_ERROR
	*	\remarks Destination image is creating by function. So *ppDstImage should be NULL
	*/
    AWPRESULT awpCopyRect (const awpImage* pSrcImage, awpImage** ppDstImage, const awpRect* pRect);

	/**
	*	\brief Paste one image to another
	*	\param pSrcImage source image
	*	\param pDstImage destination image to paste to
	*	\param Pos point of the left top corner of the source image
	*	\return AWP_OK if success else AWP_ERROR
	*	\remarks Destination image is creating by function. So *ppDstImage should be NULL
	*/
    AWPRESULT awpPasteRect(const awpImage* pSrcImage, awpImage* pDstImage,   const awpPoint Pos);

	/**
	*	\brief Copy stroke object to a new image
	*	\param pSrcImage pointer to source image
	*	\param ppDstImage pointer to pointer to result image
	*	\param pStr pointer to a stroke object of the source image
	*	\return AWP_OK if success else AWP_ERROR
	*	\remarks Destination image is creating by function. So *ppDstImage should be NULL
	*/
    AWPRESULT awpCopyImageStr(const awpImage* pSrcImage, awpImage** ppDstImage, const awpStrokeObj* pStr);

    /**
    *	\brief Get image channel from image
    *	\param pImage pointer to the source image
    *	\param ppImageDst pointer to the resulting image
    *	\param bOption number of the channel
    *	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpGetChannel(const awpImage *pImage, awpImage **ppChannel, const BYTE bOptions);

	AWPRESULT awpGetChannel2(awpImage* pImage, awpImage* pChannel, BYTE bChannel);

    /**
    *	\brief Replace image channel
	*
	*	Function replaces image channel indicated by bOption to channel from pChannel. pChannel
	*	image should have only one channel.
	*
    *	\param pImage pointer to the image
    *	\param pChannel pointer to the image channel information
    *	\param bOption number of the channel
    *	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpPutChannel(awpImage* Image, const awpImage* pChannel, const BYTE bOptions);

	/** @} */ /* end of editgroup    */


    /** @addtogroup filesgroup
    *   @{
    */

#ifdef	HAVE_JPEG
	/**
	*	\brief Convert awpImage to JPEG format
	*	\param pImage source image
	*	\param ppJpegData output array with JPEG data
	*	\param length length of the output array
	*	\param quality desired JPEG quality
	*	\return AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpImageToJpeg( const awpImage* pImage, BYTE** ppJpegData, DWORD* length, BYTE quality );

	/**
	*	\brief Convert JPEG image data to awpImage format
	*	\param pJpegData array containing JPEG data
	*	\param length length of JPEG data array
	*	\param ppImage destination awpImage pointer
	*	\return AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpJpegToImage( const BYTE* pJpegData, DWORD length, awpImage** ppImage );
#endif


#ifdef WIN32
	/**
	*	\brief Convert awpImage to Windows DIB format
	*	\param pImage source image
	*	\param pBMI pointer to BITMAPINFO structure
	*	\param pBMData pointer to DIB pixels
	*	\return AWP_OK if success or else AWP_ERROR
	*/
	AWPRESULT awpImageToDIB( const awpImage* pImage, BITMAPINFO* pBMI, void** pBMData, AWP_BOOL replace );
	/**
	*	\brief Convert Windows DIB to awpImage format
	*	\param pBMI pointer to BITMAPINFO structure
	*	\param pBMData pointer to DIB pixels
	*	\param pImage result image
	*	\return AWP_OK if success or else AWP_ERROR
	*/
	AWPRESULT awpDIBToImage( const BITMAPINFO* pBMI, const void* pBMData, awpImage** pImage );
#endif

#ifdef OPENCV
#endif 
    /** @} */ /* end of filegroup    */


    /** \addtogroup geometricgroup
    *   @{
    */

    /** Basic AWPIPL functions for geometric transformation.
     *  It defines functions for flipping, resizing, rescaling and 
     *  rotation of AWPImage.
    */

    /** \brief 180(90)-degree turn function
	 *
     *  @param  awpImage* pImageSrc - AWP Image.
     *  @param  long      lOption   - turn option, should be on of the following:\n\n
     *  AWP_FLIP_VERT   a 180-degree rotation around horizontal axis \n
     *  AWP_FLIP_HRZT   a 180-degree rotation around vertical axis \n
     *  AWP_FLIP_LEFT   90-degree turn left \n
     *  AWP_FLIP_RGHT   90-degree turn right
     *  @return AWP_OK if success or else AWP_ERROR.*/
    AWPRESULT awpFlip(awpImage** ppImageSrc, const long lOptions);

    /** \brief Resize image function
     *  @param awpImage* pImageSrc - AWP Image.
     *  @param WORD wNewWidth  - new width of image.
     *  @param WORD wNewHeight - new height of image.
     *  @return AWP_OK if success or else AWP_ERROR.*/
    AWPRESULT awpResize(awpImage* pImageSrc, const WORD wNewWidth, const WORD sNewHeight);
    AWPRESULT awpResizeBilinear(awpImage* pImageSrc, const WORD wNewWidth, const WORD wNewHeight);
    AWPRESULT awpResizePiecewiseCubic(awpImage* pImageSrc, const WORD wNewWidth, const WORD wNewHeight);
    AWPRESULT awpResizeBicubic2x2(awpImage* pImageSrc, const WORD wNewWidth, const WORD wNewHeight);
    AWPRESULT awpResizeBicubic4x4(awpImage* pImageSrc, const WORD wNewWidth, const WORD wNewHeight);



	/**
	*	\brief Resize image function
	*	\param pImageSrc pointer to the source image
	*	\param ppDst pointer to pointer to the resulting resized image
	*	\param wNewWidth new width value
	*	\param wNewHeight new height value
	*	\return AWP_OK if success or else AWP_ERROR
	*	\remarks Creates new image, so *ppDst should be NULL
	*/
    AWPRESULT awpResizeNip(awpImage* pImageSrc, awpImage** ppDst, const WORD wNewWidth, const WORD wNewHeight);

    /** \brief Rescale image function
	 *
     *  New image heigh = old image heigh * wHeightScaleFactor;\n
     *  New image width = old image width * wWidthScaleFactor ;
     *  @param awpImage* pImageSrc - AWP Image.
     *  @param WORD wWidthScaleFactor  - width scale factor. 
     *  @param WORD wHeightScaleFactor - heigh scale factor.
     *  @return AWP_OK if success or else AWP_ERROR.*/
    AWPRESULT awpRescale(awpImage* pImageSrc, const FLOAT fWidthScaleFactor, const FLOAT fHeightScaleFactor);

    /** \brief Rotate image function
     *  @param awpImage* pImageSrc - AWP Image.
     *  @param SHORT sAngle - angle for rotation.
     *  @return AWP_OK if success or else AWP_ERROR.*/
    AWPRESULT awpRotate(awpImage *Image, const SHORT sAngle );

    /** \brief Rotate image function
    *  @param awpImage* pImageSrc - AWP Image.
    *  @param SHORT sAngle - angle for rotation.
    *  @return AWP_OK if success or else AWP_ERROR.*/
    AWPRESULT awpRotate2(awpImage *Image, const DOUBLE dCos, const DOUBLE dSin );


    /** \brief Rotate image around given point function
     *  @param awpImage* pImageSrc - AWP Image.
     *  @param SHORT sAngle - angle for rotation.
     *  @param SHORT xCenter, SHORT yCenter - point for rotation.
     *  @return AWP_OK if success or else AWP_ERROR.*/
    //TODO: fix bug: then center near one of the side the image is scaling;
    AWPRESULT awpRotateCenter( awpImage *pImageSrc, const SHORT sAngle, SHORT xCenter, SHORT yCenter );

    /** \brief Rotate image around given point function
    *  @param awpImage* pImageSrc - AWP Image.
    *  @param SHORT sAngle - angle for rotation.
    *  @param SHORT xCenter, SHORT yCenter - point for rotation.
    *  @return AWP_OK if success or else AWP_ERROR.*/
    AWPRESULT awpRotateCenter2( awpImage *pImageSrc, const DOUBLE dcos, const DOUBLE dsin, SHORT xCenter, SHORT yCenter );

    /** @} */ /* end of geometricgroup */


    /** @addtogroup arifgroup
    *   @{
    */

    /**
     \brief Arithmetic and logical operations function
	 *
	 *  It sets various image manipulations like adding, substraction, multiplication, 
	 *  division, logical and, logical or, logical xor, finding min, max, and average
	 *  image. Image operation sets depending on function parameter lOption, which
	 *  should be set to one of the following options.\n\n
     *
     *  AWP_CALC_ADDIMAGES - adding images function\n
     *  AWP_CALC_SUBIMAGES - substraction images function\n
     *  AWP_CALC_MLTIMAGES - multiplication images function\n
     *  AWP_CALC_DIVIMAGES - division images function\n
     *  AWP_CALC_ANDIMAGES - logical AND images function\n
     *  AWP_CALC_ORIMAGES  - logical OR images function\n
     *  AWP_CALC_XORIMAGES - logical XOR images function\n
     *  AWP_CALC_MINIMAGES - finding minimal image function\n
     *  AWP_CALC_MAXIMAGES - finding maximum image function\n
     *  AWP_CALC_AVGIMAGES - calculate averaged image function\n\n
     *
     *  All functions with the exception of logical AND, logical OR and logical XOR have AWP_DOUBLE as
     *  output awpImage picture type, and any of awpImage type as input one. For logical AND, logical OR and
     *  logical XOR images functions you should use only AWP_BYTE awpImage type as input and output parameters.\n\n
     *  
     *  You can get output image as new image or you can replace second initial image. It dependes on function
     *  parameter lWhere, which should be set to one of the following options.\n\n
     *
     *  AWP_CALC_INPLACE  - replace second initial image\n
     *  AWP_CALC_OUTPLACE - get new image
     *
     *  \param awpImage*  pImageSrc1 - first input AWP Image.
     *  \param awpImage*  pImageSrc1 - first input AWP Image.
     *  \param awpImage** ppImageDst - output AWP Image, if lWhere = AWP_CALC_INPLACE, ppImageDst should be set NULL.
     *  \param const long lOption    - image operation parameter.
     *  \param const long lWhere     - image parameter, replace old image or create new one?  
     *  \return AWP_OK if success or else AWP_ERROR
	 */
    AWPRESULT awpCalcImage(const awpImage* pImageSrc1,  awpImage* pImageSrc2, awpImage** ppImageDst, const long lOptions, const long lWhere);

    /* statistics function */
	/**
	*	\brief Calculate minimum and maximum light levels
	*	\param const awpImage* pImage pointer to the source image
	*	\param DOUBLE** min pointer to pointer to found minimal value
	*	\param DOUBLE** max pointer to pointer to found maximal value
	*	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpMinMax(const awpImage* pImage, DOUBLE** min, DOUBLE** max);

#ifdef HAVE_FFTW3
    /* fft function */
    /* Dangerous function */
    /*AWPRESULT awpConvFFTCircular(awpImage* pSrcImage,awpImage* pMask,awpImage** pDstImage);*/

	/**
	*	\breif Calculate mask convolution using FFTW3 library
	*	\param awpImage* pSrcImage pointer to the source image
	*	\param awpImage* pMask pointer to the mask
	*	\param awpImage** pDstImage pointer to pointer to the convolution result
	*	\return AWP_OK if success or else AWP_ERROR
	*	\remarks Destination image is creating by function. So *ppDstImage should be NULL
	*/
    AWPRESULT awpConvFFTLinear(awpImage* pSrcImage,awpImage* pMask,awpImage** pDstImage);
    AWPRESULT awpFFT(awpImage** pImage,awpImage** pImageRe,awpImage** pImageIm,int iFftDir);
#endif

	/**
	*	\brief Perform mask convolution on image
	*	\param pSrcImage pointer source to the source image
	*	\param pMask pointer to the mask
	*	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpMaskConvolution(awpImage* pSrcImage,awpImage* pMask);

	/**
	*	\brief Perform mask convolution on image
	*	\param pSrcImage pointer to the source image
	*	\param pMask pointer to the mask
	*	\param pDstImage pointer to pointer to the resulting image
	*	\return AWP_OK if success or else AWP_ERROR
	*	\remarks Resulting image is creating by the function. So *ppDstImage should be NULL
	*/
    AWPRESULT awpMaskConvolutionDbl(awpImage* pSrcImage,awpImage* pMask, awpImage** pDstImage);

    AWPRESULT awpMaskConvolution2(awpImage* pSrcImage, awpImage* pDst, awpImage* pMask);
	/**
	*
	*/
	AWPRESULT awpGetSumPix(awpImage* pSrc, awpImage* pDst);
	/**
	*
	*/
	AWPRESULT awpGetMinMax(awpImage* pSrc, awpImage* pDstxt);
	AWPRESULT awpGaussianBlur(awpImage* pSrc, awpImage* pDst, float sigma);
	/**
	*	\brief Light level normalization
	*
	*  Finds maximal and minimal illumination levels for supplied image, sets them to 0 and 255
	*  respectively and recalculates all other levels according to the new values.
	*
	*	\param awpImage* pImageSrc pointer to the image
	*	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpAutoLevels(awpImage* pImage);

    /** @} */ /* end of arifgroup  */


    /** @defgroup strokegroup Strokes function
    *   @{
    */

    /**
    *	\brief Find object in image
    *	\param src source image
    *	\param Num number found objects
    *   \param strokes pointer on massiv objects
    *	\param thr threshold binarization
    *	\result AWP_OK if success else AWP_ERROR
    */
    AWPRESULT awpGetStrokes(const awpImage* src, int* Num, awpStrokeObj** strokes, BYTE thr);

    /**
    *	\brief Free memory for stroke object 
    *	\param Num number objects
    *	\param strokes pointer on massiv objects
    *	\result AWP_OK if success else AWP_ERROR
    */
    AWPRESULT awpFreeStrokes(int Num, awpStrokeObj** s);

    AWPRESULT awpCopyStrokeObj1(awpStrokeObj* pSrc, awpStrokeObj* pDst);


    /**
    *	\brief Calculate rectangle around objects
    *	\param s stroke objects
    *	\param r rectangle
    *	\result AWP_OK if success else AWP_ERROR
    */
    AWPRESULT awpCalcObjRect(const awpStrokeObj* s, awpRect* r);

	/**
	*	\brief Calculate mean object intensity
	*	\param pImg pointer to the source image
	*	\param pObj pointer to the stroke object
	*	\param I pointer to the resulting intensity value
	*	\return AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpGetObjIntensity(const awpImage* pImg, const awpStrokeObj* pObj, DOUBLE* I);

    /**
    *	\brief Calculate object square
    *	\param s pointer to the strokes object
    *	\param Sq square of the object
    *	\result AWP_OK if success else AWP_ERROR
    */
    AWPRESULT awpStrObjSquare(const awpStrokeObj* s, int* Sq);

	/**
	*	\brief Build object contour
	*	\param s pointer to the strokes object
	*	\param c countour of the object
    *	\result AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpGetObjCountour(const awpStrokeObj* s, awpContour* c);

	/**
	*	\brief Calculate perimeter of the contour
	*	\param c pointer to the contour
	*	\param Perim pointer to the perimeter value
    *	\result AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpGetContPerim(const awpContour* c, DOUBLE* Perim);

	/**
	*	\brief Calculate object centroid
	*	\param pImg pointer to the source image
	*	\param pObj pointer to the strokes object
	*	\param p pointer to the centroid point
	*	\return AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpGetObjCentroid(const awpImage* pImg, const awpStrokeObj* pObj, awpPoint* p);

	/**
	*	\brief Calculate p-th and q-th moments for the strokes object
	*	\param pImg pointer to the source image
	*	\param pObj pointer to the strokes object
	*	\param p moment order value
	*	\param q moment order value
	*	\param m moment value
	*	\return AWP_OK if success else AWP_ERROR
	*	\remark Works only with AWP_BYTE type and one channel image
	*/
    AWPRESULT awpGetObjMoment(const awpImage* pImg, const awpStrokeObj* pObj, int p, int q, DOUBLE* m);

	/**
	*	\brief Calculate orientation of the object
	*	\param pImg pointer to the source image
	*	\param pObj pointer to the strokes object
	*	\param teta orientation angle
	*	\param mi minor axis lenght
	*	\param ma major axis lenght
	*	\return AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpGetObjOrientation(const awpImage* pImg, const awpStrokeObj* pObj, DOUBLE* teta, DOUBLE* mi, DOUBLE* ma);


    /**
    *	\brief Draw stroke objects on image
    *	\param ppimg pointer on image
    *	\param w width image
    *	\param h height image
    *	\param num count strokes objects
    *	\param pobjs pointer on objects
    *	\result AWP_OK if success else AWP_ERROR
    */
    AWPRESULT awpDrawStrokes(awpImage** ppimg, WORD w, WORD h, int num, awpStrokeObj* pobjs);

	/**
	*	\brief Draw single stroke object
	*	\param pImage pointer to the destination image
	*	\param pObj pointer to the strokes object
	*	\return AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT DrawStrokeObj(awpImage* pImage, const awpStrokeObj* pObj, BYTE v);

    /** @} */ /* end of strokegroup  */


	/** @defgroup convertgroup Convertion function
	*	@{
	*/

	/**
	*	\brief Image convertion
	*	\param pImage pointer to the source image
	*	\param lOptions convertion option. Can take one of the following values:\n
	*		AWP_CONVERT_TO_DOUBLE\n
	*		AWP_CONVERT_3TO1_BYTE\n
	*		AWP_CONVERT_TO_BYTE\n
	*		AWP_CONVERT_TO_BYTE_WITH_NORM
	*	\return AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpConvert(awpImage* pImage,  long lOptions);
    AWPRESULT awpConvertV2(awpImage* Image, long lOptions);

    /**
    *	\brief Bilevels image segmentation with threshold separation
	*	\param pImage pointer to the source image
	*	\param ppBinImage pointer to the pointer to resulting binary image
	*	\param sThreshold binarization threshold value. Should be between 0 and 255.
	*	\param lOptions binarization option. Can take following values:\n
	*		AWP_BINARY\n
    *		AWP_INV_BINARY
	*	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpMakeBinary(const awpImage* pImage, awpImage** ppBinImage, const DOUBLE sThreshold, const long lOptions);

    /*color space convertions */
	/**
	*	\brief Convert RGB image to HSL format
	*	\param pImage pointer to the source image
	*	\param ppHSL pointer to pointer to the resulting HSL image
	*	\remarks Resulting image is creating by the function, so *ppHSL should be set to NULL
	*	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpRGBtoHSLImage(const awpImage* pImage,awpImage** ppHSL);

	/**
	*	\brief Convert HSL image to RGB format
	*	\param pImage pointer to the source image
	*	\param ppRGB pointer to pointer to the resulting RGB image
	*	\remarks Resulting image is creating by the function, so *ppRGB should be set to NULL
	*	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpHSLtoRGBImage(const awpImage* pImage,awpImage** ppRGB);

	/* TODO: add description */
    AWPRESULT awpBackProjection(awpImage* Image, awpImage** ppProb, awpHistogramm* Histogramm);
    AWPRESULT awpBackProjection2D(awpImage* Image, awpImage** ppProb, awpImage* pPreset, BYTE min_v, BYTE max_v);

	/**
	*	\brief Convert RGB image to HSV format
	*	\param pImage pointer to the source image
	*	\param ppHSV pointer to pointer to the resulting HSV image
	*	\remarks Resulting image is creating by the function, so *ppHSV should be set to NULL
	*	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpRGBtoHSVImage(const awpImage* pImage, awpImage** ppHSV);
    void _awpRGBtoHSV(BYTE rr,BYTE gg,BYTE bb,FLOAT *h, FLOAT *s, FLOAT *l);


	/**
	*	\brief Convert HSV image to RGB format
	*	\param pImage pointer to the source image
	*	\param ppRGB pointer to pointer to the resulting RGB image
	*	\remarks Resulting image is creating by the function, so *ppRGB should be set to NULL
	*	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpHSVtoRGBImage(const awpImage* pImage, awpImage** ppRGB);

	/** @} */ /* end of convertgroup */


	/** @defgroup histogroup Histogram function
	*   @{
	*/

    /*Histogram functions */
	/**
	*	\brief Calculate color histogram
	*	\param Image pointer to the source image
	*	\param Histogramm pointer to the resulting histogram
	*	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpGetHistogramm(const awpImage* Image, awpHistogramm* Histogramm);

    /*2D Histogramm functions */
	/**
	*	\brief Calculate 2D HS histogramm
	*	\param pImage pointer to the source image
	*	\param p2DHist pointer to the resulting histogramm
    *   \param low min threshold for V channel in HSV
    *   \param up max threshold for V channel in HSV
    *   \param needToConvert if true resulting histogramm is in BYTE, else in DOUBLE
	*	\return AWP_OK if success or else AWP_ERROR
	*/

    AWPRESULT awpGet2DHistogramm(awpImage* pImage, awpImage* p2DHist, BYTE low, BYTE up, int needToConvert);


	/**
	*	\brief Calculate maximum value of histogram
	*	\param Histogramm pointer to the histogram
	*	\param Result maximum value
	*	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpGetMax(const awpHistogramm* Histogramm, awpStat* Result);

	/**
	*	\brief Calculate minimum value of histogram
	*	\param Histogramm pointer to the histogram
	*	\param Result minimum value
	*	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpGetMin(const awpHistogramm* Histogramm, awpStat* Result);

	/**
	*	\brief Calculate average value of the histogram
	*	\param Histogramm pointer to the histogram
	*	\param Result average value
	*	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpGetAverage(const awpHistogramm* Histogramm, awpStat* Result);

	/**
	*	\brief Calculate dispersion value of the histogram
	*	\param Histogramm pointer to the histogram
	*	\param Result dispersion value
	*	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpGetDispersion(const awpHistogramm* Histogramm, awpStat* Result);

	/**
	*	\brief Calculate median value of the histogram
	*	\param Histogramm pointer to the histogram
	*	\param Result median value
	*	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpGetMedian(const awpHistogramm* Histogramm, awpStat* Result);
    


    /*Histogramm equalize function*/
	/**
	*	\brief Normalization of the light level with histogram equilization
	*	\param PImage pointer to the image
	*	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpHistogrammEqualize(awpImage* PImage);

        /** @}	end  histogroup */


    /** @defgroup contourgroup Contour functions
    *   @{
    */

	/**
	*	\brief Create contour object
	*	\param ppContour pointer to pointer to destination object
	*	\param NumPoints number of contour points
	*	\param dir contour direction
	*	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpCreateContour(awpContour** ppContour, int NumPoints, AWP_BOOL dir);

	/**
	*	\brief Delete contour object
	*	\param ppContour pointer to pointer to the contour object
	*	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpFreeContour(awpContour** ppContour);

	/**
	*	\brief Calculate circumscribed rectangle for contour
	*	\param pContour pointer to the contour object
	*	\param rect pointer to the circumscribed rectangle
	*	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpGetContourRect(const awpContour* pContour, awpRect* rect);

	/**
	*	\brief Check whether point belongs to the contour area
	*	\param pContour pointer to the contour object
	*	\param p pointer to the point
	*	\param result pointer to the check result
	*	\return AWP_OK if success or else AWP_ERROR
	*/
    AWPRESULT awpIsPointInContour(const awpContour* pContour, const awpPoint* p, AWP_BOOL* result);

    /** @} */ /* end of contourgroup */


	/** @defgroup edgegroup Edge functions
	*   @{
	*/

	/*
		awpEdgeZZZZ (awpImage* pImage, awpImage* pGradAmpl, awpImage* pGradDir, int Direction,...);
	*/

    /**
	*	\brief Perform Sobel operator on image
	*	\param pImage pointer to the source image
	*	\param pGradAmpl pointer to the resulting gradient image
	*   \param pGradDir pointer to gradient direction image. To supress gardient dir calculation 
	*	\      set pGradDir to NULL;
	*	\param Direction option for contours direction. Should be one of the following:\n
	*		AWP_EDGE_DIR_HORZ - horizontal direction\n
	*		AWP_EDGE_DIR_VERT - vertical direction\n
	*		AWP_EDGE_DIR_BOTH - both horizontal and vertical directions\n
	*	\return AWP_OK if success or else AWP_ERROR
	*	\remarks Resulting image is creating by the function. So *ppPhase should be NULL
	*/
	AWPRESULT awpEdgeSobel(awpImage* pImage, awpImage* pGradAmpl, awpImage* pGradDir, int Direction);

    /** @} */ /* end of Edge functions */


    /** @defgroup drawinggroup Drawing functions
    *   @{
    */

	/**
	*	\brief Solid fill
	*	\param pImage pointer to the source image
	*	\param pColor pointer to the fill color
	*	\return AWP_OK if success else AWP_ERROR
	*/
	AWPRESULT awpFill(awpImage *pImage, const awpColor *pColor);

	/**
	*	\brief Draw point
	*	\param pImage pointer to the image
	*	\param p point coordinates
	*	\param bChan image channel
	*	\param dValue pixel value
	*	\return AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpDrawPoint(awpImage* pImage, awpPoint p, BYTE bChan, DOUBLE dValue);

	/**
	*	\brief Draw thick point
	*	\param pImage pointer to the image
	*	\param p point coordinates
	*	\param bChan image channel
	*	\param dValue pixel value
	*	\return AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpDrawThickPoint(awpImage* pImage, awpPoint p, BYTE bChan, DOUBLE dValue, BYTE Thickness);

	/**
	*	\brief Draw line
	*	\param pImage pointer to the image
	*	\param p1 start point of the line
	*	\param p2 finish point of the line
	*	\param bChan image channel
	*	\param dValue pixels value
	*	\return AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpDrawLine(awpImage* pImage, awpPoint p1, awpPoint p2, BYTE bChan, DOUBLE dValue);

    	/**
	*	\brief Draw Thick line
	*	\param pImage pointer to the image
	*	\param p1 start point of the line
	*	\param p2 finish point of the line
	*	\param bChan image channel
	*	\param dValue pixels value
	*	\return AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpDrawThickLine(awpImage* pImage, awpPoint p1, awpPoint p2, BYTE bChan, DOUBLE dValue, BYTE Thickness);

	/**
	*	\brief Draw rectangle
	*	\param pImage pointer to the image
	*	\param pRect rectangle to draw
	*	\param bChan image channel
	*	\param dValue pixels value
	*	\return AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpDrawRect(awpImage* pImage, awpRect* pRect, BYTE bChan, DOUBLE dValue);

	/**
	*	\brief Draw thick rectangle
	*	\param pImage pointer to the image
	*	\param pRect rectangle to draw
	*	\param bChan image channel
	*	\param dValue pixels value
	*	\return AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpDrawThickRect(awpImage* pImage, awpRect* pRect, BYTE bChan, DOUBLE dValue, BYTE Thickness);

	/**
	*	\brief Draw cross
	*	\param pImage pointer to the image
	*	\param pRect cross rectangle
	*	\param bChan image channel
	*	\param dValue pixels value
	*	\return AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpDrawCross(awpImage* pImage, awpRect* pRect, BYTE bChan, DOUBLE dValue);

	/**
	*	\brief Draw thick cross
	*	\param pImage pointer to the image
	*	\param pRect cross rectangle
	*	\param bChan image channel
	*	\param dValue pixels value
	*	\return AWP_OK if success else AWP_ERROR
	*/
    AWPRESULT awpDrawThickCross(awpImage* pImage, awpRect* pRect, BYTE bChan, DOUBLE dValue, BYTE Thickness);


    AWPRESULT awpDrawEllipse(awpImage* pImage, awpPoint center, WORD width, WORD height, DOUBLE angle, BYTE bChan, DOUBLE dValue);
    AWPRESULT awpDrawEllipseV2(awpImage* pImage, awpPoint center, WORD width, WORD height, DOUBLE angle, BYTE bChan, DOUBLE dValue);
    AWPRESULT awpDrawPolygon(awpImage* pImage, awpContour* pContour, BYTE bChan, DOUBLE dValue);

    AWPRESULT awpFillPolygon(awpImage* pImage, awpContour* pContour, BYTE bChan, DOUBLE dValue, DOUBLE dFillValue);

#define awpDrawCEllipse(pImage, p,w,h,a, r, g, b)\
	{\
	awpDrawEllipse(pImage, p, w, h, a, 0, r);\
	awpDrawEllipse(pImage, p, w, h, a, 1, g);\
	awpDrawEllipse(pImage, p, w, h, a, 2, b);\
	}\


#define awpDrawCEllipseV2(pImage, p,w,h,a, r, g, b)\
	{\
	awpDrawEllipseV2(pImage, p, w, h, a, 0, r);\
	awpDrawEllipseV2(pImage, p, w, h, a, 1, g);\
	awpDrawEllipseV2(pImage, p, w, h, a, 2, b);\
	}\


	/**
	*	\brief Draw color point
	*	\param pImage pointer to the image
	*	\param p point coordinates
	*	\param r red channel value
	*	\param g green channel value
	*	\param b blue channel value
	*/
#define awpDrawCPoint(pImage, p, r, g, b)\
	{\
	awpDrawPoint(pImage, p, 0, r);\
	awpDrawPoint(pImage, p, 1, g);\
	awpDrawPoint(pImage, p, 2, b);\
	}\

	/**
	*	\brief Draw color line
	*	\param pImage pointer to the image
	*	\param p1 line start point
	*	\param p2 line end point
	*	\param r red channel value
	*	\param g green channel value
	*	\param b blue channel value
	*/
#define awpDrawCLine(pImage, p1, p2, r, g, b)\
	{\
	awpDrawLine(pImage, p1, p2, 0, r);\
	awpDrawLine(pImage, p1, p2, 1, g);\
	awpDrawLine(pImage, p1, p2, 2, b);\
	}\

	/**
	*	\brief Draw color rectangle
	*	\param pImage pointer to the image
	*	\param pRect rectangle to draw
	*	\param r red channel value
	*	\param g green channel value
	*	\param b blue channel value
	*/
#define awpDrawCRect(pImage, pRect, r, g, b)\
	{\
	awpDrawRect(pImage, pRect, 0, r);\
	awpDrawRect(pImage, pRect, 1, g);\
	awpDrawRect(pImage, pRect, 2, b);\
	}\


	/**
	*	\brief Draw color cross
	*	\param pImage pointer to the image
	*	\param pRect cross rectangle to draw
	*	\param r red channel value
	*	\param g green channel value
	*	\param b blue channel value
	*/
#define awpDrawCCross(pImage, pRect, r, g, b)\
	{\
	awpDrawCross(pImage, pRect, 0, r);\
	awpDrawCross(pImage, pRect, 1, g);\
	awpDrawCross(pImage, pRect, 2, b);\
	}\

    /** @} */ /* end of drawinggroup  */


#define _8_NEIGHBOURS	0
#define _4_NEIGHBOURS	1

AWPRESULT awpLocalMax(awpImage* pImage, awpImage** ppImage, BYTE thr, int options);
AWPRESULT awpLocalMin(awpImage* pImage, awpImage** ppImage, BYTE thr, int options);

#define DELETE_UNKNOWN_MAX	0
#define LEAVE_UNKNOWN_MAX	1

AWPRESULT awpFeatures(awpImage* pImage, awpImage** ppImage, BYTE thr_max, BYTE thr_min, BYTE relative_thr, int radius, int MinMax_options, int options);
AWPRESULT awpFeaturesV2(awpImage* pImage, awpImage** ppImage, BYTE thr_max, BYTE relative_thr, int radius, int MinMax_options);

AWPRESULT awpFilter(awpImage* pImage, int options, int method);

AWPRESULT awpHSVColorFilterPreset(awpImage* pImage, awpImage* pPreset, awpImage** ppMask, BYTE thr);

/*Point functions*/
AWPRESULT awpDistancePoints(awpPoint p1, awpPoint p2, DOUBLE* distance);

#define LINEAR  0
#define SQUARE  1
#define RLINEAR	2
#define RSQUARE	3

AWPRESULT awpIntegral( awpImage* pSrc, awpImage** ppDst, int type );

AWPRESULT awpFastIntegral( awpImage* pSrc, awpImage** ppDst, int type );

//[ANDX]
//Creating shifted image for fast computing;
//COMMENT: At 0 rotated image added 1 pixel both to width and height;
//At 45 rotated added 1 pixel to height;
AWPRESULT awpShlIntegral( awpImage* pSrc, awpImage** ppDst, int type );

AWPRESULT awpIntegral2( awpImage const* pSrc, awpImage* pDst, int type );

AWPRESULT awpBilinearBlur( awpImage* pImage, DOUBLE dfStrength );

//The result dispercy 0.5, mean 0.5 -> range = 1;
AWPRESULT awpNormalizeAdaptive( awpImage* pImage,  awpImage const* pAverage, awpImage const* pDispercy );

AWPRESULT awpDispercy( awpImage const* pImage, awpImage const* pAverage, awpImage* pDispercy );
#ifdef __cplusplus
}
#endif

#endif  /*__AWP_IPL__ */
