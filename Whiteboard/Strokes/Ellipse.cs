using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Gitusme.Whiteboard.Strokes
{
    [Serializable]
    public class Ellipse : Stroke
    {
        public Ellipse()
        {
            Shape = new Microsoft.Maui.Controls.Shapes.Ellipse();
        }

        public override void Draw(ICanvas canvas)
        {
            base.Draw(canvas);
            if(Shape != null)
            {
                canvas.DrawEllipse(
                    (float)Shape.TranslationX, (float)Shape.TranslationY,
                    (float)Shape.WidthRequest, (float)Shape.HeightRequest);
                canvas.FillEllipse(
                    (float)Shape.TranslationX, (float)Shape.TranslationY,
                    (float)Shape.WidthRequest, (float)Shape.HeightRequest);
            }
        }

        public override bool Contains(PointF point)
        {
            var x = point.X;
            var y = point.Y;

            double a = Shape.WidthRequest / 2;
            double b = Shape.HeightRequest / 2;

            return Math.Pow(x, 2) / Math.Pow(a, 2) + Math.Pow(y, 2) / Math.Pow(b, 2) <= 1;
        }
    }
}
