using System;

namespace InteropSample
{
    partial class InteropTest
    {
        class MyApiTest
        {
            IntPtr _api;
            readonly Interop.TapQuoteAPIEvent _onEvent;

            public MyApiTest()
            {
                _onEvent = OnEventCB;

            }

            public void Test()
            {
                if (_api == IntPtr.Zero)
                {
                    _api = Interop.CreateAPI();

                    if (_api == IntPtr.Zero)
                    {
                        Console.WriteLine("MyApiTest,创建API失败");
                        return;
                    }

                    Interop.RegisterEventCallBack(_api, _onEvent);
                }

                Interop.API_Test1(_api);

                Interop.API_Test2(_api);
            }

            private void OnEventCB(EventType type, int errorCode, TAPIYNFLAG isLast, IntPtr ptr)
            {
                switch (type)
                {
                    case EventType.Test1:
                        Console.WriteLine("Test1,isLast:" + isLast);
                        break;
                    case EventType.Test2:
                        TapAPIPositionProfitNotice info = Interop.ToField<TapAPIPositionProfitNotice>(ptr);
                        if (info != null)
                        {
                            Console.WriteLine("Test2," + info);
                        }
                        else
                        {
                            Console.WriteLine("Test2,info is null");
                        }
                        break;
                }
            }
        }
    }
}
