using System;
using System.Drawing;

namespace Clocker.Drawing
{
    /// <summary>
    /// Responsible for drawing minute/second/hour hands on an analog clock.
    /// </summary>
    class Hands
    {
        /// <summary>
        /// Pen to use for the second hand
        /// </summary>
        private static Pen secondPen;

        /// <summary>
        /// Pen to use for the minute hand
        /// </summary>
        private static Pen minutePen;

        /// <summary>
        /// Pen to use for the hour hand
        /// </summary>
        private static Pen hourlyPen;

        /// <summary>
        /// Draw hands on the clock
        /// </summary>
        /// <param name="g">Graphics context</param>
        public static void DrawHands (Graphics g)
        {
            var now = DateTime.Now;
            var second = (now.Second / 60d);
            var minute = (now.Minute / 60d) + (second / 60d);
            var hourly = (now.Hour / 12d) + (minute / 12d);
            g.DrawLine(secondPen, Maths.center, Maths.getPoint(second, 0.7d));
            g.DrawLine(minutePen, Maths.center, Maths.getPoint(minute, 0.6d));
            g.DrawLine(hourlyPen, Maths.center, Maths.getPoint(hourly, 0.5d));
        }

        /// <summary>
        /// Set up pens with the provided color.
        /// </summary>
        /// <param name="foreColor">color of the hand</param>
        public static void Setup (Color foreColor)
        {
            Cleanup();
            secondPen = new Pen(foreColor, 1);
            minutePen = new Pen(foreColor, 2);
            hourlyPen = new Pen(foreColor, 3);
        }

        /// <summary>
        /// Cleans up resources associated with the pens.
        /// </summary>
        public static void Cleanup ()
        {
            if (secondPen != null) {
                secondPen.Dispose();
                secondPen = null;
            }
            if (minutePen != null) {
                minutePen.Dispose();
                minutePen = null;
            }
            if (hourlyPen != null) {
                hourlyPen.Dispose();
                hourlyPen = null;
            }
        }

    }
}
