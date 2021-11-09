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

            var testService = just.Get<TestService>();
            testService.Foo();
        }
    }
}
