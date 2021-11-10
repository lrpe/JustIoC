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
    }
}
