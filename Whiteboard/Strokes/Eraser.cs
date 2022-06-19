using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Gitusme.Whiteboard.Strokes
{
    [Serializable]
    public class Eraser : Pen
    {
        public Eraser() : base()
        {
        }

        public override void Update(PointF start, PointF end, Color strokeColor, Color fillColor, double strokeSize)
        {
            base.Update(start, end, strokeColor, Colors.Transparent, strokeSize);
        }

    }
}
