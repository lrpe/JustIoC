using JustIoC.Tests.Models;
using Xunit;

namespace JustIoC.Tests
{
    public class JustContainerTests
    {
        [Fact(DisplayName = "Add Singleton Service")]
        public void AddSingletonTest()
        {
            var just = new JustContainer();
            just.Add<Human>(ServiceLifetime.Singleton);
            var human1 = just.Get<Human>();
            var human2 = just.Get<Human>();
            Assert.Equal<Human>(human1, human2);
        }

        [Fact(DisplayName = "Add Transient Service")]
        public void AddTransientTest()
        {
            var just = new JustContainer();
            just.Add<Human>(ServiceLifetime.Transient);
            var human1 = just.Get<Human>();
            var human2 = just.Get<Human>();
            Assert.NotEqual<Human>(human1, human2);
        }

        [Fact(DisplayName = "Add Singleton using Descriptor")]
        public void AddDescriptorTest()
        {
            var just = new JustContainer();
            var descriptor = new JustDescriptor(typeof(Human), ServiceLifetime.Singleton);
            just.Add(descriptor);
            var human1 = just.Get<Human>();
            var human2 = just.Get<Human>();
            Assert.Equal<Human>(human1, human2);
        }

        [Fact(DisplayName = "Add Service with Implementation")]
        public void AddServiceWithImplementation()
        {
            var just = new JustContainer();
            just.Add<IAnimal, Human>();
            var human = just.Get<IAnimal>();
            var response = human.Speak();
            Assert.Equal("Hello!", response);
        }
    }
}
