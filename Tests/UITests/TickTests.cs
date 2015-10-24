using System;
using System.Drawing;

using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Clocker.UI;
using Clocker.Interfaces;

namespace UnitTests.UITests
{
    [TestClass]
    public class TickTests
    {
        private Tick _tick;
        private Mock<IMathService> _mathService;
        private Mock<IGraphicsService> _graphicService;

        [TestInitialize]
        public void Setup()
        {
            _graphicService = new Mock<IGraphicsService>();
            _mathService = new Mock<IMathService>();
            _tick = new Tick(_mathService.Object, Color.White);
        }

        [TestMethod]
        public void TickTests_Draw()
        {
            _tick.Draw(_graphicService.Object);
            _mathService.Verify(x => x.GetPoint(It.IsAny<double>(), Tick.ShortTickEnd), Times.Exactly(12));
            _mathService.Verify(x => x.GetPoint(It.IsAny<double>(), Tick.LongTickEnd), Times.Exactly(48));
            _mathService.Verify(x => x.GetPoint(It.IsAny<double>(), Tick.TickStart), Times.Exactly(60));
            _graphicService.Verify(x => x.DrawLine(It.IsAny<Pen>(), It.IsAny<PointF>(), It.IsAny<PointF>()), Times.Exactly(60));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Null graphics wasn't thrown")]
        public void TicksTest_DrawNullGraphics()
        {
            _tick.Draw(null);
        }
    }
}
