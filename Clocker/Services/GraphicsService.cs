using System;
using System.Drawing;
using Clocker.Interfaces;

namespace Clocker.Services
{
    public class GraphicsService : IGraphicsService
    {
        public Graphics Graphics { get; set; }

        public void FillPolygon(Brush brush, PointF[] points)
        {
            Graphics.FillPolygon(brush, points);
        }

        public void FillEllipse(Brush brush, Rectangle rectangle)
        {
            Graphics.FillEllipse(brush, rectangle);
        }

        public void DrawLine(Pen pen, PointF startPoint, PointF endPoint)
        {
            Graphics.DrawLine(pen, startPoint, endPoint);
        }

        public void DrawString(string text, Font font, Brush fontBrush, PointF position, StringFormat format)
        {
            Graphics.DrawString(text, font, fontBrush, position, format);
        }

        public void Clear(Color color)
        {
            Graphics.Clear(color);
        }

    }
}
