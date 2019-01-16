using System;
using Xamarin.Forms;

namespace ExpandableListViewItemApp.CustomControls
{
    public class AbsoluteLayoutSwipe : AbsoluteLayout, ISwipeCallBack
    {
        public event EventHandler LargeBottomSwipe = (e, a) => { };

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            SwipeListener swipeListener = new SwipeListener(this, this);
        }

        public void onBottomSwipe(View view)
        {
            //
        }

        public void onLeftSwipe(View view)
        {
            //
        }

        public void onRightSwipe(View view)
        {
            //
        }

        public void onTopSwipe(View view)
        {
            //
        }

        public void onLargeBottomSwipe(View view)
        {
            LargeBottomSwipe(this, EventArgs.Empty);
        }
    }
}
