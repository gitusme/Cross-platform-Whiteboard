using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Gitusme.Whiteboard.Strokes
{
    [Serializable]
    public class Picture : Stroke
    {
        public Picture()
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
            }
        }
    }
}
