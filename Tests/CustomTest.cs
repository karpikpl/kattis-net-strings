using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace KattisSolution.Tests
{
    [TestFixture]
    [Category("sample")]
    public class CustomTest
    {
        private readonly Random random = new Random();

        private int GetRandom(int min, int max, int? not = null)
        {
            int? r = null;

            do
            {
                r = random.Next(min, max);
            }
            while (not.HasValue && r == not.Value);

            return r.Value;
        }

        [Test]
        public void Random_Should_Work()
        {
            var a = GetRandom(1, 10);
            var b = GetRandom(1, 10, a);

            Assert.That(a, Is.Not.EqualTo(b));
        }

        [Test]
        public void LinkedList_Should_Work()
        {
            var target = new LinkedList<int>();
            LinkedList<int>[] solution = new LinkedList<int>[10];

            for (int i = 0; i < 10; i++)
            {
                solution[i] = new LinkedList<int>();
                for (int j = 0; j < 100000; j++)
                {
                    solution[i].AddLast(j);
                }
            }
            Assert.That(solution.Length, Is.EqualTo(10));
        }

        [Test]
        [Ignore("wrong test data")]
        [Repeat(10)]
        public void SampleTest_WithStringData_Should_Pass()
        {
            for (int _ = 0; _ < 10; _++)
            {

                System.Console.WriteLine("Solving...");
                // Arrange
                const string expectedAnswer = "foo\n";
                const int n = 10;
                const int length = 1;

                using (var input = new MemoryStream())
                {
                    try
                    {
                        input.Write(Encoding.UTF8.GetBytes(n + "\n"));

                        for (int i = 0; i < n; i++)
                        {
                            var next = new string(Enumerable.Repeat<char>(Convert.ToChar('a' + (i % 24)), length).ToArray());
                            input.Write(Encoding.UTF8.GetBytes(next));
                            input.Write(Encoding.UTF8.GetBytes("\n"));
                        }

                        for (int i = 1; i < n; i++)
                        {
                            var a = GetRandom(1, n + 1);
                            var b = GetRandom(1, n + 1, a);
                            input.Write(Encoding.UTF8.GetBytes($"{a} {b}\n"));
                        }


                        input.Seek(0, SeekOrigin.Begin);

                        using (var output = new MemoryStream())
                        {
                            // Act
                            Program.Solve(input, output, _ => { });
                            var result = Encoding.UTF8.GetString(output.ToArray());

                            // Assert
                            Assert.That(result, Is.Not.Empty);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        input.Seek(0, SeekOrigin.Begin);
                        System.Console.WriteLine(ex);
                        var problem = Encoding.UTF8.GetString(input.ToArray(), 0, (int)input.Length);
                        System.Console.WriteLine($"Problem: {problem}");

                        throw;
                    }
                }
            }
        }

        [Test]
        public void SampleTest_WithMaxString_Should_Pass()
        {
            // Arrange
            const string expectedAnswer = "foo\n";
            const int n = 10;
            const int length = 1000000;

            using (var input = new MemoryStream())
            {
                input.Write(Encoding.UTF8.GetBytes(n + "\n"));

                for (int i = 0; i < n; i++)
                {
                    var next = new string(Enumerable.Repeat<char>(Convert.ToChar('a' + (i % 24)), length).ToArray());
                    input.Write(Encoding.UTF8.GetBytes(next));
                    input.Write(Encoding.UTF8.GetBytes("\n"));
                }

                for (int i = 1; i < n; i++)
                {
                    input.Write(Encoding.UTF8.GetBytes($"1 {i + 1}\n"));
                }


                input.Seek(0, SeekOrigin.Begin);

                using (var output = new MemoryStream())
                {
                    // Act
                    Program.Solve(input, output, _ => { });
                    var result = Encoding.UTF8.GetString(output.ToArray());

                    // Assert
                    Assert.That(result, Is.Not.Empty);
                    Console.Write(result);
                }
            }
        }

        [Ignore("generate once")]
        [Test]
        public void GenerateTestData()
        {
            const int n = 10;
            const int length = 1000000;

            using (var input = File.OpenWrite("6.in"))
            {
                input.Write(Encoding.UTF8.GetBytes(n + "\n"));

                for (int i = 0; i < n; i++)
                {
                    var next = new string(Enumerable.Repeat<char>(Convert.ToChar('a' + (i % 24)), length).ToArray());
                    input.Write(Encoding.UTF8.GetBytes(next));
                    input.Write(Encoding.UTF8.GetBytes("\n"));
                }

                for (int i = 1; i < n; i++)
                {
                    var a = GetRandom(1, n + 1);
                    var b = GetRandom(1, n + 1, a);
                    input.Write(Encoding.UTF8.GetBytes($"1 {i + 1}\n"));
                }
            }
        }

        [Test]
        public void GenerateRandomData()
        {
            const int n = 4;
            List<List<int>> solutions = new List<List<int>>();
            HashSet<int> blocked = new HashSet<int>();
            List<int> steps = new List<int>();

            GenerateNextStep(n, steps, blocked, solutions);

            for (int i = 0; i < solutions.Count; i++)
            {
                if (i % 10 == 0)
                {
                    Console.Error.WriteLine(i * 100.0 / solutions.Count + " %");
                }
                TestWithRandomInput(n, solutions[i]);
            }
        }

        public void GenerateNextStep(int n, List<int> steps, HashSet<int> blocked, List<List<int>> solutions)
        {
            if (blocked.Count == n - 1 && steps.Count == (n - 1) * 2)
            {
                solutions.Add(steps);
                return;
            }

            for (int a = 1; a <= n; a++)
            {
                if (!blocked.Contains(a))
                {
                    for (int b = 1; b <= n; b++)
                    {
                        if (b != a && !blocked.Contains(b))
                        {
                            var nextSteps = new List<int>(steps);
                            nextSteps.Add(a);
                            nextSteps.Add(b);
                            var nextBlocked = new HashSet<int>(blocked);
                            nextBlocked.Add(b);

                            GenerateNextStep(n, nextSteps, nextBlocked, solutions);
                        }
                    }
                }
            }
        }

        public void TestWithRandomInput(int n, List<int> steps)
        {
            using (var input = new MemoryStream())
            {
                input.Write(Encoding.UTF8.GetBytes(n + "\n"));

                for (int i = 0; i < n; i++)
                {
                    var next = new string(Enumerable.Repeat<char>(Convert.ToChar('a' + (i % 24)), 2).ToArray());
                    input.Write(Encoding.UTF8.GetBytes(next));
                    input.Write(Encoding.UTF8.GetBytes("\n"));
                }

                for (int i = 0; i < steps.Count; i++)
                {
                    input.Write(Encoding.UTF8.GetBytes(steps[i].ToString()));
                    if (i % 2 == 0)
                    {
                        input.Write(Encoding.UTF8.GetBytes(" "));
                    }
                    else
                    {
                        input.Write(Encoding.UTF8.GetBytes("\n"));
                    }
                }

                input.Seek(0, SeekOrigin.Begin);

                using (var output = new MemoryStream())
                {
                    // Act
                    Program.Solve(input, output, (msg) => { });
                    var result = Encoding.UTF8.GetString(output.ToArray());

                    // Assert
                    Assert.That(result, Is.Not.Empty);
                    //Console.Error.WriteLine(result);
                }
            }

        }
    }
}
