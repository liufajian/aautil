// InteropTest.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>

int main()
{
	std::cout << "Hello World!\n";

	struct s2
	{
		char q1;
		char q2;
		int i;
	};

	printf("%llu\n", sizeof(struct s2));
	system("pause");
}
