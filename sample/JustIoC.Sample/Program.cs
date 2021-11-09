using JustIoC.Sample.Services;
using System;

namespace JustIoC.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var just = new JustContainer();
            just.Add<TestService>();
            just.Add<ITestService, OtherService>();

            var testService = just.Get<TestService>();
            testService.Foo();

            var otherService = just.Get<ITestService>();
            otherService.Foo();
        }
    }
}
