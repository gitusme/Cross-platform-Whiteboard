
using Com.Gitusme.Whiteboard.Strokes;

namespace Com.Gitusme.Whiteboard;

public partial class MainPage : ContentPage
{
    private readonly Color _buttonSelectedColor = Color.Parse("#E6DFAF");

    private readonly Color _whiteColor = Colors.White;
    private readonly Color _blackColor = Colors.Black;
    private readonly Color _redColor = Color.Parse("#E63564");
    private readonly Color _blueColor = Color.Parse("#2CB0FF");
    private readonly Color _yellowColor = Color.Parse("#F4A124");
    private readonly Color _greenColor = Color.Parse("#5EB670");

    private Dictionary<ImageButton, GraphicsDrawable.DrawMode> _strokeModes = new Dictionary<ImageButton, GraphicsDrawable.DrawMode>();
    private Dictionary<ImageButton, Color> _strokeColors = new Dictionary<ImageButton, Color>();
    private Dictionary<ImageButton, float> _strokeSizes = new Dictionary<ImageButton, float>();
    private Dictionary<ImageButton, Color> _fillColors = new Dictionary<ImageButton, Color>();
    private Dictionary<ImageButton, Color> _backColors = new Dictionary<ImageButton, Color>();

    private GraphicsDrawable.DrawMode _strokeMode;
    private Color _strokeColor;
    private float _strokeSize;
    private Color _fillColor;
    private Color _backColor;

    private string _fileName;

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
        _strokeColors.Add(strokeColorTransparent, Colors.Transparent);
        _strokeColors.Add(strokeColorWhite, _whiteColor);
        _strokeColors.Add(strokeColorBlack, _blackColor);
        _strokeColors.Add(strokeColorRed, _redColor);
        _strokeColors.Add(strokeColorBlue, _blueColor);
        _strokeColors.Add(strokeColorYellow, _yellowColor);
        _strokeColors.Add(strokeColorGreen, _greenColor);
        // 初始画笔粗细
        _strokeSizes.Add(strokeSizeSmall, 1.0F);
        _strokeSizes.Add(strokeSize, 2.0F);
        _strokeSizes.Add(strokeSizeLarge, 6.0F);
        // 初始填充颜色
        _fillColors.Add(fillColorTransparent, Colors.Transparent);
        _fillColors.Add(fillColorWhite, _whiteColor);
        _fillColors.Add(fillColorBlack, _blackColor);
        _fillColors.Add(fillColorRed, _redColor);
        _fillColors.Add(fillColorBlue, _blueColor);
        _fillColors.Add(fillColorYellow, _yellowColor);
        _fillColors.Add(fillColorGreen, _greenColor);
        // 初始化背景色
        _backColors.Add(backColorWhite, _whiteColor);
        _backColors.Add(backColorBlack, _blackColor);

        // 设置默认状态
        ImageButton defautMode = pen;
        ImageButton defautColor = strokeColorBlack;
        ImageButton defautSize = strokeSize;
        ImageButton defaultFillColor = fillColorTransparent;
        ImageButton defaultBackColor = backColorWhite;

        SetButtonStatus(new List<ImageButton>(_strokeModes.Keys), defautMode);
        SetButtonStatus(new List<ImageButton>(_strokeColors.Keys), defautColor);
        SetButtonStatus(new List<ImageButton>(_strokeSizes.Keys), defautSize);
        SetButtonStatus(new List<ImageButton>(_fillColors.Keys), defaultFillColor);
        SetButtonStatus(new List<ImageButton>(_backColors.Keys), defaultBackColor);

        _strokeMode = _strokeModes[defautMode];
        _strokeColor = _strokeColors[defautColor];
        _strokeSize = _strokeSizes[defautSize];
        _fillColor = _fillColors[defaultFillColor];
        _backColor = _backColors[defaultBackColor];

        SetDrawMode(_strokeMode);
        SetStrokeColor(_strokeColor);
        SetStrokeSize(_strokeSize);
        SetFillColor(_fillColor);
        SetBackColor(_backColor);
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

        Stroke stroke = GraphicsDrawable.CreateShape(start, start, _strokeColor, _fillColor, _strokeSize);

