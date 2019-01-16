using ExpandableListViewItemApp.Services;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpandableListViewItemApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExpandableItemPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ExpandableItemPopup ()
		{
			InitializeComponent ();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            
            Cinema cinema = (Cinema)this.BindingContext;
            gridContent.HeightRequest = cinema.gridHeight;
            gridContent.WidthRequest = cinema.gridWidth;

            // Posicionamos el Grid
            AbsoluteLayout.SetLayoutBounds(gridContent, new Rectangle(0, cinema.ViewPointY, 1, -1));
            AbsoluteLayout.SetLayoutFlags(gridContent, AbsoluteLayoutFlags.WidthProportional);
        }

        private void MainExpLayout_SizeChanged(object sender, System.EventArgs e)
        {
            if (mainExpLayout.Height == -1 || mainExpLayout.Width == -1)
                return;

            Cinema cinema = (Cinema)this.BindingContext;
            double start = cinema.ViewPointY;
            double end = 0;

            uint rate = 10; // pace at which animation proceeds
            uint length = 600; // 0.6 second animation
            Easing easing = Easing.CubicOut;

            //
            Action<double> callback = input =>
            {
                AbsoluteLayout.SetLayoutBounds(gridContent, new Rectangle(0, input, 1, -1));
                AbsoluteLayout.SetLayoutFlags(gridContent, AbsoluteLayoutFlags.WidthProportional);
            };

            // Altura a aumentar
            Action<double> callbackHeight = input =>
            {
                gridContent.HeightRequest = input;
            };

            // Close icon mostrar con opacity
            Action<double> callbackImage = input =>
            {
                imageClose.Opacity = input / 100;
            };

            // Cambiar Padding y RoundedCorner
            Action<double> callbackPadding = input =>
            {
                gridContent.Padding = new Thickness(input);
                frameContent.CornerRadius = (float)input + ((input > 3) ? 3 : 2);
            };
            Action<double, bool> finishedPadding = (input, valid) =>
            {
                imageClose.IsVisible = true;
                imageClose.Animate("closeicon", callbackImage, 0, 100, 10);
                gridContent.Padding = new Thickness(0);
                frameContent.CornerRadius = 0;
            };

            gridContent.IsVisible = true;

            gridContent.Animate("expandable", callback, start, end, rate, length, easing);
            gridContent.Animate("cahngeheight", callbackHeight, gridContent.Height, mainExpLayout.Height, rate, length, easing);
            gridContent.Animate("changepadding", callbackPadding, 7, 0, rate, length, easing, finishedPadding);
        }

        private async void CloseImage_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}