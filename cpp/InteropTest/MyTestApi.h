#pragma once

#include "pch.h"
#include <string>

class MyTestApi
{
private:
	TapQuoteAPIEvent OnEvent{ nullptr };

public:
	void RegisterEventCallBack(TapQuoteAPIEvent onEvent) {
		OnEvent = onEvent;
	}

	void Test1() {
		OnEvent(EventType::Test1, 0, APIYNFLAG_YES, 0);
	}

	void Test2(TapAPIPositionProfitNotice* info) {
		TapAPIPositionProfitNoticeM* d = new TapAPIPositionProfitNoticeM();
		d->IsLast = info->IsLast;
		TapAPIPositionProfit* data = info->Data;
		d->CalculatePrice = data->CalculatePrice;
		d->FloatingPL = data->FloatingPL;
		d->LMEPositionProfit = data->LMEPositionProfit;
		d->OptionMarketValue = data->OptionMarketValue;
		d->PositionProfit = data->PositionProfit;
		d->PositionStreamId = data->PositionStreamId;
		strcpy_s(d->PositionNo, data->PositionNo);
		OnEvent(EventType::Test2, 0, APIYNFLAG_YES, d);
	}
};

