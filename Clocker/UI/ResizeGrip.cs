using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clocker.Interfaces;
using System.Windows.Forms;

namespace Clocker.UI
{
    class ResizeGrip : IDrawable
    {
        /// <summary>
        /// Reference to the math service
        /// </summary>
        private IMathService _mathService;

        /// <summary>
        /// Current colour of the resize grip
        /// </summary>
        private Color _color;

        /// <summary>
        /// Constructor. Sets up the math service.
        /// </summary>
        /// <param name="mathService"></param>
        public ResizeGrip(IMathService mathService)
        {
            _mathService = mathService;
        }

        /// <summary>
        /// Sets a new color to something that contrasts against the provided background.
        /// </summary>
        public Color Color
        {
            set
            {
                _color = (value.ToArgb() & 0x00FFFFFFFF) > (0xffffff / 6) ? Color.Black : Color.White;
            }
        }

        /// <summary>
        /// Disposes of resources (nothing)
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // nothing to see here
        }

        /// <summary>
        /// Draws the resize in a contrasting colour to the provided graphics context.
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(IGraphicsService graphics)
        {
            ControlPaint.DrawSizeGrip(graphics.Graphics, _color, _mathService.ResizeRect);
        }
    }
}
