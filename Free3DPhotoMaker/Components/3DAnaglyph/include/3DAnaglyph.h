#ifndef _3DAnaglyph_h_
#define _3DAnaglyph_h_
/*
//	Интерфейс библиотеки выполняющей преобразование статического изображения 
//	в трехмерное анаглифное изображение.
*/
// опции преобразования
#define TrueAnaglyph			0
#define GrayAnaglyph			1
#define ColorAnaglyph			2
#define HalfColorAnaglyph		3
#define OptimizedAnaglyph		4
#define TiledImages				5
#define VGAAvi					6
#define HDAvi720				7
#define HDAvi1080				8
#define AnimatedGif				9
#define MacroVideo				10

/*
	Алгоритмы преобразования видео
*/
#define TimeShift				1
#define AdaptiveTimeShift		2

// коды ошибок
#define NOERROR					 0
#define ERR_LOAD_SOURCE			-1
#define ERR_CREATE_DESTINATION	-2
#define ERR_CREATE_DEPTH_MAP	-3
#define ERR_INVALID_PARAMS		-4
#define ERR_CREATE_STEREOPAIR	-5
#define ERR_SAVE_ANAGLYPH		-6
#define ERR_MAKE_DEPTH			-7
#define ERR_MEMORY				-8


typedef void(*progress_func)(int value); 
/*
// синтезируется анаглиф из дефокусированного изображения
*/
int Make3DAnaglyph(const char* lpSrcName, const char* lpDstName, progress_func f, int options = ColorAnaglyph);
/*
// синтезируется анаглиф из двух смещенных изображений
*/
int Make3DAnaglyphPair(const char* lpSrcName1, const char* lpSrcName2, const char* lpDstName, progress_func f, int options = ColorAnaglyph);
/*
// преобразование видеоролика в анаглиф
*/
int Make3DVideoAnaglyph(const char* lpSrcName, const char* lpDstName, int AnaglyphType = ColorAnaglyph, int Algorithm = TimeShift);
#endif //_3DAnaglyph_h_