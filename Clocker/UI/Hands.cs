using System;
using System.Drawing;

using Clocker.Interfaces;

namespace Clocker.UI
{
    /// <summary>
    /// Responsible for drawing minute/second/hour hands on an analog clock.
    /// </summary>
    public class Hands : IDrawable
    {
        /// <summary>
        /// Length of the hour hand, % of full radius.
        /// </summary>
        public const double HourLength = 0.4d;

        /// <summary>
        /// Length of the minute hand, % of full radius.
        /// </summary>
        public const double MinuteLength = 0.5d;

        /// <summary>
        /// Length of the second hand, % of full radius.
        /// </summary>
        public const double SecondLength = 0.6d;

        /// <summary>
        /// Interface for retrieving the date.
        /// </summary>
        private IDateTimeService _datetime;

        /// <summary>
        /// Reference to the maths object.
        /// </summary>
        private IMathService _mathService;

        /// <summary>
        /// Pen to use for the second hand
        /// </summary>
        private Pen _secondPen;

        /// <summary>
        /// Pen to use for the minute hand
        /// </summary>
        private Pen _minutePen;

        /// <summary>
        /// Pen to use for the hour hand
        /// </summary>
        private Pen _hourlyPen;

        /// <summary>
        /// Constructor. Sets up maths reference and initial colour.
        /// </summary>
        /// <param name="mathService"></param>
        /// <param name="initialColor"></param>
        public Hands (IMathService mathService, IDateTimeService dateTime, Color initialColor)
        {
            _datetime = dateTime;
            _mathService = mathService;
            _secondPen = new Pen(initialColor, 1);
            _minutePen = new Pen(initialColor, 2);
            _hourlyPen = new Pen(initialColor, 3);
        }

        /// <summary>
        /// Updates the color of the pends to something new.
        /// </summary>
        /// <param name="color"></param>
        public Color Color
        {
            set
            {
                _secondPen.Color = value;
                _minutePen.Color = value;
                _hourlyPen.Color = value;
            }
        }

        /// <summary>
        /// Draw hands on the clock
        /// </summary>
        /// <param name="graphics">Graphics context</param>
        public void Draw(IGraphicsService graphics)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException("graphics");
            }

            var now = _datetime.Now;
            var second = (now.Second / 60d);
            var minute = (now.Minute / 60d) + (second / 60d);
            var hourly = (now.Hour / 12d) + (minute / 12d);
            graphics.DrawLine(_secondPen, _mathService.Center, _mathService.GetPoint(second, SecondLength));
            graphics.DrawLine(_minutePen, _mathService.Center, _mathService.GetPoint(minute, MinuteLength));
            graphics.DrawLine(_hourlyPen, _mathService.Center, _mathService.GetPoint(hourly, HourLength));
        }

        /// <summary>
        /// Dispose of the created pens.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_secondPen != null)
                {
                    _secondPen.Dispose();
                    _secondPen = null;
                }
                if (_minutePen != null)
                {
                    _minutePen.Dispose();
                    _minutePen = null;
                }
                if (_hourlyPen != null)
                {
                    _hourlyPen.Dispose();
                    _hourlyPen = null;
                }
            }
        }
    }
}
