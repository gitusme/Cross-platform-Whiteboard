using System.Globalization;

namespace Com.Gitusme.Whiteboard;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        MainPage = new AppShell();
	}
}
