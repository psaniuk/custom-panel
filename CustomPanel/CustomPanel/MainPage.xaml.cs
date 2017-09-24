using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml.Controls;


namespace CustomPanel
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPageLoaded;                
        }

        private async void MainPageLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e) => await SetItemsSource();

        private async Task SetItemsSource()
        {
            StorageFolder folder = Package.Current.InstalledLocation;
            StorageFolder photosFolder = await folder.GetFolderAsync("Photos");
            IReadOnlyList<StorageFile> files = await photosFolder.GetFilesAsync();
            ItemsControl.ItemsSource = files.Select(f => new Uri(f.Path));
        }
    }
}
