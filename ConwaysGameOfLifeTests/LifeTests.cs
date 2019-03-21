using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConwaysGameOfLife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.Tests
{
    [TestClass()]
    public class LifeTests
    {
        [Ignore]
        [TestMethod()]
        public void LifeTest()
        {
            Assert.IsFalse(false);
        }

        [Ignore]
        [TestMethod()]
        public void ExecuteOneCycleTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CountEmpty0x0Matrix()
        {
            var testMatrix = new bool[0, 0];
            var life = new Life(testMatrix);
            var count = life.LiveNeighboursCount(0, 0);
            Assert.AreEqual(count, 0);
        }

        [TestMethod()]
        public void CountEmpty1x1Matrix()
        {
            var testMatrix = new bool[1, 1];
            var life = new Life(testMatrix);
            var count = life.LiveNeighboursCount(0, 0);
            Assert.AreEqual(count, 0);
        }

        [TestMethod()]
        public void CountEmpty3x3Matrix()
        {
            var testMatrix = new bool[3, 3];
            var life = new Life(testMatrix);
            var count = life.LiveNeighboursCount(0, 0);
            Assert.AreEqual(count, 0);
        }

        [TestMethod()]
        public void CountMatrixTest()
        {
            var testMatrix = new bool[3, 3];
            testMatrix[0, 0] = true;
            testMatrix[0, 2] = true;
            testMatrix[2, 0] = true;
            testMatrix[2, 2] = true;
            var life = new Life(testMatrix);
            var count = life.LiveNeighboursCount(1, 1);
            Assert.AreEqual(count, 4);
        }

        [TestMethod()]
        public void Rule1Test()
        {
            Assert.IsFalse(Life.DetermineStatus(true, 0));
            Assert.IsFalse(Life.DetermineStatus(true, 1));
        }

        [TestMethod()]
        public void Rule2Test()
        {
            Assert.IsTrue(Life.DetermineStatus(true, 2));
            Assert.IsTrue(Life.DetermineStatus(true, 3));
        }

        [TestMethod()]
        public void Rule3Test()
        {
            Assert.IsFalse(Life.DetermineStatus(true, 4));
            Assert.IsFalse(Life.DetermineStatus(true, 5));
            Assert.IsFalse(Life.DetermineStatus(true, 6));
            Assert.IsFalse(Life.DetermineStatus(true, 7));
            Assert.IsFalse(Life.DetermineStatus(true, 8));
        }

        [TestMethod()]
        public void Rule4Test()
        {
            Assert.IsTrue(Life.DetermineStatus(false, 3));
        }

        [TestMethod()]
        public void IsInBoundTest()
        {
            bool[,] matrixTest = new bool[3, 3];
            var life = new Life(matrixTest);
            Assert.IsTrue(life.IsInBound(2, 2));
            Assert.IsFalse(life.IsInBound(-1, 0));
            Assert.IsFalse(life.IsInBound(0, -1));
            Assert.IsFalse(life.IsInBound(3, 0));
            Assert.IsFalse(life.IsInBound(0, 3));
        }

    }
}