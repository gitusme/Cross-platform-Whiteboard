namespace Com.Gitusme.Whiteboard;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
	}

    private async void about_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("//MainPage");
    }
}