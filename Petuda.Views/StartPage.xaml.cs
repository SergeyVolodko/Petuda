using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Petuda.ViewModels;
using Petuda.ViewModels.ViewModelsFactory;
using Petuda.Views.Navigation;

namespace Petuda.Views
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Window
    {
        private readonly StartPageViewModel viewModel;

        public StartPage()
        {
            InitializeComponent();
            
            InitializeCulture("ru-ru");

            this.viewModel = PetudaViewModelsFactory.CreateStartViewModel(new PetudaNavigationService(this));
            this.DataContext = this.viewModel;
        }

        private static void InitializeCulture(string language)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd.MMM.yyyy";
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                                                               new FrameworkPropertyMetadata(
                                                                   XmlLanguage.GetLanguage(
                                                                       CultureInfo.CurrentCulture.IetfLanguageTag)));
        }

        private void StartPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (this.viewModel.CheckForUpdatesCommand.CanExecute(null))
            {
                this.viewModel.CheckForUpdatesCommand.Execute(null);
            }
        }

        private void textBlockStart_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.viewModel.OpenMainVindowCommand.CanExecute(null))
            {
                this.viewModel.OpenMainVindowCommand.Execute(null);
                this.Close();
            }
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

        private void labelClose_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        
    }//winow
}//namespace