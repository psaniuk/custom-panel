using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace CustomPanel
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SetItemsSource();
        }

        private async void SetItemsSource()
        {
            StorageFolder folder = Package.Current.InstalledLocation;
            StorageFolder photosFolder = await folder.GetFolderAsync("Photos");
            IReadOnlyList<StorageFile> files = await photosFolder.GetFilesAsync();

            const int visibleItemsCount = 6;
            int hiddenItemsCount = files.Count - visibleItemsCount;

            ItemsControl.ItemsSource = files.Select((f, i) => new
            {
                ImageUri = new Uri(f.Path),
                Counter = $"+{i + 1}",
                OverlayVisiblity = hiddenItemsCount > 0 && i == visibleItemsCount - 1 ? Visibility.Visible : Visibility.Collapsed
            });
        }
    }
}
