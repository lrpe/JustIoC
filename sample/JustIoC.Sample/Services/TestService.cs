﻿using System;

namespace JustIoC.Sample.Services
{
    public class TestService : ITestService
    {
        public void Foo()
        {
            Console.WriteLine("Bar");
        }
    }
}
