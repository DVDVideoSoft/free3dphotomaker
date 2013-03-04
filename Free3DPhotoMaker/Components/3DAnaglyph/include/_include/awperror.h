#ifndef _AWP_ERROR_H
#define _AWP_ERROR_H

#include <stdio.h>
#include <assert.h>
#include <string.h>

typedef  long AWPRESULT;

#define  AWP_BASE_ERR  0

/* base awp errors*/
#define AWP_OK					AWP_BASE_ERR
#define AWP_BADMEMORY				AWP_BASE_ERR + 1
#define AWP_NOTAWPIMAGE				AWP_BASE_ERR + 2
#define AWP_BADVERSION				AWP_BASE_ERR + 3
#define AWP_BADCOLOR				AWP_BASE_ERR + 4
#define AWP_NOTSUPPORT				AWP_BASE_ERR + 5
#define AWP_BADARG				AWP_BASE_ERR + 6
#define AWP_NOTENOUGH_MEM			AWP_BASE_ERR + 7
#define AWP_BADSIZES				AWP_BASE_ERR + 8
#define AWP_BADIMAGE_FORMAT			AWP_BASE_ERR + 9
#define AWP_NOT_COLOR_IMAGE			AWP_BASE_ERR + 10
#define AWP_BADAWPRECT				AWP_BASE_ERR + 11
#define AWP_NOT_IMPIMENTED			AWP_BASE_ERR + 12

#define AWP_FILE_BASE							100
#define AWP_CREATE_FILE_ERROR    AWP_BASE_ERR + 101
#define AWP_WRITE_FILE_ERROR     AWP_BASE_ERR + 102
#define AWP_OPEN_FILE_ERROR      AWP_BASE_ERR + 103
#define AWP_READ_FILE_ERROR      AWP_BASE_ERR + 104
#define AWP_NOT_AWP_FILE		 AWP_BASE_ERR + 105 	
#define AWP_CORRUPT_FILE		 AWP_BASE_ERR + 106


#define AWP_COLOR_BASE              200
#define AWP_MONOTONE				AWP_BASE_ERR + 201

#define AWP_COUNTOUR_BASE            300
#define AWP_CANNOT_CREATE_COUNTOURS  AWP_BASE_ERR + 301
#define AWP_STROKES_NOTFOUND		 AWP_COUNTOUR_BASE + 2	

#define AWP_SKELETON_BASE			400
#define AWP_SKELETON_OK				AWP_BASE_ERR + 401
#define AWP_SKELETON_BADMEMORY		AWP_BASE_ERR + 402
#define AWP_SKELETON_MAXLIN			AWP_BASE_ERR + 403 /* ���������� �� ���������*/
#define AWP_SKELETON_NULL			AWP_BASE_ERR + 404 /* ����� �� �����������*/
#endif
