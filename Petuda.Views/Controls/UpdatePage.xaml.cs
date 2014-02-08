using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using Petuda.ViewModels;

namespace Petuda.Views.Controls
{
    /// <summary>
    /// Interaction logic for UpdatePage.xaml
    /// </summary>
    public partial class UpdatePage : UserControl
    {
        public UpdatePage()
        {
            InitializeComponent();
        }

        private void textBlockOpenLink_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var textBlock = sender as TextBlock;
            if (textBlock == null)
            {
                return;
            }

            var url = textBlock.Text ?? "";
            Process.Start(new ProcessStartInfo(url));
        }

        private void textBlockDowloadInstaller_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var viewModel = (UpdatePageViewModel) this.DataContext;

            if (viewModel.RunUpdateInstallationCommand.CanExecute(null))
            {
                viewModel.RunUpdateInstallationCommand.Execute(null);
            }
        }

    }
}