using System;

namespace JustIoC.Sample.Services
{
    public class OtherService : ITestService
    {
        public void Foo()
        {
            Console.WriteLine("Other");
        }
    }
}
