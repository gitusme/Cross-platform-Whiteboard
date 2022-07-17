using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Com.Gitusme.Whiteboard.Strokes
{
    [Serializable]
    [XmlInclude(typeof(Pen)), XmlInclude(typeof(Eraser)), XmlInclude(typeof(Line)), XmlInclude(typeof(Rectangle)), XmlInclude(typeof(Ellipse))]
    public abstract class Stroke : IStroke
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [XmlIgnore]
        public Shape Shape { get; set; }

        [XmlAttribute(AttributeName = "StrokeColor")]
        public String StrokeColorString
        {
            get
            {
                Color color = (Shape.Stroke as SolidColorBrush).Color;
                return color != Colors.Transparent ? color.ToHex() : String.Empty;
            }
            set
            {
                Color color = !String.IsNullOrEmpty(value) ? Color.Parse(value) : Colors.Transparent;
                Shape.Stroke = new SolidColorBrush(color);
            }
        }

        [XmlIgnore]
        public Color StrokeColor
        {
            get => !String.IsNullOrEmpty(StrokeColorString) ? Color.Parse(StrokeColorString) : Colors.Transparent;
        }

        [XmlAttribute(AttributeName = "FillColor")]
        public String FillColorString
        {
            get
            {
                Color color = (Shape.Fill as SolidColorBrush).Color;
                return color != Colors.Transparent ? color.ToHex() : String.Empty;
            }
            set
            {
                Color color = !String.IsNullOrEmpty(value) ? Color.Parse(value) : Colors.Transparent;
                Shape.Fill = new SolidColorBrush(color);
            }
        }

        [XmlIgnore]
        public Color FillColor
        {
            get => !String.IsNullOrEmpty(FillColorString) ? Color.Parse(FillColorString) : Colors.Transparent;
        }

        [XmlAttribute(AttributeName = "StrokeThickness")]
        public double StrokeThickness { get => Shape.StrokeThickness; set => Shape.StrokeThickness = value; }

        [XmlAttribute(AttributeName = "StrokeDashArray")]
        public double[] StrokeDashArray { get => Shape.StrokeDashArray.ToArray(); set => Shape.StrokeDashArray = value; }

        [XmlAttribute(AttributeName = "StrokeDashOffset")]
        public double StrokeDashOffset { get => Shape.StrokeDashOffset; set => Shape.StrokeDashOffset = value; }

        [XmlAttribute(AttributeName = "TranslationX")]
        public double TranslationX { get => Shape.TranslationX; set => Shape.TranslationX = value; }

        [XmlAttribute(AttributeName = "TranslationY")]
        public double TranslationY { get => Shape.TranslationY; set => Shape.TranslationY = value; }

        [XmlAttribute(AttributeName = "WidthRequest")]
        public double WidthRequest { get => Shape.WidthRequest; set => Shape.WidthRequest = value; }

        [XmlAttribute(AttributeName = "HeightRequest")]
        public double HeightRequest { get => Shape.HeightRequest; set => Shape.HeightRequest = value; }

        public virtual void Update(PointF start, PointF end, Color strokeColor, Color fillColor, double strokeSize)
        {
            if (Shape != null)
            {
                Shape.Stroke = new SolidColorBrush(strokeColor);
                Shape.StrokeThickness = strokeSize;
                Shape.StrokeDashArray = new double[0];
                Shape.StrokeDashOffset = 0;
                Shape.Fill = new SolidColorBrush(fillColor);
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
                canvas.FillColor = (Shape.Fill as SolidColorBrush).Color;
                canvas.SetShadow(
                    new SizeF((float)Shape.Shadow.Offset.X, (float)Shape.Shadow.Offset.Y),
                    Shape.Shadow.Radius,
                    (Shape.Shadow.Brush as SolidColorBrush).Color);
            }
        }

        public virtual bool Contains(PointF point)
        {
            return false;
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
