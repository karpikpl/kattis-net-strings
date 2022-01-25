using System.Collections.Generic;
using System.IO;
using System.Text;
using KattisSolution.IO;
using NUnit.Framework;

namespace KattisSolution.Helpers.Tests
{
    [Category("Helpers")]
    [TestFixture]
    public class LineReaderTests
    {
        [Test]
        public void LineReader_Should_Return2lines_When_InputIn2Lines()
        {
            // Arrange
            string[] expected = { "abc", "def" };
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("abc\ndef")))
            {
                var target = new LineReader(ms);

                // Act
                var result = new List<string>();
                while (target.HasNext())
                {
                    result.Add(target.Next());
                }

                // Assert
                CollectionAssert.AreEqual(expected, result);
            }
        }
    }
}
