namespace JustIoC.Tests.Models
{
    public class Dog : IAnimal
    {
        public string Speak()
        {
            return "Woof!";
        }
    }
}
