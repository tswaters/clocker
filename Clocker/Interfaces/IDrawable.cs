using System;
using System.Drawing;

namespace Clocker.Interfaces
{
    interface IDrawable : IDisposable
    {
        void Draw(IGraphicsService graphics);
        Color Color { set; }
    }
}
