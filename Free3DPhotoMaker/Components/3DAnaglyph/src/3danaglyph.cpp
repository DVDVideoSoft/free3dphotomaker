#include "_3DAnaglyph.h"
 //true anaglyph
const float c_fA11 = 0.299f;
const float c_fA21 = 0.587f;
const float c_fA31 = 0.114f;
static void _MakeTrueAnaglyph(IplImage* src1, IplImage* src2, IplImage* dst,progress_func f)
{
	_awpColor* s1 = NULL;
	_awpColor* s2 = NULL;
	_awpColor* d = NULL;
    int i,j,k;
	for (j = 0; j < src1->height; j++)
	{
		s1 = (_awpColor*)(src1->imageData + j*src1->widthStep);
		s2 = (_awpColor*)(src2->imageData + j*src2->widthStep);
		d = (_awpColor*)(dst->imageData + j*dst->widthStep);
		for (i = 0; i < src1->width; i++)
		{
			d[i].bBlue = (unsigned char)(c_fA31*s1[i].bRed + c_fA21*s1[i].bGreen + c_fA11*s1[i].bBlue);
			d[i].bGreen = 0;
			d[i].bRed = (unsigned char)(c_fA31*s2[i].bRed + c_fA21*s2[i].bGreen + c_fA11*s2[i].bBlue);
		}
		k = 100 * j / src1->height;
		if (f != NULL)
			f(k);
	}
}
static void _MakeGrayAnaglyph(IplImage* src1, IplImage* src2, IplImage* dst,progress_func f)
{
	_awpColor* s1 = NULL;
	_awpColor* s2 = NULL;
	_awpColor* d = NULL;
    int i,j,k;
	for (j = 0; j < src1->height; j++)
	{
		s1 = (_awpColor*)(src1->imageData + j*src1->widthStep);
		s2 = (_awpColor*)(src2->imageData + j*src2->widthStep);
		d = (_awpColor*)(dst->imageData + j*dst->widthStep);
		for (i = 0; i < src1->width; i++)
		{
			d[i].bBlue = (unsigned char)(c_fA31*s1[i].bRed + c_fA21*s1[i].bGreen + c_fA11*s1[i].bBlue);
			d[i].bGreen = (unsigned char)(c_fA31*s2[i].bRed + c_fA21*s2[i].bGreen + c_fA11*s2[i].bBlue);
			d[i].bRed = (unsigned char)(c_fA31*s2[i].bRed + c_fA21*s2[i].bGreen + c_fA11*s2[i].bBlue);
		}
		k = 100 * j / src1->height;
		if (f != NULL)
			f(k);
	}
}
static void _MakeColorAnaglyph(IplImage* src1, IplImage* src2, IplImage* dst,progress_func f)
{
	_awpColor* s1 = NULL;
	_awpColor* s2 = NULL;
	_awpColor* d = NULL;
    int i,j,k;
	for (j = 0; j < src1->height; j++)
	{
		s1 = (_awpColor*)(src1->imageData + j*src1->widthStep);
		s2 = (_awpColor*)(src2->imageData + j*src2->widthStep);
		d = (_awpColor*)(dst->imageData + j*dst->widthStep);
		for (i = 0; i < src1->width; i++)
		{
			d[i].bBlue = s1[i].bBlue;
			d[i].bGreen = s2[i].bGreen;
			d[i].bRed = s2[i].bRed;
		}
		k = 100 * j / src1->height;
		if (f != NULL)
			f(k);
	}
}
static void _MakeHalfColorAnaglyph(IplImage* src1, IplImage* src2, IplImage* dst,progress_func f)
{
	_awpColor* s1 = NULL;
	_awpColor* s2 = NULL;
	_awpColor* d = NULL;
    int i,j,k;
	for (j = 0; j < src1->height; j++)
	{
		s1 = (_awpColor*)(src1->imageData + j*src1->widthStep);
		s2 = (_awpColor*)(src2->imageData + j*src2->widthStep);
		d = (_awpColor*)(dst->imageData + j*dst->widthStep);
		for (i = 0; i < src1->width; i++)
		{
		d[i].bBlue = (unsigned char)(c_fA31*s1[i].bRed + c_fA21*s1[i].bGreen + c_fA11*s1[i].bBlue);
		d[i].bGreen = s2[i].bGreen;
		d[i].bRed = s2[i].bRed;
		}
		k = 100 * j / src1->height;
		if (f != NULL)
			f(k);
	}
}
static void _MakeOptimizedAnaglyph(IplImage* src1, IplImage* src2, IplImage* dst, progress_func f)
{
	_awpColor* s1 = NULL;
	_awpColor* s2 = NULL;
	_awpColor* d = NULL;
    int i,j,k;
	for (j = 0; j < src1->height; j++)
	{
		s1 = (_awpColor*)(src1->imageData + j*src1->widthStep);
		s2 = (_awpColor*)(src2->imageData + j*src2->widthStep);
		d = (_awpColor*)(dst->imageData + j*dst->widthStep);
		for (i = 0; i < src1->width; i++)
		{
			d[i].bBlue = (unsigned char)(c_fA31*s1[i].bRed + c_fA21*s1[i].bGreen);
			d[i].bGreen = s2[i].bGreen;
			d[i].bRed = s2[i].bRed;
		}
		k = 100 * j / src1->height;
		if (f != NULL)
			f(k);
	}
}
void MakeAnaglyph(IplImage* src1, IplImage* src2, IplImage* dst, progress_func f, int options)
{
	switch (options)
	{
	case TrueAnaglyph:
		_MakeTrueAnaglyph(src1,src2,dst,f);
		break;
	case GrayAnaglyph:
		_MakeGrayAnaglyph(src1,src2,dst,f);
		break;
	case ColorAnaglyph:
		_MakeColorAnaglyph(src1,src2,dst,f);
		break;
	case HalfColorAnaglyph:
		_MakeHalfColorAnaglyph(src1,src2,dst,f);
		break;
	case OptimizedAnaglyph:
		_MakeOptimizedAnaglyph(src1,src2,dst,f);
		break;
	}
}