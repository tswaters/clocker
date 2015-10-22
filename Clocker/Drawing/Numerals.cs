using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clocker.Drawing
{
    /// <summary>
    /// Responsible for drawing roman numerals on an analog clock.
    /// </summary>
    class Numerals
    {
        /// <summary>
        /// Roman numerals to use (these don't change)
        /// </summary>
        private static string[] numerals = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };

        /// <summary>
        /// Font brush to use for drawing the numerals.
        /// </summary>
        public static SolidBrush fontBrush;

        /// <summary>
        /// Draw 12 roman numerals on the given graphics.
        /// </summary>
        /// <param name="g">Graphics context</param>
        public static void DrawNumerals (Graphics g)
        {
            for (var x = 5; x <= 60; x+= 5)
            {
                var numeral = numerals[(x / 5) - 1];
                var font = new Font("Segui UI", 16);
                var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString(numeral, font, fontBrush, Maths.getPoint(x / 60d, 1.2d), format);
            }
        }

        /// <summary>
        /// Sets up the font brush
        /// </summary>
        /// <param name="fontColor">Color to use for the brush</param>
        public static void Setup (Color fontColor)
        {
            if (fontBrush != null)
            {
                Cleanup();
            }
            fontBrush = new SolidBrush(fontColor);
        }

        /// <summary>
        /// Cleans up resources associated with the class (SolidBrush)
        /// </summary>
        public static void Cleanup()
        {
            fontBrush.Dispose();
            fontBrush = null;
        }

    }
}
