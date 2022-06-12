using Com.Gitusme.Whiteboard.Strokes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Gitusme.Whiteboard
{
    /// <summary>
    /// @see https://c.runoob.com/more/svgeditor/
    /// </summary>
    public class GraphicsDrawable : IDrawable
    {
        public DrawMode Mode { get; set; } = DrawMode.None;

        public Stroke ProgressStroke { get; set; }

        private Stack<Stroke> _strokes { get; set; } = new Stack<Stroke>();

        private Stack<Stroke> _undoStrokes { get; set; } = new Stack<Stroke>();

        public Stroke CreateShape(PointF start, PointF end, Color strokeColor, float strokeSize)
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
                    break;
                case DrawMode.Line:
                    shape = new Line();
                    break;
                case DrawMode.Ellipse:
                    shape = new Ellipse();
                    break;
                case DrawMode.Rectangle:
                    shape = new Rectangle();
                    break;
                case DrawMode.Text:
                    break;
            }
            shape?.Update(start, end, strokeColor, strokeSize);
            return shape;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // 绘制历史Stroke
            foreach (Stroke stroke in _strokes.Reverse())
            {
                stroke.Draw(canvas);
            }
            // 绘制正在操作的Stroke
            if (ProgressStroke != null)
            {
                ProgressStroke.Draw(canvas);
            }
            //Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)   "/data/user/0/com.companyname.mauiapp1/files"
        }

        public void AddStroke(Stroke shape)
        {
            _strokes.Push(shape);
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

        public void Save()
        {

        }

        public void Clear()
        {
            _strokes.Clear();
            _undoStrokes.Clear();
        }

        public enum DrawMode
        {
            None,
            Select,
            Pen,
            Eraser,
            Line,
            Rectangle,
            Ellipse,
            Arrow,
            Text
        }

    }
}
