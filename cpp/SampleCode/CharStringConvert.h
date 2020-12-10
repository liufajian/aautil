#pragma once
#include <string>
#include <iostream>
#include <sstream>
using namespace std;

//from: https://www.cnblogs.com/kean0048/p/11060559.html

class CharStringConvert
{
private:

	// stringתchar*,��stringת��Ϊchar* ��3�ַ�����data(); c_str(); copy();
	static void StringToCharPtr() {

		//1) ����string��data()����
		string str1 = "hello";
		const char* p = str1.data();//��const������char *p=(char*)str1.data();
		cout << "string to char*,use string.data: " << str1 << endl;

		//2) ����string��c_str()����
		string str2 = "world";
		const char* p2 = str2.c_str();//ͬ�ϣ�Ҫ��const���ߵȺ��ұ���char*

		//һ��Ҫʹ��strcpy()����������������c_str()���ص�ָ�� 
		//���磺��ò�Ҫ����: 
		//char* c;
		//string s = "1234";
		//c = s.c_str(); //c���ָ�����������������Ϊs���������������ݱ�����
		//Ӧ�������ã� 
		char c[20];
		string str21 = "1234";
		strcpy_s(c, str21.c_str());

		//�پٸ�����
		//c_str() �� char* ��ʽ���� string �ں��ַ���
		//���һ������Ҫ��char*����������ʹ��c_str()������ 
		string str22 = "Hello World!";
		printf("string to char*,use c_str: %s \n", str22.c_str()); //��� "Hello World!"

		//3) ����string��copy()����
		size_t length;
		char buffer[8];
		string str3("Test string......");

		length = str3.copy(buffer, 7, 5);//����7�����Ƽ����ַ���5�����Ƶ�λ�ã�
		buffer[length] = '\0'; //ע���ֶ��ӽ�����������
		cout << "string to char*,use string.copy(buffer,7,5): " << buffer << endl;

		//��������

		//stringתconst char*
		std::string str4 = "HelloWorld!"; //��ʼ��string���ͣ������帳ֵ
		const char* constc4 = nullptr;//��ʼ��const char*���ͣ�����ֵΪ��
		constc4 = str4.c_str();             //string����תconst char*����
		printf_s("string to const char*: %s\n", str4.c_str());   //��ӡstring�������� .c_str()
		printf_s("string to const char*: %s\n", constc4);        //��ӡconst char*��������

		//stringתchar*
		std::string str5 = "HelloWorld!"; //��ʼ��string���ͣ������帳ֵ
		char* c5 = nullptr;      //��ʼ��char*���ͣ�����ֵΪ��
		const char* constc5 = nullptr;//��ʼ��const char*���ͣ�����ֵΪ��
		constc5 = str5.c_str();             //string����תconst char*����
		c5 = const_cast<char*>(constc5);    //const char*����תchar*����
		printf_s("string to char*: %s\n", str5.c_str());   //��ӡstring�������� .c_str()
		printf_s("string to char*: %s\n", c);              //��ӡchar*��������
	}

	//char*תstring,����ֱ�Ӹ�ֵ
	static void CharPtrToString() {
		string s;
		const char* p = "hello";
		s = p;
		cout << "char* to string:" << s << endl;
	}

	//stringתchar[]
	static void StringToCharArray() {

		//������ֱ�Ӹ�ֵ������ѭ��char* �ַ�������ַ���ֵ, Ҳ����ʹ��strcpy_s�Ⱥ���

		string pp = "HelloWorld";
		char p[100];
		unsigned int i;
		for (i = 0;i < pp.length();i++)
			p[i] = pp[i];
		p[i] = '\0';
		printf("string to char[]:%s\n", p);
	}

	//char[] תstring,����ֱ�Ӹ�ֵ
	static void CharArrayToString() {
		string s;
		char ch[] = "hello world";
		s = ch;
		cout << "char[] to string:" << s << endl;
	}

