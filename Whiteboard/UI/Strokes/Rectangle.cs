﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Gitusme.Whiteboard.Strokes
{
    [Serializable]
    public class Rectangle : Stroke
    {
        public Rectangle()
        {
            Shape = new Microsoft.Maui.Controls.Shapes.Rectangle();
        }

        public override void Draw(ICanvas canvas)
        {
            base.Draw(canvas);
            if (Shape != null)
            {
                canvas.DrawRectangle(
                    (float)Shape.TranslationX, (float)Shape.TranslationY,
                    (float)Shape.WidthRequest, (float)Shape.HeightRequest);
                canvas.FillRectangle(
                    (float)Shape.TranslationX, (float)Shape.TranslationY,
                    (float)Shape.WidthRequest, (float)Shape.HeightRequest);
            }
        }

        public override bool Contains(PointF point)
        {
            var x = point.X;
            var y = point.Y;
            return (x >= Shape.TranslationX && x <= Shape.TranslationX + Shape.WidthRequest) &&
                (y >= Shape.TranslationY && y <= Shape.TranslationY + Shape.HeightRequest);
        }
    }
}
