using Moq;
using Clocker.UI;
using Clocker.Interfaces;

using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.UITests
{
    [TestClass]
    public class NumeralsTest
    {
        private Numerals _numerals;
        private Mock<IMathService> _mathService;
        private Mock<IGraphicsService> _graphicService;

        [TestInitialize]
        public void Setup()
        {
            _graphicService = new Mock<IGraphicsService>();
            _mathService = new Mock<IMathService>();
            _numerals = new Numerals(_mathService.Object, Color.White);
        }

        [TestMethod]
        public void NumeralsTest_Draw()
        {
            _numerals.Draw(_graphicService.Object);

            _graphicService.Verify(x => x.DrawString(It.Is<string>(y => y == "I"), It.IsAny<Font>(), It.IsAny<Brush>(), It.IsAny<PointF>(), It.IsAny<StringFormat>()), Times.Once());
            _graphicService.Verify(x => x.DrawString(It.Is<string>(y => y == "II"), It.IsAny<Font>(), It.IsAny<Brush>(), It.IsAny<PointF>(), It.IsAny<StringFormat>()), Times.Once());
            _graphicService.Verify(x => x.DrawString(It.Is<string>(y => y == "III"), It.IsAny<Font>(), It.IsAny<Brush>(), It.IsAny<PointF>(), It.IsAny<StringFormat>()), Times.Once());
            _graphicService.Verify(x => x.DrawString(It.Is<string>(y => y == "IV"), It.IsAny<Font>(), It.IsAny<Brush>(), It.IsAny<PointF>(), It.IsAny<StringFormat>()), Times.Once());
            _graphicService.Verify(x => x.DrawString(It.Is<string>(y => y == "V"), It.IsAny<Font>(), It.IsAny<Brush>(), It.IsAny<PointF>(), It.IsAny<StringFormat>()), Times.Once());
            _graphicService.Verify(x => x.DrawString(It.Is<string>(y => y == "VI"), It.IsAny<Font>(), It.IsAny<Brush>(), It.IsAny<PointF>(), It.IsAny<StringFormat>()), Times.Once());
            _graphicService.Verify(x => x.DrawString(It.Is<string>(y => y == "VII"), It.IsAny<Font>(), It.IsAny<Brush>(), It.IsAny<PointF>(), It.IsAny<StringFormat>()), Times.Once());
            _graphicService.Verify(x => x.DrawString(It.Is<string>(y => y == "VIII"), It.IsAny<Font>(), It.IsAny<Brush>(), It.IsAny<PointF>(), It.IsAny<StringFormat>()), Times.Once());
            _graphicService.Verify(x => x.DrawString(It.Is<string>(y => y == "IX"), It.IsAny<Font>(), It.IsAny<Brush>(), It.IsAny<PointF>(), It.IsAny<StringFormat>()), Times.Once());
            _graphicService.Verify(x => x.DrawString(It.Is<string>(y => y == "X"), It.IsAny<Font>(), It.IsAny<Brush>(), It.IsAny<PointF>(), It.IsAny<StringFormat>()), Times.Once());
            _graphicService.Verify(x => x.DrawString(It.Is<string>(y => y == "XI"), It.IsAny<Font>(), It.IsAny<Brush>(), It.IsAny<PointF>(), It.IsAny<StringFormat>()), Times.Once());
            _graphicService.Verify(x => x.DrawString(It.Is<string>(y => y == "XII"), It.IsAny<Font>(), It.IsAny<Brush>(), It.IsAny<PointF>(), It.IsAny<StringFormat>()), Times.Once());

            _mathService.Verify(x => x.GetPoint(It.Is<double>(y => y.Equals(5 / 60d)), Numerals.NumeralsLength), Times.Once());
            _mathService.Verify(x => x.GetPoint(It.Is<double>(y => y.Equals(10 / 60d)), Numerals.NumeralsLength), Times.Once());
            _mathService.Verify(x => x.GetPoint(It.Is<double>(y => y.Equals(15 / 60d)), Numerals.NumeralsLength), Times.Once());
            _mathService.Verify(x => x.GetPoint(It.Is<double>(y => y.Equals(20 / 60d)), Numerals.NumeralsLength), Times.Once());
            _mathService.Verify(x => x.GetPoint(It.Is<double>(y => y.Equals(25 / 60d)), Numerals.NumeralsLength), Times.Once());
            _mathService.Verify(x => x.GetPoint(It.Is<double>(y => y.Equals(30 / 60d)), Numerals.NumeralsLength), Times.Once());
            _mathService.Verify(x => x.GetPoint(It.Is<double>(y => y.Equals(35 / 60d)), Numerals.NumeralsLength), Times.Once());
            _mathService.Verify(x => x.GetPoint(It.Is<double>(y => y.Equals(40 / 60d)), Numerals.NumeralsLength), Times.Once());
            _mathService.Verify(x => x.GetPoint(It.Is<double>(y => y.Equals(45 / 60d)), Numerals.NumeralsLength), Times.Once());
            _mathService.Verify(x => x.GetPoint(It.Is<double>(y => y.Equals(50 / 60d)), Numerals.NumeralsLength), Times.Once());
            _mathService.Verify(x => x.GetPoint(It.Is<double>(y => y.Equals(55 / 60d)), Numerals.NumeralsLength), Times.Once());
            _mathService.Verify(x => x.GetPoint(It.Is<double>(y => y.Equals(60 / 60d)), Numerals.NumeralsLength), Times.Once());

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Null graphics wasn't thrown")]
        public void NumeralsTest_DrawNullGraphics()
        {
            _numerals.Draw(null);
        }
    }
}
