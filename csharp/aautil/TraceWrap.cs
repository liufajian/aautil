using System;
using System.Diagnostics;

namespace aautil
{
    /// <summary>
    /// 
    /// </summary>
    public class TraceWrap
    {
        readonly string _traceName;

        public TraceWrap(string traceName)
        {
            _traceName = traceName ?? throw new ArgumentNullException(nameof(traceName));
        }

        public void WriteLine(string message)
        {
            Trace.WriteLine(message, _traceName);
        }

        public void WriteData(object data, string description)
        {
            if (data == null)
            {
                Trace.WriteLine((description ?? "TraceData") + ": null", _traceName);
            }
            else
            {
                Trace.WriteLine(new TraceDataWrap(data, description), _traceName);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TraceDataWrap
    {
        public TraceDataWrap()
        {

        }

        public TraceDataWrap(object data, string description)
        {
            Data = data;
            Description = description;
        }

        public object Data { get; set; }

        public string Description { get; set; }
    }
}
