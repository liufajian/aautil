#pragma once
#include <iostream>
#include <string>
#include <cstdlib>
#include <sstream>
#include <cstring> 
#include <stdio.h>
using namespace std;

class StringConcat
{
public:

	void test() {
		test1();
		test2();
		test3();
		test4();
		test5();
	};

private:
	//��һ��C����ת��(��ǰһֱϲ����  sprintf ����ǿ��)
	void test1()
	{
		const char* s = "dong";
		int a = 52;
		float b = .1314;
		char* buf = new char[strlen(s) + sizeof(a) + 100];
		sprintf_s(buf, strlen(s) + sizeof(a) + 100, "%s%d%.4f", s, a, b);
		printf("test1: %s\n", buf);
	};

	//��C��C++���
	void test2()
	{
		string s = "dong";
		int a = 520;
		char* buf = new char[10];//2147483647 int���ֵ
		_itoa_s(a, buf, 10, 10);      //itoa��Ȼ����ת��Ϊ���ֽ��ƣ�����ע�ⲻ����float����double
		cout << "test2: " << s + buf << " | ";
		_itoa_s(a, buf, 10, 16);
		cout << "test2: " << s + buf << endl;
	};

	//��C++���
	void test3()
	{
		string s = "������";
		int a = 52;
		double b = .1314;
		ostringstream oss;
		oss << s << a << b;
		cout << "test3: " << oss.str() << endl;
	};

	//C++11������
	void test4()
	{
		int a = 520;
		float b = 5.20;
		string str = "dong";
		string res = str + to_string(a);
		cout << "test4: " << res << endl;
		res = str + to_string(b);
		res.resize(8);
		cout << "test4: " << res << endl;
	};

	void test5() {
		int nfun = 10001;
		string str = "dong " + to_string(nfun) + " ni hao ma";
		cout << "test5: " << str << endl;

		const char* cc = str.c_str();

		cout << "test5: " << cc << endl;
	};
};

