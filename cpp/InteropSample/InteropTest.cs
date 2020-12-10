using System.Text;

namespace InteropSample
{
    class InteropTest
    {
        public InteropTest()
        {
            DLLHelper.EnsureDLL();
        }

        public string GetVersion()
        {
            var sb = new StringBuilder(100);
            Interop.GetVersion(sb, 100);
            return sb.ToString();
        }
    }
}
