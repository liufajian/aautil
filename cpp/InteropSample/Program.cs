using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteropSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new InteropTest();
            Console.WriteLine("GetVersion:" + test.GetVersion());
            Console.Read();
        }
    }
}
