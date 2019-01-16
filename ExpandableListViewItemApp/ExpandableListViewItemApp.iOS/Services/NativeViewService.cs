using ExpandableListViewItemApp.Services;
using Xamarin.Forms;


[assembly: Xamarin.Forms.Dependency(typeof(ExpandableListViewItemApp.iOS.Services.NativeViewService))]
namespace ExpandableListViewItemApp.iOS.Services
{
    public class NativeViewService : INativeViewService
    {
        public Rectangle GetScreenDimensions()
        {
            System.nfloat height = UIKit.UIScreen.MainScreen.Bounds.Height;
            System.nfloat width = UIKit.UIScreen.MainScreen.Bounds.Width;
            return new Rectangle()
            {
                Height = height,
                Width = width
            };
        }

        public Rectangle GetScreenCoordinates(View view)
        {
            try
            {
                CoreGraphics.CGPoint coordinates = new CoreGraphics.CGPoint(0, 0);
                var nativeView = Xamarin.Forms.Platform.iOS.Platform.GetRenderer(view).NativeView;
                var rect = nativeView.ConvertPointToView(nativeView.Frame.Location, null);
                return new Rectangle() { X = rect.X, Y = rect.Y };
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Excepción en NativeViewService -> GetScreenCoordinates: " + ex.Message);
                return new Rectangle(-1, -1, -1, -1);
            }
        }
    }
}