//---------------------------------------------------------------------------
//#ifdef _OPEN_CV_
#ifndef AwpOpenCVH
#define AwpOpenCVH
//---------------------------------------------------------------------------
#include "cv.h"
#include "awpipl.h"

// awp to openCV image convertion
IplImage* awpConvertToIplImage(awpImage* src);
void awpImageToOpenCvImage(awpImage* src, IplImage* dst);
//openCV to awp convertion
awpImage* awpConvertToAwpImage(IplImage* src);


inline int iplWidth( IplImage* img )
{
    return !img ? 0 : !(img->roi) ? img->width : img->roi->width;
}

inline int iplHeight( IplImage* img )
{
    return !img ? 0 : !(img->roi) ? img->height : img->roi->height;
}


#endif
//#endif
