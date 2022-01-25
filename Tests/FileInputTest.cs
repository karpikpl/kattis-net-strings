using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using KattisSolution.Helpers;
using NUnit.Framework;

namespace KattisSolution.Tests
{
    [Category("Kattis samples")]
    [TestFixture]
    public class FileInputTest
    {
        [Test, TestCaseSource(nameof(GetTestCases))]
        public void AllCasesFromFiles2_Should_Pass(TestCase testCase)
        {

            Console.WriteLine("Running for {0}", testCase.In);
            // Arrange
            var expectedResult = File.ReadAllText(testCase.Out);

            using (var dataStream = File.OpenRead(testCase.In))
            using (var outStream = new MemoryStream())
            {
                // Act
                Program.Solve(dataStream, outStream, (_) => {});
                var result = Encoding.UTF8.GetString(outStream.ToArray());

                // Assert
                Assert.That(result, Is.EqualTo(expectedResult));
            }
        }

        private static IEnumerable<TestCase> GetTestCases()
        {
            return TestCaseFinder.GetTestCases();
        }
    }
}
