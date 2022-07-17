using Com.Gitusme.Whiteboard.Platforms;
using Com.Gitusme.Whiteboard.Strokes;
using Com.Gitusme.Whiteboard.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Com.Gitusme.Whiteboard
{
    /// <summary>
    /// @see https://c.runoob.com/more/svgeditor/
    /// </summary>
    public class GraphicsDrawable : IDrawable
    {
        public Color CanvasColor { get; set; } = Colors.White;

        public DrawMode Mode { get; set; } = DrawMode.None;

        public Stroke ProgressStroke { get; set; }

        private Stack<Stroke> _strokes { get; set; } = new Stack<Stroke>();

        private Stack<Stroke> _undoStrokes { get; set; } = new Stack<Stroke>();

        public Stroke CreateShape(PointF start, PointF end, Color strokeColor, Color fillColor, float strokeSize)
        {
            Stroke shape = null;
            switch (Mode)
            {
                case DrawMode.Select:
                    break;
                case DrawMode.Pen:
                    shape = new Pen();
                    break;
                case DrawMode.Eraser:
                    shape = new Eraser();
                    strokeColor = CanvasColor == Colors.Black ? Colors.Black : Colors.White;
                    strokeSize = 2 * strokeSize;
                    break;
                case DrawMode.Text:
                    shape = new Text();
                    break;
                case DrawMode.Fill:
                    shape = _strokes.ToList().Find((it) => {
                        return it.Contains(start);
                    });
                    break;
                case DrawMode.Line:
                    shape = new Line();
                    break;
                case DrawMode.Ellipse:
                    shape = new Ellipse();
                    break;
                case DrawMode.Star:
                    shape = new Star();
                    break;
                case DrawMode.Rectangle:
                    shape = new Rectangle();
                    break;
            }
            shape?.Update(start, end, strokeColor, fillColor, strokeSize);
            return shape;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // 绘制背景
            canvas.StrokeSize = 0;
            canvas.FillColor = CanvasColor;
            canvas.FillRectangle(dirtyRect);
            // 绘制历史Stroke
            foreach (Stroke stroke in _strokes.Reverse())
            {
                if (stroke is Eraser)
                {
                    stroke.Shape.Stroke = CanvasColor == Colors.Black ? Colors.Black : Colors.White;
                }
                stroke.Draw(canvas);
            }
            // 绘制正在操作的Stroke
            if (ProgressStroke != null)
            {
                ProgressStroke.Draw(canvas);
            }
        }

        public void AddStroke(Stroke shape)
        {
            if (!_strokes.Contains(shape))
            {
                _strokes.Push(shape);
            }
        }

        public Stroke RemoveStroke()
        {
            return _strokes.Count > 0 ? _strokes.Pop() : null;
        }

        public void Undo()
        {
            Stroke stroke = RemoveStroke();
            if(stroke != null)
            {
                _undoStrokes.Push(stroke);
            }
        }

        public void Redo()
        {
            Stroke stroke = _undoStrokes.Count > 0 ? _undoStrokes.Pop() : null;
            if (stroke != null)
            {
                AddStroke(stroke);
            }
        }

        public void Save(string fileName, Task<IScreenshotResult> screenshot)
        {
            GraphicsWriter writer = new GraphicsWriter(fileName);
            writer.WriteToPictrue(screenshot);
            writer.WriteToXml(_strokes.ToArray());
        }

        public void Clear()
        {
            _strokes.Clear();
            _undoStrokes.Clear();
        }

        public enum DrawMode
        {
            None,
            Arrow,
            Select,
            Pen,
            Eraser,
            Text,
            Fill,
            Line,
            Rectangle,
            Ellipse,
            Star
        }

    }
}
