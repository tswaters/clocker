using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clocker.Drawing
{
    /// <summary>
    /// Mathematical functions for processing the analog clock.
    /// </summary>
    class Maths
    {
        /// <summary>
        /// Radius of the clock (set by form height/width)
        /// </summary>
        public static double radius;

        /// <summary>
        /// Center of the clock (set by form height/width)
        /// </summary>
        public static PointF center;

        /// <summary>
        /// Retrieve a point based on interval and length.
        /// </summary>
        /// <param name="interval">value between 0 and 1</param>
        /// <param name="length">length from the center.</param>
        /// <returns>PointF</returns>
        public static PointF getPoint(double interval, double length)
        {
            var value = (Math.PI * 2d * interval) - (Math.PI / 2d);
            var targetX = center.X + (Math.Cos(value) * radius * length);
            var targetY = center.Y + (Math.Sin(value) * radius * length);
            return new PointF((float)targetX, (float)targetY);
        }
    }
}
