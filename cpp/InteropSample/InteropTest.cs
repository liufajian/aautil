using System;
using System.Text;
using System.Runtime.InteropServices;

namespace InteropSample
{
    partial class InteropTest
    {
        public InteropTest()
        {
            DLLHelper.EnsureDLL();
        }

        public InteropTest Test()
        {
            Console.WriteLine("GetVersion:" + GetVersion());

            var ptr = Interop.TestTapAPIPositionProfitNotice();

            if (ptr != IntPtr.Zero)
            {
                var ttt = Marshal.PtrToStructure<TapAPIPositionProfitNotice>(ptr);
                Console.WriteLine(ttt);
            }
            else
            {
                Console.WriteLine("TestTapAPIPositionProfitNotice get null");
            }

            TestArray();

            return this;
        }

        public InteropTest TestAPI()
        {
            new MyApiTest().Test();

            return this;
        }

        private static string GetVersion()
        {
            var sb = new StringBuilder(100);
            Interop.GetVersion(sb, 100);
            return sb.ToString();
        }

        private static void TestArray()
        {
            // C++ will return its TempStruct array in ptr
            IntPtr ptr;
            int size;

            Interop.GetArrResult(out ptr, out size);

            TempStruct[] someData2 = new TempStruct[size];

            for (int i = 0; i < size; i++)
            {
                IntPtr ptr2 = Marshal.ReadIntPtr(ptr, i * IntPtr.Size);
                someData2[i] = (TempStruct)Marshal.PtrToStructure(ptr2, typeof(TempStruct));
            }

            // Important! We free the TempStruct allocated by C++. We let the
            // C++ do it, because it knows how to do it.
            Interop.FreeArrSomeData(ptr, size);

            foreach (var item in someData2)
            {
                Console.WriteLine(item);
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1), Serializable]
        struct TempStruct
        {
            public string str;
            public int num;

            public override string ToString()
            {
                return $"[TempStruct]str:{str},num:{num}";
            }
        }
    }
}
