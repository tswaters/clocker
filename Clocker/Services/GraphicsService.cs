using System;
using System.Drawing;
using Clocker.Interfaces;

namespace Clocker.Services
{
    public class GraphicsService : IGraphicsService
    {
        private Graphics _graphics;

        public GraphicsService(Graphics graphics)
        {
            _graphics = graphics;
        }

        public void FillEllipse(Brush brush, Rectangle rectangle)
        {
            Graphics.FillEllipse(brush, rectangle);
        }

        public void DrawLine(Pen pen, PointF startPoint, PointF endPoint)
        {
            _graphics.DrawLine(pen, startPoint, endPoint);
        }

        public void DrawString(string text, Font font, Brush fontBrush, PointF position, StringFormat format)
        {
            _graphics.DrawString(text, font, fontBrush, position, format);
        }

        public void Clear(Color color)
        {
            _graphics.Clear(color);
        }

    }
}
