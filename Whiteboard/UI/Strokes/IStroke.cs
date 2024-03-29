﻿using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Gitusme.Whiteboard.Strokes
{
    public interface IStroke
    {
        public abstract void Update(PointF start, PointF end, Color strokeColor, Color fillColor, double strokeSize);
        public abstract void Draw(ICanvas canvas);
        public abstract bool Contains(PointF point);
    }
}
