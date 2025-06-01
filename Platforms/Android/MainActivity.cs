using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace Proyecto_Grupo3
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Cambia el color del status bar a negro  
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop && Window != null)
            {
                Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#0066CC"));

            }
        }
    }
}
