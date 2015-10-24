using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clocker.Interfaces;

namespace Clocker.Services
{
    /// <summary>
    /// Mathematical functions for processing the analog clock.
    /// </summary>
    public class MathService : IMathService
    {

        /// <summary>
        /// Constructor for math calculations.
        /// </summary>
        /// <param name="box">ClientRectangle of form</param>
        public MathService(Rectangle box)
        {
            Rectangle = box;
        }

        /// <summary>
        /// Box that contains the clock.
        /// </summary>
        public Rectangle Rectangle { get; set; }

        /// <summary>
        /// Center of the clock.
        /// </summary>
        public PointF Center {
            get
            {
                return new PointF(Rectangle.Width / 2, Rectangle.Height / 2);
            }
        }

        /// <summary>
        /// Radius of the clock.
        /// </summary>
        public double Radius
        {
            get
            {
                return (Center.X < Center.Y ? Center.X : Center.Y);
            }
        }

        /// <summary>
        /// Retrieve a point based on interval and length.
        /// </summary>
        /// <param name="interval">value between 0 and 1</param>
        /// <param name="length">length from the center.</param>
        /// <returns>PointF</returns>
        public PointF GetPoint(double interval, double length)
        {
            var value = (Math.PI * 2d * interval) - (Math.PI / 2d);
            var targetX = Center.X + (Math.Cos(value) * Radius * length);
            var targetY = Center.Y + (Math.Sin(value) * Radius * length);
            return new PointF((float)targetX, (float)targetY);
        }
    }
}
