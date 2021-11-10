using System;

namespace JustIoC.Tests.Models
{
    public class Human : IAnimal
    {
        public string Speak()
        {
            return "Hello!";
        }

        void Greet(string name)
        {
            Console.WriteLine($"Hello {name}!");
        }
    }
}
