using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Clocker.Services;


namespace UnitTests.ServiceTests
{
    [TestClass]
    public class MathsTest
    {
        private MathService maths;

        [TestInitialize]
        public void Initialize()
        {
            maths = new MathService(new Rectangle(0, 0, 100, 100));
        }

        [TestMethod]
        public void MathsTest_GetPoint()
        {
            Assert.AreEqual(new Point(100, 50), maths.GetPoint(15 / 60d, 1.0d));
            Assert.AreEqual(new Point(50, 100), maths.GetPoint(30 / 60d, 1.0d));
            Assert.AreEqual(new Point(0, 50), maths.GetPoint(45 / 60d, 1.0d));
            Assert.AreEqual(new Point(50, 0), maths.GetPoint(60 / 60d, 1.0d));
        }

        [TestMethod]
        public void MathsTest_Center()
        {
            Assert.AreEqual(new Point(50, 50), maths.Center);
        }

        [TestMethod]
        public void MathsTest_Radius()
        {
            Assert.AreEqual(50, maths.Radius);
        }
    }
}
