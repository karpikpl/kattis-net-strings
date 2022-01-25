using System.IO;
using System.Linq;
using NUnit.Framework;

namespace KattisSolution.Helpers.Tests
{
    [TestFixture]
    [Category("Helpers")]
    public class TestCaseFinderTests
    {
        private string _testCaseInPath;
        private string _testCaseOutPath;
        private const string TestCaseIn = "12345.in";
        private const string TestCaseOut = "12345.ans";

        [OneTimeSetUp]
        public void SetUp()
        {
            var solutionDir = Directory.GetParent(TestContext.CurrentContext.TestDirectory).Parent.Parent;
            _testCaseInPath = Path.Combine(solutionDir.FullName, TestCaseIn);
            _testCaseOutPath = Path.Combine(solutionDir.FullName, TestCaseOut);

            using (var f = File.CreateText(_testCaseInPath))
            {
                f.Write("11\n");
            }
            using (var f = File.CreateText(_testCaseOutPath))
            {
                f.Write("55\n");
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            if (File.Exists(_testCaseInPath))
                File.Delete(_testCaseInPath);
            if (File.Exists(_testCaseOutPath))
                File.Delete(_testCaseOutPath);
        }

        [Test]
        public void TestCaseFinder_Should_FindTestCasesInTheSolutionDir()
        {
            // Arrange


            // Act
            var result = TestCaseFinder.GetTestCases().ToList();

            // Assert
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Any(r => r.In.EndsWith(TestCaseIn) && r.Out.EndsWith(TestCaseOut)), Is.True);
        }
    }
}
