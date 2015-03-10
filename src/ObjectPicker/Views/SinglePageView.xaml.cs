using Microsoft.ConnectedServices.Samples.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.ConnectedServices.Samples.Views
{
    /// <summary>
    /// Interaction logic for SinglePageView.xaml
    /// </summary>
    internal partial class SinglePageView : UserControl
    {
        public SinglePageView(SinglePageViewModel viewModel)
        {
            this.DataContext = viewModel;
            this.InitializeComponent();
        }

        private SinglePageViewModel ViewModel
        {
            get { return (SinglePageViewModel)this.DataContext; }
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await this.ViewModel.LoadObjectsAsync();
        }
    }
}
