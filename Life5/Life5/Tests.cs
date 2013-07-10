using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Life5
{
    [TestFixture]
    public class ProgramTest
    {
        private static readonly object[] AdjacentArgs =
            {
                new object[] {new Size(0, 0), 2},
                new object[] {new Size(2, 0), 1}
            };

        [TestCaseSource("AdjacentArgs")]
        public void CountAdjacentPoints(Size point, int expected)
        {
            var game = new Game(new HashSet<Size> { new Size(0, 0), new Size(1, 0), new Size(-1, 0) });
            Assert.AreEqual(expected, game.EnumerateAlive(point).Count());
        }


        private static readonly object[] StateArgs =
            {
                new object[] {new Size(0, 0), true},
                new object[] {new Size(1, 0), false}
            };

        [TestCaseSource("StateArgs")]
        public void TestNextPointState(Size point, bool expected)
        {
            var game = new Game(new HashSet<Size> { new Size(0, 0), new Size(1, 0), new Size(-1, 0) });
            Assert.AreEqual(expected, game.WillBeAlive(point));
        }

        [Test]
        public void TestNextStep()
        {
            var game = new Game(new HashSet<Size> { new Size(0, 0), new Size(1, 0), new Size(-1, 0) });
            CollectionAssert.AreEquivalent(
                new HashSet<Size> { new Size(0, 0), new Size(0, 1), new Size(0, -1) },
                game.NextStep()
                );
        }

//        [Test]
//        public void TestToString()
//        {
//            var game = new Game(new HashSet<Size> { new Size(0, 0), new Size(1, 0), new Size(-1, 0) });
//            Assert.AreEqual("", game.ToString());
//        }


    }
}
