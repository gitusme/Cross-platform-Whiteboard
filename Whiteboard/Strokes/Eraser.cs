using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Gitusme.Whiteboard.Strokes
{
    public class Eraser : Pen
    {
        public Eraser() : base()
        {
        }

        public override void Update(PointF start, PointF end, Color strokeColor, float strokeSize)
        {
            base.Update(start, end, Colors.White, strokeSize);
        }

    }
}
