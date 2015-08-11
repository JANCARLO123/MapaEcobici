using System.Linq;
using Windows.UI.Xaml;
using Ecobici.WP.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Ecobici.WP.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Ecobici.WP.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private void MapControl_OnCenterChanged(Windows.UI.Xaml.Controls.Maps.MapControl sender, object args)
        {
            VMMainPage vm = sender.DataContext as VMMainPage;

            vm?.GetNearStationsCommand.Execute(sender.Center);
        }

        private void UIElement_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var grid = sender as Grid;
            if (grid != null)
            {
                
                var childrenGrid =
                    grid.Children.OfType<Grid>().FirstOrDefault(pre => pre.Name.Equals("GridStationLogoDataItem"));
                if (childrenGrid.Visibility == Visibility.Visible)
                {
                    var storyBoardHideItem = grid.Resources["GridStationLogoHideItem"] as Storyboard;
                    storyBoardHideItem?.Begin();
                    var storyBoardShowItem = grid.Resources["GridStationInfoShowItem"] as Storyboard;
                    storyBoardShowItem?.Begin();
                }
                else
                {
                    var storyBoardShowItem = grid.Resources["GridStationLogoShowItem"] as Storyboard;
                    storyBoardShowItem?.Begin();
                    var storyBoardHideItem = grid.Resources["GridStationInfoHideItem"] as Storyboard;
                    storyBoardHideItem?.Begin();
                }

            }
        }
    }
}