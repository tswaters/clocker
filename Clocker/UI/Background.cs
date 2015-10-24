using System;
using System.Drawing;
using Clocker.Interfaces;

namespace Clocker.UI
{
    public class Background : IDrawable
    {
        /// <summary>
        /// Color to use for the background.
        /// </summary>
        private Color _color;

        /// <summary>
        /// Constructor. Sets up initial colour.
        /// </summary>
        /// <param name="initialColor"></param>
        public Background (Color initialColor)
        {
            _color = initialColor;
        }

        /// <summary>
        /// Updates the color of the background to something new.
        /// </summary>
        /// <param name="color"></param>
        public Color Color
        {
            set
            {
                _color = value;
            }
        }

        /// <summary>
        /// Clears the background of the graphics context.
        /// </summary>
        /// <param name="graphics">Graphics context</param>
        public void Draw(IGraphicsService graphics)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException("graphics");
            }

            graphics.Clear(_color);
        }

        /// <summary>
        /// Cleans up resources associated with the class (nothing!)
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // nothing to dispose :(
        }
    }
}
