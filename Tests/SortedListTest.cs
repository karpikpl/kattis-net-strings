using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using KattisSolution.Helpers;
using NUnit.Framework;

namespace KattisSolution.Tests
{
    [Category("Sorted List")]
    [TestFixture]
    public class SortedListTest
    {
        [Test]
        public void SortedList_Should_BeFast()
        {
            // Arrnage
            var target = new SortedList<int, int>();
            var random = new Random();

            // Act
            for (int i = 0; i < 100000; i++)
            {
                var rand = random.Next(0, 300000000);

                if (target.ContainsKey(rand))
                {
                    target[rand]++;
                }
                else
                {
                    target.Add(rand, 1);
                }
            }
        }


    }
}
