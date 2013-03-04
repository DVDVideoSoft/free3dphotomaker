#ifndef __3danaglyph_h_
#define __3danaglyph_h_
#include "3DAnaglyph.h"
#include "cv.h"
#include <windows.h>
#include <Vfw.h>
#include "avi_utils.h"
#include <string>

#ifdef __cplusplus
extern "C" {
#endif
#include "cs.h"
#ifdef __cplusplus
}
#endif
#include "cxcore.h"
#include "highgui.h"


struct  _awpColor
{
	unsigned char bRed;
	unsigned char bGreen;
	unsigned char bBlue;
};
/*
//	���������� �������� ���������
*/
void MakeMaskGradient(IplImage* src1, IplImage* mask, IplImage* gr);
/*
//	������ �������������� ����������
*/
void GetSigmaFromGradients(IplImage* n1, IplImage* n2, IplImage* s);
/*
	����������
*/
void NormSigma(IplImage* s, IplImage* d);
/*
//	����������� ������������� ������
*/
void JointBilateralFilter(IplImage* depth1, IplImage* src1, IplImage* result);
/*
//	����������� ������
*/
void GaussFilter(IplImage* src1, IplImage* dst1);
/*
//������� �������������� ��������� ����������� � ����������� ����� ������� 
*/
void MakeSparceDepthMap(IplImage* src, IplImage* depth_map);
/*
// ��������� ������������ ����������� 
*/
int Interpolate1(IplImage* pSrc, IplImage* pConstMap, IplImage* pConstVals, IplImage* pResult);
/*
// ��������� ������ ����� �������	
*/
int MakeFullDepthMap(IplImage* src, IplImage* sparce_depth, IplImage* full_depth);
/*
// ��������� �������� ����������� � ������������ � ������ �������.
// ������������ �������������� �������� - 5 ���� (������������� ������� 0)
// ������������ ������������ �������� - 0 ����.
// ����������� ������� ����������� ���������� ����������
*/
void Displace(IplImage* src, IplImage* map, IplImage* dst, int max_shift);

void Displace1(IplImage* src, IplImage* map, IplImage* dst, int max_shift);
/*
// ��������� 3D �������� �� ���� �����������
*/
void MakeAnaglyph(IplImage* src1, IplImage* src2, IplImage* dst, progress_func f, int options);
/*
// ��������� ���������� ����������� 
*/
void MakeTile(IplImage* src1, IplImage* src2, IplImage* dst);
/*
// �������� ���������� ��� ����� �����
*/
bool CheckFileNameExt(const char* lpExt, const char* lpFileName);
/*
// �������� ������������ ����� ����� 
*/
bool AcceptFileName(const char* lpFileName, int options, bool isSrc);

int  _RemoveBorder(IplImage* src, IplImage* dst, int lx, int rx);

/*
// ��������� ��������������� �����������, ������������ ���������� OpenCV 
// ��� ������ � ������ ������������
*/
class C3dVideoConvertorCV
{
protected:
	
	CvCapture* m_loader;
	CvVideoWriter* m_writer;
	int		   m_AnaglyphType;
	int		   m_FrameCount;
	int		   m_Fps;
	CvSize	   m_ImageSize;
public:
	C3dVideoConvertorCV();
	virtual ~C3dVideoConvertorCV();
	virtual int Init(const char* lpSrcName, const char* lpDstName, int AnaglyphType);
	virtual int Convert() = 0;
};
const int MaxShift = 1000;
class C3dTimeShift : public C3dVideoConvertorCV
{
protected:
	int m_FrameShift;
	int m_Count;
	IplImage* m_buffer[MaxShift];
public:
	C3dTimeShift();
	virtual ~C3dTimeShift ();
	virtual int Convert();
};
#endif//__3danaglyph_h_