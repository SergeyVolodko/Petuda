using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Petuda.Views.Helpers;
using Petuda.Views.Resources;

namespace Petuda.Views
{
    /// <summary>
    /// Interaction logic for ScriptEditor.xaml
    /// </summary>
    public partial class ScriptEditor : Window
    {
        public ScriptEditor()
        {
            InitializeComponent();
        }

        private void closeWindow_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // "Select a date" watermark fix
        private void DatePicker_OnLoaded(object sender, RoutedEventArgs e)
        {
            var dp = sender as DatePicker;
            if (dp == null)
            {
                return;
            }

            var tb = PetudaVisualTreeHelper.GetChildOfType<DatePickerTextBox>(dp);
            if (tb == null)
            {
                return;
            }

            var wm = tb.Template.FindName("PART_Watermark", tb) as ContentControl;
            if (wm == null)
            {
                return;
            }

            wm.Content = Strings.SelectADate;
        }
    }
}