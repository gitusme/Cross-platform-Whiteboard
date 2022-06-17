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
            }
        }
    }
}
