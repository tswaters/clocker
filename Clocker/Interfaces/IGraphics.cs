using System.Drawing;

namespace Clocker.Interfaces
{
    public interface IGraphicsService
    {
        Graphics Graphics { get; set; }
        void FillEllipse(Brush brush, Rectangle rectangle);
        void FillPolygon(Brush brush, PointF[] points);
        void DrawLine(Pen pen, PointF startPoint, PointF endPoint);
        void DrawString(string text, Font font, Brush fontBrush, PointF position, StringFormat format);
        void Clear(Color color);
    }
}
