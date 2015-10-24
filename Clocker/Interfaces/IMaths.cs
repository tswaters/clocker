using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clocker.Interfaces
{
    /// <summary>
    /// Interface for Maths.
    /// </summary>
    public interface IMathService
    {
        Rectangle Rectangle { get; set; }
        PointF Center { get; }
        PointF GetPoint(double interval, double length);
    }
}
