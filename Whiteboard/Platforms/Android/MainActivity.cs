using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace Com.Gitusme.Whiteboard;

//[Activity(ConfigurationChanges = ConfigChanges.Density |
//                                 ConfigChanges.Orientation |
//                                 ConfigChanges.ScreenLayout |
//                                 ConfigChanges.ScreenSize |
//                                 ConfigChanges.SmallestScreenSize |
//                                 ConfigChanges.UiMode,
//          LaunchMode = LaunchMode.SingleTop,
//          MainLauncher = true,
//          Theme = "@style/Maui.SplashTheme")]


[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize
    | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize
    | ConfigChanges.Density)]

public class MainActivity : MauiAppCompatActivity
{
    private void SetWindowLayout()
    {
        if (Window != null)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
            {
#pragma warning disable CA1416

                IWindowInsetsController wicController = Window.InsetsController;


                Window.SetDecorFitsSystemWindows(false);
                Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

                if (wicController != null)
                {
                    wicController.Hide(WindowInsets.Type.Ime());
                    wicController.Hide(WindowInsets.Type.NavigationBars());
                }
#pragma warning restore CA1416
            }
            else
            {
#pragma warning disable CS0618


                //Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);


                Window.ClearFlags(WindowManagerFlags.TranslucentStatus
                        | WindowManagerFlags.TranslucentNavigation);

                //Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(
                //                                                                //SystemUiFlags.Fullscreen | 
                //                                                                //SystemUiFlags.LayoutFullscreen |
                //                                                             SystemUiFlags.HideNavigation |
                //                                                             SystemUiFlags.Immersive |
                //                                                             //SystemUiFlags.ImmersiveSticky |
                //                                                             //SystemUiFlags.LayoutHideNavigation |
                //                                                             SystemUiFlags.LightNavigationBar|
                //                                                             SystemUiFlags.LayoutStable// |
                //                                                             //SystemUiFlags.LowProfile
                //                                                             );

                Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                Window.SetStatusBarColor(Android.Graphics.Color.Blue);
                Window.SetNavigationBarColor(Android.Graphics.Color.Blue);

                //SupportActionBar.Hide();

#pragma warning restore CS0618
            }
        }
    }

    protected override void OnCreate(
        Bundle bSavedInstanceState)
    {
        base.OnCreate(bSavedInstanceState);

        // 设置状态颜色
        Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#34A2DA"));
        Window.SetNavigationBarColor(Android.Graphics.Color.ParseColor("#34A2DA"));

        // 强制竖屏
        RequestedOrientation = ScreenOrientation.Portrait;

        //SetWindowLayout();
        //SetFullscreen();
    }

    private void SetFullscreen()
    {
        if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
        {
            //Window.ClearFlags(WindowManagerFlags.TranslucentStatus
            //        | WindowManagerFlags.TranslucentNavigation);
            ////Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(
            ////    SystemUiFlags.HideNavigation | SystemUiFlags.Immersive | SystemUiFlags.ImmersiveSticky |
            ////    SystemUiFlags.LayoutHideNavigation | SystemUiFlags.LayoutStable | SystemUiFlags.LowProfile);
            //Window.DecorView.SystemUiVisibility = (StatusBarVisibility)( 
            //                                                                 SystemUiFlags.LayoutFullscreen |
            //                                                                 SystemUiFlags.LayoutHideNavigation |
            //                                                                 SystemUiFlags.LayoutStable);
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(
                SystemUiFlags.Fullscreen |
                SystemUiFlags.LayoutFullscreen |
                                                                         SystemUiFlags.HideNavigation |
                                                                         SystemUiFlags.Immersive |
                                                                         SystemUiFlags.ImmersiveSticky |
                                                                         SystemUiFlags.LayoutHideNavigation |
                                                                         SystemUiFlags.LayoutStable |
                                                                         SystemUiFlags.LowProfile);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
            Window.SetNavigationBarColor(Android.Graphics.Color.Transparent);
        }
        else if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
        {
            // 实现透明状态栏
            Window.AddFlags(WindowManagerFlags.TranslucentStatus);
            // 实现透明导航栏
            Window.AddFlags(WindowManagerFlags.TranslucentNavigation);
        }
    }

}