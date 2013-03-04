#include "_3DAnaglyph.h"

C3dVideoConvertorCV::C3dVideoConvertorCV()
{
	m_AnaglyphType = ColorAnaglyph;
	m_loader = NULL;
	m_writer = NULL;
}
C3dVideoConvertorCV::~C3dVideoConvertorCV()
{
	if (m_loader)
		cvReleaseCapture(&m_loader);
	if (m_writer)
		cvReleaseVideoWriter(&m_writer);
}


int C3dVideoConvertorCV::Init(const char* lpSrcName, const char* lpDstName, int AnaglyphType)
{
	m_loader = cvCaptureFromFile(lpSrcName);
	if (m_loader == NULL)
		return ERR_LOAD_SOURCE;
	//чтение свойств открытого видеопотока
	CvSize s;
	double fps;
	double val = 0;
	val = cvGetCaptureProperty(m_loader, CV_CAP_PROP_FRAME_WIDTH);
	s.width = (int)val;
	val = cvGetCaptureProperty(m_loader, CV_CAP_PROP_FRAME_HEIGHT);
	s.height = (int)val;
	m_ImageSize = s;
	val = cvGetCaptureProperty(m_loader, CV_CAP_PROP_FPS);
	fps = val;m_Fps = fps;
	val = cvGetCaptureProperty(m_loader, CV_CAP_PROP_FRAME_COUNT);
	m_FrameCount = (int)val;
	//создание видеопотока на запись. 
	m_writer = cvCreateVideoWriter(lpDstName, 0, fps, s);
	if (m_writer == NULL)
	{
		cvReleaseCapture(&m_loader);
		return ERR_CREATE_DESTINATION;
	}
	return NOERROR;
}
C3dTimeShift::C3dTimeShift():C3dVideoConvertorCV()
{
	m_Count = 0;
	m_FrameShift = 100;
	memset(m_buffer, 0, sizeof(m_buffer));
}
C3dTimeShift::~C3dTimeShift ()
{
	for (int i = 0; i < m_FrameShift; i++)
		cvReleaseImage(&m_buffer[i]);
}
int C3dTimeShift::Convert()
{
	IplImage* pimg = NULL;
	IplImage* lep  = NULL;
	IplImage* rep  = NULL;
	IplImage* anaglyph = NULL;
	m_FrameShift = m_Fps;
	for (int i = 0; i < m_FrameShift; i++)
		m_buffer[i] = cvCreateImage(m_ImageSize, IPL_DEPTH_8U, 3);
	anaglyph = cvCreateImage(m_ImageSize, IPL_DEPTH_8U, 3);
	do
	{
		pimg = cvQueryFrame(m_loader);
		if (pimg == NULL)
			break;
		if (m_Count < m_FrameShift)
		{
			cvCopy(pimg, m_buffer[m_Count]);
			m_Count++;
		}
		else
		{
			lep = m_buffer[0];
			rep = pimg;
			MakeAnaglyph(lep, rep, anaglyph, NULL, m_AnaglyphType);
#ifdef _DEBUG
			//cvSaveImage("anaglyph.png", anaglyph);
			//cvSaveImage("lep.png", lep);
			//cvSaveImage("rep.png", rep);
#endif 
			cvWriteFrame(m_writer, anaglyph);
			for (int i = 0; i < m_FrameShift-1; i++)
				m_buffer[i] = m_buffer[i+1];
			cvCopy(pimg,m_buffer[m_FrameShift-1]);

		}
	}while(true);
	cvReleaseImage(&anaglyph);
	return NOERROR;
}

int Make3DVideoAnaglyph(const char* lpSrcName, const char* lpDstName, int AnaglyphType, int Algorithm)
{
	//проверка входных аргументов
	if (lpSrcName == NULL || lpDstName == NULL)
		return ERR_INVALID_PARAMS;
	//проверка расширения имен файлов
	if (!CheckFileNameExt(".avi",lpSrcName))
		return ERR_INVALID_PARAMS;
	if (!CheckFileNameExt(".avi",lpDstName))
		return ERR_INVALID_PARAMS;

	C3dVideoConvertorCV* convertor = NULL;
	int result = 0;
	switch(Algorithm)
	{
	case TimeShift:
		convertor = new C3dTimeShift();
		result = convertor->Init(lpSrcName, lpDstName, AnaglyphType);
		break;

	}
	if (result != NOERROR)
	{
		delete convertor;
		return result;
	}
	result = convertor->Convert();
	delete convertor;
	return result;
}
