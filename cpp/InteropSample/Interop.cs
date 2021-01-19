using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace InteropSample
{
    static class Interop
    {
        const string DllName = "exlibs\\InteropTest.dll";

        /// <summary>
        /// 指针转为实际数据对象
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToField<T>(IntPtr ptr)
        {
            return IntPtr.Zero == ptr ? default : Marshal.PtrToStructure<T>(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void TapQuoteAPIEvent(EventType type, int errorCode, TAPIYNFLAG isLast, IntPtr info);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TestStringInOut([MarshalAs(UnmanagedType.AnsiBStr)] string str);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetVersion(StringBuilder buffer, int capacity);

        //-------------------------------------------------------
        // api
        //-------------------------------------------------------

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr CreateAPI();

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void RegisterEventCallBack(IntPtr pApi, TapQuoteAPIEvent onEvent);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void API_Test1(IntPtr pApi);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void API_Test2(IntPtr pApi);

        //-------------------------------------------------------
        // test
        //-------------------------------------------------------

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TestTapAPIPositionProfitNotice();

        //-------------------------------------------------------
        // array
        //-------------------------------------------------------

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void GetArrResult(out IntPtr outPtr, out int numPtr);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void FreeArrSomeData(IntPtr ptr, int num);
    }

    public enum TAPIYNFLAG : byte
    {
        /// <summary>
        /// 是
        /// </summary>
        YES = (byte)'Y',

        /// <summary>
        /// 否
        /// </summary>
        NO = (byte)'N'
    }

    public enum EventType
    {
        Test1 = 1,
        Test2 = 2,
    }
}
