using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Gitusme.Whiteboard.Strokes
{
    public class Line : Stroke
    {
        public Line()
        {
            Shape = new Microsoft.Maui.Controls.Shapes.Line();
        }

        public override void Draw(ICanvas canvas)
        {
            base.Draw(canvas);
            if(Shape != null)
            {
                canvas.DrawLine(
                    (float)Shape.TranslationX, (float)Shape.TranslationY,
                    (float)(Shape.WidthRequest + Shape.TranslationX), (float)(Shape.HeightRequest + Shape.TranslationY));
            }
        }
    }
}
