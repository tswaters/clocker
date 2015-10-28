using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clocker.Interfaces;

namespace Clocker.UI
{
    public sealed class Center : IDrawable
    {
        /// <summary>
        /// Size of the circle at the center of the clock (% of radius)
        /// </summary>
        public const double CircleSize = 0.03d;

        /// <summary>
        /// Reference to the math service
        /// </summary>
        private IMathService _mathService;

        /// <summary>
        /// Brush to use to draw the cirlce.
        /// </summary>
        private SolidBrush _brush;

        /// <summary>
        /// Constructor. Sets up the math service and initial color.
        /// </summary>
        /// <param name="mathService"></param>
        /// <param name="initialColor"></param>
        public Center(IMathService mathService, Color initialColor)
        {
            _mathService = mathService;
            _brush = new SolidBrush(initialColor);
        }

        /// <summary>
        /// Sets the color of the brush to a new value.
        /// </summary>
        public Color Color
        {
            set
            {
                _brush.Color = value;
            }
        }

        /// <summary>
        /// Draws the circle on the specified graphics context
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(IGraphicsService graphics)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException("graphics");
            }
            graphics.FillEllipse(_brush, _mathService.GetCenterRect(CircleSize));
        }

        /// <summary>
        /// Disposes resources.
        /// </summary>
        public void Dispose()
        {
            if (_brush != null) _brush.Dispose();
        }
    }
}
