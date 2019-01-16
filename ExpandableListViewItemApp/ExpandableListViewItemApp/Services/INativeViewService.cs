using Xamarin.Forms;

namespace ExpandableListViewItemApp.Services
{
    public interface INativeViewService
    {
        Rectangle GetScreenDimensions();
        Rectangle GetScreenCoordinates(View view);
    }
}
