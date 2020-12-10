#pragma once 

//#pragma pack(push, 1)

#ifdef _WIN32
// Windows
#define FTDC2C_API extern __declspec(dllexport)
#define MYDECL	__stdcall
#else
// Linux
#define FTDC2C_API extern
#define MYDECL  __attribute__((stdcall))
#endif

#ifdef _WIN32
#define TAP_CDECL  __cdecl
#define TAP_DLLEXPORT __declspec(dllexport)
#else
#define TAP_CDECL
#define TAP_DLLEXPORT
#endif

//! 单字符定义，可用于定义其他类型
typedef char				TAPICHAR;

//=============================================================================
/**
 *	\addtogroup G_DATATYPE_NUMBER	基本数值类型定义
 *	@{
 */
 //=============================================================================

//! int 32
typedef int					TAPIINT32;
//! unsigned 32
typedef unsigned int		TAPIUINT32;
//! int 64
typedef long long			TAPIINT64;
//! unsigned 64
typedef unsigned long long	TAPIUINT64;
//! unsigned 16
typedef unsigned short		TAPIUINT16;
//! unsigned 8
typedef unsigned char		TAPIUINT8;
//! real 64
typedef double				TAPIREAL64;

//=============================================================================
/**
 *	\addtogroup G_DATATYPE_YNFLAG	是否标示
 *	@{
 */
 //=============================================================================
 //! 是否标示
typedef TAPICHAR			TAPIYNFLAG;
//! 是
const TAPIYNFLAG			APIYNFLAG_YES = 'Y';
//! 否
const TAPIYNFLAG			APIYNFLAG_NO = 'N';

//函数指针定义

enum class EventType
{

};

#ifdef __cplusplus
extern "C" {
#endif
	typedef void(MYDECL* TapQuoteAPIEvent)(EventType type, TAPIINT32 errorCode, TAPIYNFLAG isLast, const void* info);
#ifdef __cplusplus
}
#endif