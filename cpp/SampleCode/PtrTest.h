#pragma once

///�������
struct UfxError {
	int errcode;
	const char* errmsg;

	inline explicit UfxError()
		: errcode(0)
		, errmsg("") {}

	inline explicit UfxError(int errcode, const char* errmsg)
		: errcode(errcode)
		, errmsg(errmsg) {}
};

class PtrTest
{
public:

	void Test1(UfxError& err) {
		err.errcode = 1;
		err.errmsg = "nihao1";
	};

	void Test11(UfxError& err) {
		InnerTest11(err);
	};

	UfxError* Test2() {
		UfxError err;
		err.errcode = 2;
		err.errmsg = "nihao2";
		return &err;
	};

	UfxError* Test3() {
		UfxError* err = new UfxError();
		err->errcode = 3;
		err->errmsg = "nihao3";
		return err;
	};

	UfxError* Test4() {
		UfxError* err = new UfxError();
		InnerTest4(*err);
		return err;
	};

	UfxError* Test5() {
		UfxError err;
		InnerTest5(err);
		return &err;
	};

private:
	void InnerTest11(UfxError& err) {
		err.errcode = 11;
		err.errmsg = "test11";
	};
	void InnerTest4(UfxError& err) {
		err.errcode = 4;
		err.errmsg = "test4";
	};

	void InnerTest5(UfxError& err) {
		err.errcode = 5;
		err.errmsg = "test5";
	}
};

