using System;

namespace InteropSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("进程:" + (Environment.Is64BitProcess ? "x64" : "x86"));

            new InteropTest().Test().TestStringInOut();

            Console.Read();
        }
    }
}
