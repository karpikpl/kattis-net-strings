using System.IO;
using System.Text;
using KattisSolution.IO;
using NUnit.Framework;

namespace KattisSolution.Helpers.Tests
{
    [Category("Helpers")]
    [TestFixture]
    public class OptimizedPositiveIntReaderTests
    {
        [OneTimeSetUp]
        public void SetUp()
        {
        }

        [Test]
        public void OptimizedReader_Should_ThrowException_When_StreamEmpty()
        {
            // Arrange
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("")))
            {
                var target = new OptimizedPositiveIntReader(ms);

                // Act & Assert
                Assert.Throws<NoMoreTokensException>(() => target.NextInt());
            }
        }

        [Test]
        public void OptimizedReader_Should_Return0_When_0()
        {
            // Arrange
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("0")))
            {
                var target = new OptimizedPositiveIntReader(ms);

                // Act
                var result = target.NextInt();

                // Assert
                Assert.That(result, Is.EqualTo(0));
            }
        }

        [Test]
        public void OptimizedReader_Should_Return9_When_9()
        {
            // Arrange
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("9")))
            {
                var target = new OptimizedPositiveIntReader(ms);

                // Act
                var result = target.NextInt();

                // Assert
                Assert.That(result, Is.EqualTo(9));
            }
        }

        [Test]
        public void OptimizedReader_Should_Return12345_When_12345()
        {
            // Arrange
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("12345")))
            {
                var target = new OptimizedPositiveIntReader(ms);

                // Act
                var result = target.NextInt();

                // Assert
                Assert.That(result, Is.EqualTo(12345));
            }
        }

        [Test]
        public void OptimizedReader_Should_Return12_When_12AndSomeOtherText()
        {
            // Arrange
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(" \n 12 ")))
            {
                var target = new OptimizedPositiveIntReader(ms);

                // Act
                var result = target.NextInt();

                // Assert
                Assert.That(result, Is.EqualTo(12));
            }
        }
    }
}
