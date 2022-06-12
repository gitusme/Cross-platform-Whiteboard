
using Com.Gitusme.Whiteboard.Strokes;

namespace Com.Gitusme.Whiteboard;

public partial class MainPage : ContentPage
{
    private Dictionary<ImageButton, GraphicsDrawable.DrawMode> _strokeModes = new Dictionary<ImageButton, GraphicsDrawable.DrawMode>();
    private Dictionary<ImageButton, Color> _strokeColors = new Dictionary<ImageButton, Color>();
    private Dictionary<ImageButton, float> _strokeSizes = new Dictionary<ImageButton, float>();

    private GraphicsDrawable.DrawMode _strokeMode;
    private Color _strokeColor;
    private float _strokeSize;

    public MainPage()
	{
		InitializeComponent();

        InitToolBar();

        GraphicsView.StartHoverInteraction += GraphicsView_StartHoverInteraction;
        GraphicsView.MoveHoverInteraction += GraphicsView_MoveHoverInteraction;
        GraphicsView.EndHoverInteraction += GraphicsView_EndHoverInteraction;
        GraphicsView.StartInteraction += GraphicsView_StartInteraction;
        GraphicsView.DragInteraction += GraphicsView_DragInteraction;
        GraphicsView.EndInteraction += GraphicsView_EndInteraction;
        GraphicsView.CancelInteraction += GraphicsView_CancelInteraction;

        Loaded += MainPage_Loaded;
    }

    private void InitToolBar()
    {
        // 初始化工具/形状
        _strokeModes.Add(pen, GraphicsDrawable.DrawMode.Pen);
        _strokeModes.Add(eraser, GraphicsDrawable.DrawMode.Eraser);
        _strokeModes.Add(line, GraphicsDrawable.DrawMode.Line);
        _strokeModes.Add(ellipse, GraphicsDrawable.DrawMode.Ellipse);
        _strokeModes.Add(rectangle, GraphicsDrawable.DrawMode.Rectangle);
        // 初始画笔颜色
        _strokeColors.Add(strokeColorBlack, Color.Parse("#010101"));
        _strokeColors.Add(strokeColorRed, Color.Parse("#E63564"));
        _strokeColors.Add(strokeColorBlue, Color.Parse("#2CB0FF"));
        _strokeColors.Add(strokeColorYellow, Color.Parse("#F4A124"));
        _strokeColors.Add(strokeColorGreen, Color.Parse("#5EB670"));
        // 初始画笔粗细
        _strokeSizes.Add(strokeSizeSmall, 1.0F);
        _strokeSizes.Add(strokeSize, 2.0F);
        _strokeSizes.Add(strokeSizeLarge, 6.0F);

        // 设置默认状态
        ImageButton defautMode = pen;
        ImageButton defautColor = strokeColorBlack;
        ImageButton defautSize = strokeSize;

        SetButtonStatus(new List<ImageButton>(_strokeModes.Keys), defautMode);
        SetButtonStatus(new List<ImageButton>(_strokeColors.Keys), defautColor);
        SetButtonStatus(new List<ImageButton>(_strokeSizes.Keys), defautSize);

        _strokeMode = _strokeModes[defautMode];
        _strokeColor = _strokeColors[defautColor];
        _strokeSize = _strokeSizes[defautSize];

        SetDrawMode(_strokeMode);
        SetStrokeColor(_strokeColor);
        SetStrokeSize(_strokeSize);
    }

    private void MainPage_Loaded(object sender, EventArgs e)
    {
        //Thread th = new Thread(() =>
        //{
        //    int i = 0;
        //    while (i++<10)
        //    {
        //        Dispatcher.Dispatch(() =>
        //        {
        //            ScreenshotImage.Source = TakeScreenshotAsync().GetAwaiter().GetResult();
        //        });
        //        Thread.Sleep(500);
        //    }
        //});
        //th.Start();
    }

    private void GraphicsView_StartHoverInteraction(object sender, TouchEventArgs e)
    {
        
    }

    private void GraphicsView_MoveHoverInteraction(object sender, TouchEventArgs e)
    {
    }

    private void GraphicsView_EndHoverInteraction(object sender, EventArgs e)
    {
    }

