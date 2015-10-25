using System.Drawing;

namespace Clocker.Interfaces
{
    public interface IGraphicsService
    {
        void FillEllipse(Brush brush, Rectangle rectangle);
        void DrawLine(Pen pen, PointF startPoint, PointF endPoint);
        void DrawString(string text, Font font, Brush fontBrush, PointF position, StringFormat format);
        void Clear(Color color);
    }
}
