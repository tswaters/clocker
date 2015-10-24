using System;
using System.Drawing;
using Clocker.Interfaces;

namespace Clocker.UI
{
    /// <summary>
    /// Responsible for drawing roman numerals on an analog clock.
    /// </summary>
    public class Numerals : IDrawable
    {
        /// <summary>
        /// Position of the numerals, % of full radius.
        /// </summary>
        public const double NumeralsLength = 0.9d;

        /// <summary>
        /// Roman numerals to use (these don't change)
        /// </summary>
        private string[] numerals = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };

        /// <summary>
        /// Math service reference
        /// </summary>
        private IMathService _mathService;

        /// <summary>
        /// Font brush to use for drawing the numerals.
        /// </summary>
        private SolidBrush _fontBrush;

        /// <summary>
        /// Font to use for drawing the numerals.
        /// </summary>
        private Font _font;

        /// <summary>
        /// Font format to use for drawing the numerals.
        /// </summary>
        private StringFormat _stringFormat;

        /// <summary>
        /// Constructor initializes the math object and initial font color.
        /// </summary>
        /// <param name="mathService"></param>
        /// <param name="initialColor"></param>
        public Numerals(IMathService mathService, Color initialColor)
        {
            _mathService = mathService;
            _fontBrush = new SolidBrush(initialColor);
            _font = new Font("Segoe UI", 16);
            _stringFormat = new StringFormat();
            _stringFormat.Alignment = StringAlignment.Center;
            _stringFormat.LineAlignment = StringAlignment.Center;
        }

        /// <summary>
        /// Sets the color of the numerals.
        /// </summary>
        /// <param name="color"></param>
        public Color Color
        {
            set
            {
                _fontBrush.Color = value;
            }
        }

        /// <summary>
        /// Draw 12 roman numerals on the given graphics.
        /// </summary>
        /// <param name="graphics">Graphics context</param>
        public void Draw (IGraphicsService graphics)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException("graphics");
            }
            for (var x = 5; x <= 60; x+= 5)
            {
                var numeral = numerals[(x / 5) - 1];
                graphics.DrawString(numeral, _font, _fontBrush, _mathService.GetPoint(x / 60d, NumeralsLength), _stringFormat);
            }
        }

        /// <summary>
        /// Cleans up resources associated with the class (SolidBrush)
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) {
                if (_fontBrush != null)
                {
                    _fontBrush.Dispose();
                    _fontBrush = null;
                }
                if (_font != null)
                {
                    _font.Dispose();
                    _fontBrush = null;
                }
                if (_stringFormat != null)
                {
                    _stringFormat.Dispose();
                    _stringFormat = null;
                }
            }
        }
    }
}
