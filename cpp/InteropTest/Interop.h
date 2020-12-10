#pragma once
#include "pch.h"
#include <string.h>

#ifdef __cplusplus
extern "C" {
#endif

	FTDC2C_API void MYDECL GetVersion(char* buf, int n) {
		const char* version = "Hello World";
		strncpy_s(buf, strlen(version) + 1, version, n);
	}

#ifdef __cplusplus
}
#endif


