using ExpandableListViewItemApp.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpandableListViewItemApp
{
    public partial class MainPage : ContentPage
    {
        private static INativeViewService _nativeViewService;
        private static double _screenHeight;

        public MainPage()
        {
            InitializeComponent();

            _nativeViewService = DependencyService.Get<INativeViewService>();

            // Obtener el alto de la pantalla en cada plataforma
            Rectangle rctScreen = _nativeViewService.GetScreenDimensions();
            _screenHeight = rctScreen.Height;

            listViewNearbyCinemas.ItemsSource = Cinema.GetFakeCinemas();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await System.Threading.Tasks.Task.Delay(200);

            double absoluteLayoutHeight = mainLayout.Height;

            // Obtener las coordenadas (en relación a pantalla) de la vista pulsada
            Rectangle rctProduct = _nativeViewService.GetScreenCoordinates((View)sender);

            // Regla de 3 para adapatar las coordenadas de pantalla al Grid principal
            double pointY = (rctProduct.Y * absoluteLayoutHeight) / _screenHeight;

            Grid gridTapped = (Grid)sender;
            var tapGesture = (TapGestureRecognizer)gridTapped.GestureRecognizers[0];

            Cinema cinema = (Cinema)tapGesture.CommandParameter;
            cinema.ViewPointY = pointY;
            cinema.gridHeight = gridTapped.Height;
            cinema.gridWidth = gridTapped.Width;

            await gridTapped.ScaleTo(0.95, 150);
            await gridTapped.ScaleTo(1, 150);

            var expandablePopup = new ExpandableItemPopup();
            expandablePopup.BindingContext = cinema;
            await PopupNavigation.Instance.PushAsync(expandablePopup);
        }
    }
}
