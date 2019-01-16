using Xamarin.Forms;

namespace ExpandableListViewItemApp.CustomControls
{
    public interface ISwipeCallBack
    {
        void onLeftSwipe(View view);
        void onRightSwipe(View view);
        void onTopSwipe(View view);
        void onBottomSwipe(View view);
        void onLargeBottomSwipe(View view);
    }
}
