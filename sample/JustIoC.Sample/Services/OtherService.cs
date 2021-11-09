using System;

namespace JustIoC.Sample.Services
{
    public class OtherService : ITestService
    {
        public OtherService(NewService service, AnotherService srv)
        {

        }

        public void Foo()
        {
            Console.WriteLine("Other");
        }
    }

    public class AnotherService { }
}