        if (GraphicsDrawable.Mode != GraphicsDrawable.DrawMode.Fill)
        {
            GraphicsDrawable.ProgressStroke = stroke;
        }
        else
        {
            stroke?.Update(
                new PointF((float)stroke.TranslationX, (float)stroke.TranslationY),
                new PointF((float)(stroke.TranslationX + stroke.WidthRequest), (float)(stroke.TranslationY + stroke.HeightRequest)),
                stroke.StrokeColor, _fillColor, stroke.StrokeThickness);

            GraphicsDrawable.ProgressStroke = null;
        }

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
                new PointF((float)stroke.TranslationX, (float)stroke.TranslationY), end,
                stroke.StrokeColor, stroke.FillColor, stroke.StrokeThickness);
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
                stroke.StrokeColor, stroke.FillColor, stroke.StrokeThickness);

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


    private async void save_Clicked(object sender, EventArgs e)
    {
        ImageButton button = sender as ImageButton;
        SetButtonStatus(button, _buttonSelectedColor);

        string fileName = String.IsNullOrEmpty(_fileName) ? await DisplayPromptAsync("保存", "请输入文件名:") : _fileName;
        if (!String.IsNullOrEmpty(fileName))
        {
            GraphicsDrawable.Save(fileName, GraphicsView.CaptureAsync());
        }
        _fileName = fileName;
    }

    private void save_Released(object sender, EventArgs e)
    {
        ImageButton button = sender as ImageButton;
        SetButtonStatus(button, Colors.Transparent);
    }

    private void clear_Clicked(object sender, EventArgs e)
    {
        ImageButton button = sender as ImageButton;
        SetButtonStatus(button, _buttonSelectedColor);

        GraphicsDrawable.Clear();
        GraphicsView.Invalidate();
        _fileName = null;
    }

    private void clear_Released(object sender, EventArgs e)
    {
        ImageButton button = sender as ImageButton;
        SetButtonStatus(button, Colors.Transparent);
    }

    private void undo_Clicked(object sender, EventArgs e)
    {
        ImageButton button = sender as ImageButton;
        SetButtonStatus(button, _buttonSelectedColor);

        GraphicsDrawable.Undo();
        GraphicsView.Invalidate();
    }

    private void undo_Released(object sender, EventArgs e)
    {
        ImageButton button = sender as ImageButton;
        SetButtonStatus(button, Colors.Transparent);
    }

    private void redo_Clicked(object sender, EventArgs e)
    {
        ImageButton button = sender as ImageButton;
        SetButtonStatus(button, _buttonSelectedColor);

        GraphicsDrawable.Redo();
        GraphicsView.Invalidate();
    }

    private void redo_Released(object sender, EventArgs e)
    {
        ImageButton button = sender as ImageButton;
        SetButtonStatus(button, Colors.Transparent);
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

    private void fillColor_Clicked(object sender, EventArgs e)
    {
        ImageButton button = sender as ImageButton;
        if (button != null)
        {
            SetButtonStatus(new List<ImageButton>(_fillColors.Keys), button);
            SetFillColor(_fillColors[button]);
        }
    }

    private void backColor_Clicked(object sender, EventArgs e)
    {
        ImageButton button = sender as ImageButton;
        if (button != null)
        {
            SetButtonStatus(new List<ImageButton>(_backColors.Keys), button);
            SetBackColor(_backColors[button]);
        }

        if (_backColor != GraphicsDrawable.CanvasColor)
        {
            GraphicsDrawable.CanvasColor = _backColor;
            GraphicsView.Invalidate();
        }
    }

    private void SetButtonStatus(List<ImageButton> buttonGroup, ImageButton clickButton)
    {
        foreach (ImageButton btn in buttonGroup)
        {
            SetButtonStatus(btn, Object.ReferenceEquals(btn, clickButton) ? _buttonSelectedColor : Colors.Transparent);
        }
    }

    private void SetButtonStatus(ImageButton button, Color color)
    {
        if (button != null)
        {
            button.BackgroundColor = color;
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

    private void SetFillColor(Color fillColor)
    {
        _fillColor = fillColor;
    }

    private void SetStrokeSize(float strokeSize)
    {
        _strokeSize = strokeSize;
    }

    private void SetBackColor(Color backColor)
    {
        _backColor = backColor;
    }

    private void more_Clicked(object sender, EventArgs e)
    {
        menuBar.IsVisible = !menuBar.IsVisible;
        contactBorder.IsVisible = menuBar.IsVisible && Email.Default.IsComposeSupported;
        more.Source = ImageSource.FromFile(menuBar.IsVisible ? "opt_more.png" : "opt_more_vertical.png");
    }

    private async void contact_item_Clicked(object sender, EventArgs e)
    {
        more_Clicked(sender, e);
        await SendEmail();
    }

    private async Task SendEmail()
    {
        if (Email.Default.IsComposeSupported)
        {
            string subject = "Hello friends!";
            string body = "It was great to see you last weekend.";
            string[] recipients = new[] { "j526180212@outlook.com" };

            var message = new EmailMessage
            {
                Subject = subject,
                Body = body,
                BodyFormat = EmailBodyFormat.PlainText,
                To = new List<string>(recipients)
            };

            await Email.Default.ComposeAsync(message);
        }
    }

    private async void about_item_Clicked(object sender, EventArgs e)
    {
        more_Clicked(sender, e);
        await Shell.Current.GoToAsync("//AboutPage");
    }
}

