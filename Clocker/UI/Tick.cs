using System.Drawing;

using Clocker.Interfaces;
using System;

namespace Clocker.UI
{
    /// <summary>
    /// Responsible for drawing 60 ticks on an analog clock.
    /// </summary>
    public sealed class Tick : IDrawable
    {

        /// <summary>
        /// Where the short ticks end (% of total radius)
        /// </summary>
        public const double ShortTickEnd = 0.65d;

        /// <summary>
        /// Where the long ticks end (% of total radius)
        /// </summary>
        public const double LongTickEnd = 0.7d;

        /// <summary>
        /// Where all ticks start (% of total radius)
        /// </summary>
        public const double TickStart = 0.8d;

        /// <summary>
        /// Math service reference
        /// </summary>
        private IMathService _mathService;

        /// <summary>
        /// Pen to use for the ticks
        /// </summary>
        private Pen _tickPen;

        /// <summary>
        /// Constructor. Sets up the MathService reference and initial color.
        /// </summary>
        /// <param name="mathService"></param>
        /// <param name="initialColor"></param>
        public Tick(IMathService mathService, Color initialColor)
        {
            _mathService = mathService;
            _tickPen = new Pen(initialColor, 1);
        }

        /// <summary>
        /// Color to use for drawing the ticks.
        /// </summary>
        public Color Color
        {
            set
            {
                _tickPen.Color = value;
            }
        }

        /// <summary>
        /// Draws ticks on an analog clock.
        /// </summary>
        /// <param name="graphics">Graphics context</param>
        public void Draw(IGraphicsService graphics)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException("graphics");
            }

            for (var x = 1; x <= 60; x++)
            {
                var begPoint = _mathService.GetPoint(x / 60d, x % 5 == 0 ? ShortTickEnd : LongTickEnd);
                var endPoint = _mathService.GetPoint(x / 60d, TickStart);
                graphics.DrawLine(_tickPen, begPoint, endPoint);
            }
        }

        /// <summary>
        /// Cleans up resources associated with the pen.
        /// </summary>
        public void Dispose()
        {
            if (_tickPen != null) _tickPen.Dispose();
        }
    }
}
