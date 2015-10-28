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
        /// Constants for the hands - hour/minute/second each have different values.
        /// </summary>
        private static class Constants
        {
            /// <summary>
            /// 1/4 of a the cirlce, used to move the base of the hand out from the center.
            /// </summary>
            public const double QuarterCircle = 0.25d;

            /// <summary>
            /// Length of the hour hand (% of radius)
            /// </summary>
            public const double HourlyLength = 0.40d;

            /// <summary>
            /// Max width of the hour hand (adds to angle)
            /// </summary>
            public const double HourlyWidth = 0.010d;

            /// <summary>
            /// Length of the hour hand at max width (% of radius)
            /// </summary>
            public const double HourlyMaxWidthLength = 0.97d;

            /// <summary>
            /// Max width of the minute hand (adds to angle)
            /// </summary>
            public const double MinuteLength = 0.50d;

            /// <summary>
            /// Max width of the minute hand (adds to angle)
            /// </summary>
            public const double MinuteWidth = 0.006d;

            /// <summary>
            /// Length of the minute hand at max width (% of radius)
            /// </summary>
            public const double MinuteMaxWidthLength = 0.90d;

            /// <summary>
            /// Max width of the second hand (adds to angle)
            /// </summary>
            public const double SecondLength = 0.60d;

            /// <summary>
            /// Max width of the second hand (adds to angle)
            /// </summary>
            public const double SecondWidth = 0.003d;

            /// <summary>
            /// Length of the second hand at max width (% of radius)
            /// </summary>
            public const double SecondMaxWidthLength = 1.00d;
        }

        /// <summary>
        /// Structure to hold the arguments passed to DrawHand
        /// </summary>
        private class DrawArgs
        {
            /// <summary>
            /// brush to use for drawing the hand
            /// </summary>
            public SolidBrush Brush;

            /// <summary>
            /// base angle for the hand (between 0 and 1)
            /// </summary>
            public double Angle;

            /// <summary>
            /// length to use for the hand (second &gt; minute &gt ;hour)
            /// </summary>
            public double Length;

            /// <summary>
            /// the length at which the hand reaches it's maxiumum width
            /// </summary>
            public double MaxWidthLength;

            /// <summary>
            /// width of the hand (adds/subtracts to interval)
            /// </summary>
            public double Width;
        }


        /// <summary>
        /// Interface for retrieving the date.
        /// </summary>
        private IDateTimeService _dateTimeService;

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
        /// Constructor. Sets up maths reference and initial colour.
        /// </summary>
        /// <param name="mathService"></param>
        /// <param name="dateTimeService"></param>
        /// <param name="initialColor"></param>
        public Hands (IMathService mathService, IDateTimeService dateTimeService, Color initialColor)
        {
            _mathService = mathService;
            _dateTimeService = dateTimeService;
            _secondBrush = new SolidBrush(initialColor);
            _minuteBrush = new SolidBrush(initialColor);
            _hourlyBrush = new SolidBrush(initialColor);
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

            var now = _dateTimeService.Now;
            var second = (now.Second / 60d);
            var minute = (now.Minute / 60d) + (second / 60d);
            var hourly = (now.Hour / 12d) + (minute / 12d);

            Array.ForEach(new DrawArgs[] {
                new DrawArgs
                {
                    Brush = _secondBrush,
                    Angle = second,
                    Length = Constants.SecondLength,
                    MaxWidthLength = Constants.SecondMaxWidthLength,
                    Width = Constants.SecondWidth
                },
                new DrawArgs
                {
                    Brush = _minuteBrush,
                    Angle = minute,
                    Length = Constants.MinuteLength,
                    MaxWidthLength = Constants.MinuteMaxWidthLength,
                    Width = Constants.MinuteWidth
                },
                new DrawArgs
                {
                    Brush = _hourlyBrush,
                    Angle = hourly,
                    Length = Constants.HourlyLength,
                    MaxWidthLength = Constants.HourlyMaxWidthLength,
                    Width = Constants.HourlyWidth
                }
            }, x =>
                graphics.FillPolygon(x.Brush, new PointF[] {
                    _mathService.GetPoint(x.Angle + Constants.QuarterCircle, Center.CircleSize / 2),
                    _mathService.GetPoint(x.Angle + x.Width, x.Length * x.MaxWidthLength),
                    _mathService.GetPoint(x.Angle, x.Length),
                    _mathService.GetPoint(x.Angle - x.Width, x.Length * x.MaxWidthLength),
                    _mathService.GetPoint(x.Angle - Constants.QuarterCircle, Center.CircleSize / 2)
                })
            );
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
                if (_secondBrush != null) _secondBrush.Dispose();
                if (_minuteBrush != null) _minuteBrush.Dispose();
                if (_hourlyBrush != null) _hourlyBrush.Dispose();
            }
        }
    }
}
