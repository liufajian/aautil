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

//! ���ַ����壬�����ڶ�����������
typedef char	TAPICHAR;

//! ����Ϊ70���ַ���
typedef char	TAPISTR_70[71];

//=============================================================================
/**
 *	\addtogroup G_DATATYPE_NUMBER	������ֵ���Ͷ���
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
 *	\addtogroup G_DATATYPE_YNFLAG	�Ƿ��ʾ
 *	@{
 */
 //=============================================================================
 //! �Ƿ��ʾ
typedef TAPICHAR			TAPIYNFLAG;
//! ��
const TAPIYNFLAG			APIYNFLAG_YES = 'Y';
//! ��
const TAPIYNFLAG			APIYNFLAG_NO = 'N';

//����ָ�붨��

enum class EventType
{
	Test1 = 1,
	Test2 = 2,
	Test3 = 3,
};

#ifdef __cplusplus
extern "C" {
#endif
	typedef void(MYDECL* TapQuoteAPIEvent)(EventType type, TAPIINT32 errorCode, TAPIYNFLAG isLast, const void* info);
#ifdef __cplusplus
}
#endif

//! �ͻ��ֲ�ӯ��
struct TapAPIPositionProfit
{
	const char* PositionNo;						///< ���سֲֺţ���������д
	TAPIUINT32					PositionStreamId;				///< �ֲ�����
	TAPIREAL64					PositionProfit;					///< �ֲ�ӯ��
	TAPIREAL64					LMEPositionProfit;				///< LME�ֲ�ӯ��
	TAPIREAL64					OptionMarketValue;				///< ��Ȩ��ֵ
	TAPIREAL64					CalculatePrice;					///< ����۸�
	TAPIREAL64					FloatingPL;						///< ��ʸ�ӯ

	explicit inline TapAPIPositionProfit()
		: PositionNo("")
		, PositionStreamId(1)
		, LMEPositionProfit(0)
		, OptionMarketValue(0)
		, CalculatePrice(0)
		, FloatingPL(0) {}
};

//! �ͻ��ֲ�ӯ��֪ͨ
struct TapAPIPositionProfitNotice
{
	TAPIYNFLAG			IsLast;					///< �Ƿ����һ��
	TapAPIPositionProfit* Data;					///< �ͻ��ֲ�ӯ����Ϣ
};

//! �ͻ��ֲ�ӯ��֪ͨM
struct TapAPIPositionProfitNoticeM
{
	TAPIYNFLAG	IsLast;							///< �Ƿ����һ��
	TAPISTR_70	PositionNo;						///< ���سֲֺţ���������д
	TAPIUINT32	PositionStreamId;				///< �ֲ�����
	TAPIREAL64	PositionProfit;					///< �ֲ�ӯ��
	TAPIREAL64	LMEPositionProfit;				///< LME�ֲ�ӯ��
	TAPIREAL64	OptionMarketValue;				///< ��Ȩ��ֵ
	TAPIREAL64	CalculatePrice;					///< ����۸�
	TAPIREAL64	FloatingPL;						///< ��ʸ�ӯ
};

struct TempStruct
{
	char* str;
	int num;

	// Note the strdup. You don't know the source of str.
	// For example if the source is "Foo", then you can't free it.
	// Using strdup solves this problem.
	inline explicit TempStruct(const char* str, int num)
		: str(_strdup(str)), num(num)
	{
	}

	~TempStruct()
	{
		free(str);
	}
};
