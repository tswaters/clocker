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
        private SolidBrush _secondBrush;

        /// <summary>
        /// Pen to use for the minute hand
        /// </summary>
        private SolidBrush _minuteBrush;

        /// <summary>
        /// Pen to use for the hour hand
        /// </summary>
        private SolidBrush _hourlyBrush;

        /// <summary>
        /// Pen used for the center circle.
        /// </summary>
        private SolidBrush _centerBrush;

        /// <summary>
        /// Constructor. Sets up maths reference and initial colour.
        /// </summary>
        /// <param name="mathService"></param>
        /// <param name="initialColor"></param>
        public Hands (IMathService mathService, IDateTimeService dateTime, Color initialColor)
        {
            _datetime = dateTime;
            _mathService = mathService;
            _secondBrush = new SolidBrush(initialColor);
            _minuteBrush = new SolidBrush(initialColor);
            _hourlyBrush = new SolidBrush(initialColor);
            _centerBrush = new SolidBrush(initialColor);
        }

        /// <summary>
        /// Updates the color of the pends to something new.
        /// </summary>
        /// <param name="color"></param>
        public Color Color
        {
            set
            {
                _secondBrush.Color = value;
                _minuteBrush.Color = value;
                _hourlyBrush.Color = value;
                _centerBrush.Color = value;
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

            DrawHand(_secondBrush, second, SecondLength, 0.005f, graphics);
            DrawHand(_minuteBrush, minute, MinuteLength, 0.006f, graphics);
            DrawHand(_hourlyBrush, hourly, HourLength, 0.01f, graphics);
        }

        private void DrawHand(Brush brush, double interval, double length, double width, IGraphicsService graphics)
        {
            graphics.FillPolygon(brush, new PointF[]
            {
                _mathService.GetPoint(interval + 0.25f, Center.CircleSize / 2),
                _mathService.GetPoint(interval + width, length * 0.95f),
                _mathService.GetPoint(interval, length),
                _mathService.GetPoint(interval - width, length * 0.95f),
                _mathService.GetPoint(interval - 0.25f, Center.CircleSize / 2)
            });
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
                if (_centerBrush != null)
                {
                    _centerBrush.Dispose();
                    _centerBrush = null;
                }
                if (_secondBrush != null)
                {
                    _secondBrush.Dispose();
                    _secondBrush = null;
                }
                if (_minuteBrush != null)
                {
                    _minuteBrush.Dispose();
                    _minuteBrush = null;
                }
                if (_hourlyBrush != null)
                {
                    _hourlyBrush.Dispose();
                    _hourlyBrush = null;
                }
            }
        }
    }
}
