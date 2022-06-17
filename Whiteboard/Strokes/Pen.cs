using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Gitusme.Whiteboard.Strokes
{
    [Serializable]
    public class Pen : Stroke
    {
        private Color _color;

        public Pen()
        {
            Shape = new Microsoft.Maui.Controls.Shapes.Path();
        }

        public Pen(Color color) : this()
        {
            this._color = color;
        }

        public override void Update(PointF start, PointF end, Color strokeColor, float strokeSize)
        {
            base.Update(start, end, strokeColor, strokeSize);
            Microsoft.Maui.Controls.Shapes.Path path = Shape as Microsoft.Maui.Controls.Shapes.Path;
            if (path != null)
            {
                if (path.Data == null)
                {
                    path.Data = new Microsoft.Maui.Controls.Shapes.PathGeometry();
                    Microsoft.Maui.Controls.Shapes.PathFigure pathFigure = new Microsoft.Maui.Controls.Shapes.PathFigure();
                    pathFigure.StartPoint = start;
                    (path.Data as Microsoft.Maui.Controls.Shapes.PathGeometry)?.Figures.Add(pathFigure);
                }
                (path.Data as Microsoft.Maui.Controls.Shapes.PathGeometry)?.Figures[0].Segments.Add(new Microsoft.Maui.Controls.Shapes.LineSegment(end));
                path.Stroke = new SolidColorBrush(strokeColor);
                path.StrokeThickness = strokeSize;
                path.StrokeDashArray = new float[0];
                path.StrokeDashOffset = 0;
            }
        }

        public override void Draw(ICanvas canvas)
        {
            base.Draw(canvas);
            if (Shape != null)
            {
                canvas.DrawPath(Shape.GetPath());
            }
        }
    }
}
