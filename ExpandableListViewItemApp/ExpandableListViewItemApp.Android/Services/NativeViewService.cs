using ExpandableListViewItemApp.Services;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Android.Util;

[assembly: Xamarin.Forms.Dependency(typeof(ExpandableListViewItemApp.Droid.Services.NativeViewService))]
namespace ExpandableListViewItemApp.Droid.Services
{
    public class NativeViewService : INativeViewService
    {
        private Android.App.Activity CurrentActivity => CrossCurrentActivity.Current.Activity;

        public Rectangle GetScreenDimensions()
        {
            DisplayMetrics displayMetrics = new DisplayMetrics();
            CurrentActivity.WindowManager.DefaultDisplay.GetMetrics(displayMetrics);
            int height = displayMetrics.HeightPixels;
            int width = displayMetrics.WidthPixels;
            return new Rectangle()
            {
                Width = width,
                Height = height
            };
        }

        public Rectangle GetScreenCoordinates(Xamarin.Forms.View view)
        {
            try
            {
                int[] coordinates = new int[2] { 0, 0 };
                var nativeView = Xamarin.Forms.Platform.Android.Platform.GetRenderer(view).View;
                nativeView.GetLocationOnScreen(coordinates);
                return new Rectangle() { X = coordinates[0], Y = coordinates[1] };
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Excepción en NativeViewService -> GetScreenCoordinates: " + ex.Message);
                return new Rectangle(-1, -1, -1, -1);
            }
        }
    }
}