#pragma once
#include "pch.h"
#include <string.h>
#include "MyTestApi.h"

#ifdef __cplusplus
extern "C" {
#endif

	FTDC2C_API void MYDECL GetVersion(char* buf, int n) {
		const char* version = "Hello World";
		strncpy_s(buf, strlen(version) + 1, version, n);
	}

	FTDC2C_API void* MYDECL CreateAPI() {
		MyTestApi* api = new MyTestApi();
		return api;
	}

	FTDC2C_API void MYDECL RegisterEventCallBack(void* pAPI, TapQuoteAPIEvent onEvent) {
		MyTestApi* api = static_cast<MyTestApi*>(pAPI);
		api->RegisterEventCallBack(onEvent);
	}

	FTDC2C_API void MYDECL API_Test1(void* pAPI) {
		MyTestApi* api = static_cast<MyTestApi*>(pAPI);
		api->Test1();
	}

	FTDC2C_API void MYDECL API_Test2(void* pAPI) {
		MyTestApi* api = static_cast<MyTestApi*>(pAPI);
		TapAPIPositionProfitNotice* info = new TapAPIPositionProfitNotice();
		info->IsLast = APIYNFLAG_YES;
		info->Data = new TapAPIPositionProfit();
		info->Data->CalculatePrice = 100.11;
		info->Data->FloatingPL = 100.12;
		info->Data->LMEPositionProfit = 100.13;
		info->Data->OptionMarketValue = 100.14;
		strcpy_s(info->Data->PositionNo, "very good");
		info->Data->PositionProfit = 100.15;
		info->Data->PositionStreamId = 100;
		api->Test2(info);
	}

	FTDC2C_API TapAPIPositionProfitNoticeM* MYDECL TestTapAPIPositionProfitNotice() {

		TapAPIPositionProfitNoticeM* info = new TapAPIPositionProfitNoticeM();

		info->CalculatePrice = 100.10;
		info->FloatingPL = 100.11;
		info->LMEPositionProfit = 100.12;
		info->OptionMarketValue = 100.13;
		info->PositionProfit = 100.14;
		info->PositionStreamId = 100;

		strcpy_s(info->PositionNo, "very good");

		info->IsLast = APIYNFLAG_YES;

		return info;
	};

	__declspec(dllexport) void GetArrResult(TempStruct*** outPtr, int* size)
	{
		*outPtr = new TempStruct * [2];

		(*outPtr)[0] = new TempStruct("sdf", 123);
		(*outPtr)[1] = new TempStruct("abc", 456);

		*size = 2;
	}

	__declspec(dllexport) void FreeArrSomeData(TempStruct** ptr, int size)
	{
		for (int i = 0; i < size; i++)
		{
			delete ptr[i];
		}

		delete[] ptr;
	}

#ifdef __cplusplus
}
#endif


