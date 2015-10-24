using System.Drawing;

using Clocker.UI;
using Clocker.Interfaces;

using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests.UITests
{
    [TestClass]
    public class HandsTest
    {
        private Hands _hands;
        private Mock<IMathService> _mathService;
        private Mock<IDateTimeService> _dateTimeService;
        private Mock<IGraphicsService> _graphicService;

        [TestInitialize]
        public void Setup()
        {
            _graphicService = new Mock<IGraphicsService>();
            _mathService = new Mock<IMathService>();
            _dateTimeService = new Mock<IDateTimeService>();
            _hands = new Hands(_mathService.Object, _dateTimeService.Object, Color.White);
        }

        [TestMethod]
        public void HandsTest_Draw()
        {
            // there's no pretty way I can see to generate these values but to reimplement the logic here
            // I suppose I'm just testing that the right values is passed for the associated length value.
            var second = 30;
            var minute = 30;
            var hour = 6;
            var secondValue = second / 60d;
            var minuteValue = (minute / 60d) + (secondValue / 60d);
            var hourValue = (hour / 12d) + (minuteValue / 12d);

            _dateTimeService.Setup(x => x.Now).Returns(new System.DateTime(2015, 01, 01, hour, minute, second));

            _hands.Draw(_graphicService.Object);

            _mathService.Verify(x => x.GetPoint(It.Is<double>(y => y.Equals(secondValue)), Hands.SecondLength));
            _mathService.Verify(x => x.GetPoint(It.Is<double>(y => y.Equals(minuteValue)), Hands.MinuteLength));
            _mathService.Verify(x => x.GetPoint(It.Is<double>(y => y.Equals(hourValue)), Hands.HourLength));

            _graphicService.Verify(x => x.DrawLine(It.IsAny<Pen>(), It.IsAny<PointF>(), It.IsAny<PointF>()), Times.Exactly(3));

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Null graphics wasn't thrown")]
        public void HandsTest_DrawNullGraphics()
        {
            _hands.Draw(null);
        }

    }
}
