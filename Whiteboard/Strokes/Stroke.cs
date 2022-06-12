using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Gitusme.Whiteboard.Strokes
{
    public abstract class Stroke : IStroke
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public Shape Shape { get; set; }

        public virtual void Update(PointF start, PointF end, Color strokeColor, float strokeSize)
        {
            if (Shape != null)
            {
                Shape.Stroke = new SolidColorBrush(strokeColor);
                Shape.StrokeThickness = strokeSize;
                Shape.StrokeDashArray = new float[0];
                Shape.StrokeDashOffset = 0;
                Shape.TranslationX = start.X;
                Shape.TranslationY = start.Y;
                Shape.WidthRequest = end.X - start.X;
                Shape.HeightRequest = end.Y - start.Y;
                Shape.Shadow = new Shadow()
                {
                    Brush = new SolidColorBrush(Colors.Gray),
                    Opacity = 0.0F,
                    Offset = new Point(10, 10),
                    Radius = 0
                };
            }
        }

        public virtual void Draw(ICanvas canvas)
        {
            if (canvas != null)
            {
                canvas.StrokeColor = (Shape.Stroke as SolidColorBrush).Color;
                canvas.StrokeSize = (float)Shape.StrokeThickness;
                canvas.StrokeDashPattern = Shape.StrokeDashPattern;
                canvas.StrokeDashOffset = (float)Shape.StrokeDashOffset;
                canvas.SetShadow(
                    new SizeF((float)Shape.Shadow.Offset.X, (float)Shape.Shadow.Offset.Y),
                    Shape.Shadow.Radius,
                    (Shape.Shadow.Brush as SolidColorBrush).Color);
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as Stroke;
            if (other != null && String.Equals(other.Id, this.Id))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id);
        }
    }
}