	//char[]תchar*,����ֱ�Ӹ�ֵ
	static void CharArrayToCharPt() {
		char arrc[20] = "HelloWorld!";  //��ʼ��char[] ���ͣ������帳ֵ
		std::string str;                //��ʼ��string
		const char* constc = nullptr;   //��ʼ��const char*
		char* c = nullptr;               //��ʼ��char*

		str = arrc;                      //char[]����תstring����
		constc = arrc;                   //char[]����תconst char* ����
		c = arrc;                        //char[]����תchar*����

		printf_s("char[]תchar*: %s\n", arrc);         //��ӡchar[]��������
		printf_s("char[]תchar*: %s\n", str.c_str());  //��ӡstring��������
		printf_s("char[]תchar*: %s\n", constc);       //��ӡconst char* ��������
		printf_s("char[]תchar*: %s\n", c);            //��ӡchar*��������
	}

	//char*תchar[]
	static void CharPtrToCharArray() {
		//����ֱ�Ӹ�ֵ������ѭ��char*�ַ�������ַ���ֵ,Ҳ����ʹ��strcpy_s�Ⱥ�����

		const char* c = "HelloWorld!";  //��ʼ��char* ���ͣ������帳ֵ
		char arrc[20] = { 0 };    //��ʼ��char[] ���ͣ������帳ֵ
		strncpy_s(arrc, c, 20);     //char*����תchar[] ����
		printf_s("char*תchar: %s\n", c);      //��ӡchar* ��������
		printf_s("char*תchar: %s\n", arrc);   //��ӡchar[]��������
	}

	//charתstring
	static void CharToString() {
		//1. ʹ��string()���캯������
		char c = 'F';
		string s = string(1, c);
		cout << "char to string:" << s << endl;

		//2. ʹ��stringstream�ַ���
		char c1 = 'F';
		stringstream ss;
		ss << c1;
		string s2;
		ss >> s2;
		cout << "char to string:" << s2 << endl;

		//3. ʹ��springf()����
		char c2 = 'F';
		char s3[10];
		sprintf_s(s3, "%c", c2);
		string s4 = string(s3);
		cout << "char to string:" << s4 << endl;
	}

public:
	static void Sample() {

		//string to char*
		StringToCharPtr();

		//char* to string
		CharPtrToString();

		//string to char[]
		StringToCharArray();

		//char[] to string
		CharArrayToString();

		//char[] to char*
		CharArrayToCharPt();

		//char* to char[]
		CharPtrToCharArray();

		//char to string
		CharToString();
	}

	static void Sample2() {
		size_t length;
		char buffer[8];
		string str("Test string......");
		cout << "str:" << str << endl;

		length = str.copy(buffer, 7, 5);
		buffer[length] = '\0';
		cout << "str.copy(buffer,7,5),buffer contains: " << buffer << endl;

		length = str.copy(buffer, str.size(), 5);
		buffer[length] = '\0';
		cout << "str.copy(buffer,str.size(),5),buffer contains:" << buffer << endl;
		length = str.copy(buffer, 7, 0);
		buffer[length] = '\0';
		cout << "str.copy(buffer,7,0),buffer contains:" << buffer << endl;
		length = str.copy(buffer, 7); //ȱʡ����pos��Ĭ��pos=0
		buffer[length] = '\0';
		cout << "str.copy(buffer,7),buffer contains:" << buffer << endl;

		length = str.copy(buffer, string::npos, 5);    //copy������2�����������˿����ǳ��ȣ�Ҳ������һ��λ�ã�string::npos��ʾ�ַ����Ľ���λ��
		buffer[length] = '\0';
		cout << "string::npos:" << (int)(string::npos) << endl;
		cout << "buffer[string::npos]:" << buffer[string::npos] << endl;
		cout << "buffer[length-1]:" << buffer[length - 1] << endl;
		cout << "str.copy(buffer,string::npos,5),buffer contains:" << buffer << endl;

		length = str.copy(buffer, string::npos);
		buffer[length] = '\0';
		cout << "str.copy(buffer,string::npos),buffer contains:" << buffer << endl;
		cout << "buffer[string::npos]:" << buffer[string::npos] << endl;
		cout << "buffer[length-1]:" << buffer[length - 1] << endl;
	}
};

