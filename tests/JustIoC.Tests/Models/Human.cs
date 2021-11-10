using System;

namespace JustIoC.Tests.Models
{
    public class Human
    {
        void Greet(string name)
        {
            Console.WriteLine($"Hello {name}!");
        }
    }
}
