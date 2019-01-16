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

            // Animación que va cambiando la Y
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
                frameContent.CornerRadius = (float)input + ((input > 3) ? 5 : 2);
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

        private void MainExpLayout_LargeBottomSwipe(object sender, EventArgs e)
        {
            CloseImage_Tapped(sender, e);
        }

        private void CloseImage_Tapped(object sender, EventArgs e)
        {
            Cinema cinema = (Cinema)this.BindingContext;
            double originalHeight = cinema.gridHeight;
            double originalY = cinema.ViewPointY;

            double start = 0;
            double end = originalY;

            uint rate = 10; // pace at which animation proceeds
            uint length = 600; // 0.6 second animation
            Easing easing = Easing.CubicIn;

            // Animación que va cambiando la Y
            Action<double> callback = input =>
            {
                AbsoluteLayout.SetLayoutBounds(gridContent, new Rectangle(0, input, 1, -1));
                AbsoluteLayout.SetLayoutFlags(gridContent, AbsoluteLayoutFlags.WidthProportional);
            };

            // Altura a disminuir
            Action<double> callbackHeight = input =>
            {
                gridContent.HeightRequest = input;
            };

            // Cambiar Padding y RoundedCorner
            Action<double> callbackPadding = input =>
            {
                gridContent.Padding = new Thickness(input);
                frameContent.CornerRadius = (float)input + ((input > 3) ? 2 : 5);
            };
            Action<double, bool> finishedPadding = async (input, valid) =>
            {
                gridContent.Padding = new Thickness(7);
                frameContent.CornerRadius = 10;
                await PopupNavigation.Instance.PopAsync();
            };

            gridContent.Animate("collapse", callback, start, end, rate, length, easing);
            gridContent.Animate("collapseheight", callbackHeight, mainExpLayout.Height, originalHeight, rate, length, easing);
            gridContent.Animate("collapsepadding", callbackPadding, 0, 7, rate, length, easing, finishedPadding);

            imageClose.IsVisible = false;
        }
    }
}