using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Clocker.Drawing
{
    /// <summary>
    /// Responsible for drawing 60 ticks on an analog clock.
    /// </summary>
    class Tick
    {
        /// <summary>
        /// Pen to use for the ticks
        /// </summary>
        private static Pen tickPen;

        /// <summary>
        /// Draws ticks on an analog clock.
        /// </summary>
        /// <param name="g">Graphics context</param>
        public static void DrawTicks(Graphics g)
        {
            for (var x = 1; x <= 60; x++)
            {
                var begPoint = Maths.getPoint(x / 60d, x % 5 == 0 ? 0.8 : 0.9);
                var endPoint = Maths.getPoint(x / 60d, 1.0);
                g.DrawLine(tickPen, begPoint, endPoint);
            }
        }

        /// <summary>
        /// Set up the pen with the provided color.
        /// </summary>
        /// <param name="foreColor">color of the hand</param>
        public static void Setup(Color foreColor)
        {
            Cleanup();
            tickPen = new Pen(foreColor, 1);
        }

        /// <summary>
        /// Cleans up resources associated with the pen.
        /// </summary>
        public static void Cleanup()
        {
            if (tickPen != null)
            {
                tickPen.Dispose();
                tickPen = null;
            }
        }

    }
}