    private void GraphicsView_StartInteraction(object sender, TouchEventArgs e)
    {
        var points = e.Touches;

        PointF start = points.FirstOrDefault();

        GraphicsDrawable.ProgressStroke =
            GraphicsDrawable.CreateShape(start, start, _strokeColor, _strokeSize);

        GraphicsView.Invalidate();
    }

    private void GraphicsView_DragInteraction(object sender, TouchEventArgs e)
    {
        var points = e.Touches;

        if(GraphicsDrawable.ProgressStroke != null)
        {
            PointF end = points.FirstOrDefault();

            Stroke stroke = GraphicsDrawable.ProgressStroke;
            stroke.Update(
                new PointF((float)stroke.Shape.TranslationX, (float)stroke.Shape.TranslationY), end,
                (stroke.Shape.Stroke as SolidColorBrush).Color, (float)stroke.Shape.StrokeThickness);
        }

        GraphicsView.Invalidate();
    }

    private void GraphicsView_EndInteraction(object sender, TouchEventArgs e)
    {
        var points = e.Touches;

        if (GraphicsDrawable.ProgressStroke != null)
        {
            PointF end = points.FirstOrDefault();

            Stroke stroke = GraphicsDrawable.ProgressStroke;
            stroke.Update(
                new PointF((float)stroke.Shape.TranslationX, (float)stroke.Shape.TranslationY), end,
                _strokeColor, _strokeSize);

            GraphicsDrawable.AddStroke(stroke);
            GraphicsDrawable.ProgressStroke = null;
        }

        GraphicsView.Invalidate();
    }

    private void GraphicsView_CancelInteraction(object sender, EventArgs e)
    {
        GraphicsDrawable.RemoveStroke();
        GraphicsDrawable.ProgressStroke = null;
    }

    public async Task<ImageSource> TakeScreenshotAsync()
    {
        if (Screenshot.Default.IsCaptureSupported)
        {
            IScreenshotResult screen = await Screenshot.Default.CaptureAsync();

            Stream stream = await screen.OpenReadAsync();

            ImageSource imageSource = ImageSource.FromStream(() => stream);
            return imageSource;
        }

        return null;
    }


    private void save_Clicked(object sender, EventArgs e)
    {
        GraphicsDrawable.Save();
    }

    private void clear_Clicked(object sender, EventArgs e)
    {
        GraphicsDrawable.Clear();
        GraphicsView.Invalidate();
    }

    private void undo_Clicked(object sender, EventArgs e)
    {
        GraphicsDrawable.Undo();
        GraphicsView.Invalidate();
    }

    private void redo_Clicked(object sender, EventArgs e)
    {
        GraphicsDrawable.Redo();
        GraphicsView.Invalidate();
    }

    private void strokeMode_Clicked(object sender, EventArgs e)
    {
        ImageButton button = sender as ImageButton;
        if (button != null)
        {
            SetButtonStatus(new List<ImageButton>(_strokeModes.Keys), button);
            SetDrawMode(_strokeModes[button]);
        }
    }

    private void strokeSize_Clicked(object sender, EventArgs e)
    {
        ImageButton button = sender as ImageButton;
        if (button != null)
        {
            SetButtonStatus(new List<ImageButton>(_strokeSizes.Keys), button);
            SetStrokeSize(_strokeSizes[button]);
        }
    }

    private void strokeColor_Clicked(object sender, EventArgs e)
    {
        ImageButton button = sender as ImageButton;
        if (button != null)
        {
            SetButtonStatus(new List<ImageButton>(_strokeColors.Keys), button);
            SetStrokeColor(_strokeColors[button]);
        }
    }

    private void SetButtonStatus(List<ImageButton> buttonGroup, ImageButton clickButton)
    {
        foreach (ImageButton btn in buttonGroup)
        {
            btn.BackgroundColor = Object.ReferenceEquals(btn, clickButton) ? Color.Parse("#eeeeee") : Colors.Transparent;
        }
    }
    private void SetDrawMode(GraphicsDrawable.DrawMode drawMode)
    {
        GraphicsDrawable.Mode = drawMode;
    }

    private void SetStrokeColor(Color strokeColor)
    {
        _strokeColor = strokeColor;
    }

    private void SetStrokeSize(float strokeSize)
    {
        _strokeSize = strokeSize;
    }
}

